var dMasterMhs = {}
var isInternal = false;
function prep(id) {
    $.ajax({
        url: '/Admin/DaftarSeluruhMahasiswa/GetMHS?id=' + id,
        type: 'get'
    }).then(function (response) {
        $("#jenisKegiatanMBKM").select2({
            placeholder: "-- Pilih Jenis Kegiatan --"
        });
        if (response.NIM == response.NIMAsal && response.NIM && response.NIMAsal) {
        //if (1==1) {
            isInternal = true;

            $('#eksternal').hide();
            setLookupValue("StatusKerjasama", "ADA KERJASAMA", "statusKerjasamaInternal");
            $("#jenisKegiatanMBKM").prop("disabled", true);
            $("#jenisProgramMBKM").select2({
                placeholder: "-- Pilih Jenis Program --",
                width: "100%",
                ajax: {
                    url: "/Admin/DaftarSeluruhMahasiswa/GetProgram",
                    dataType: 'json',
                    method: "POST",
                    delay: 250,
                    cache: false,
                    data: function (params) {
                        return {
                            Search: params.term || ""
                        };
                    },
                    processResults: function (data, params) {
                        return {
                            results: $.map(data, function (item) { return { id: item.JenisPertukaran, value: item.JenisPertukaran, text: item.JenisPertukaran } })
                        };
                    },
                }
            });
            $("#jenisProgramMBKM").change(function () {
                if (!$("#jenisProgramMBKM").val().includes('Non Pertukaran')) {
                    $("#trNoSKTugas").hide();
                    $("#trTanggalSKTugas").hide();
                    $("#trJudulAktivitas").hide();
                    $("#trLokasi").hide();
                } else {
                    $("#trNoSKTugas").show();
                    $("#trTanggalSKTugas").show();
                    $("#trJudulAktivitas").show();
                    $("#trLokasi").show();
                }
                $("#jenisKegiatanMBKM").empty();
                $("#jenisKegiatanMBKM").prop("disabled", false);

                $("#jenisKegiatanMBKM").select2({
                    placeholder: "-- Pilih Jenis Kegiatan --",
                    width: "100%",
                    ajax: {
                        url: "/Admin/DaftarSeluruhMahasiswa/GetKegiatanByProgram",
                        dataType: 'json',
                        method: "POST",
                        delay: 250,
                        cache: false,
                        data: function (params) {
                            return {
                                Search: params.term || "",
                                program: $('#jenisProgramMBKM').val()
                            };
                        },
                        processResults: function (data, params) {
                            return {
                                results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.JenisKerjasama } })
                            };
                        },
                    }
                });
            });
        } else {
            isInternal = false;
            $('#internal').hide();
        }
    });
}
function setLookupValue(tipe, value, id) {
    $.ajax({
        type: "GET",
        url: "/Admin/DaftarSeluruhMahasiswa/getLookupByValue?tipe=" + tipe + "&value=" + value
    }).then(function (response) {
        var option = $("<option selected='selected'></option>").val(response.Nilai).text(response.Nama);
        $("#" + id).append(option).trigger('change');
    });
}
function DetailMhs(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/DaftarSeluruhMahasiswa/ModalDetailMhs/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
           // console.log(data);
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            prep(id);
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            $("#Update").hide();
            //alert($("#noKJs").val());
            loadBiaya($("#noKJs").val());
            $("#Edit").click(function () {
                //$("input").removeAttr("disabled");
                $("#statusBayarDiv").removeAttr("disabled");
                $("#jenisProgramMBKM").removeAttr("disabled");

                //$("select").removeAttr("disabled");
                //$("textarea").removeAttr("disabled");

                loadFromLookup("StatusKerjasama", "skj", "StatusKerjasama");
                $("#skj").change(function () {
                    $("#noKJ").prop('disabled', true);
                    $("#noKJ").empty();
                    var latest_valueskj = $("option:selected:first", "#skj").val();
                    //alert(latest_valueskj)
                    if (latest_valueskj == "ADA KERJASAMA") {
                        $("#noKJ").removeAttr("disabled");
                        
                    }
                    
                });

                //$("#simpan").show();
                $("#skj").removeAttr("disabled");
                $("#sKerjaSama2").prop('hidden', true);
                $("#sKerjaSama1").prop('hidden', false);
                $("#Update").show();
                loadNoKerjasama();
                $("#Edit").hide();
                //$("#noKJs").removeAttr("hidden");
                //THISTOENABLE$("#noKJ").removeAttr("disabled");
                $("#noKerjaSama2").prop('hidden',true);
                $("#noKerjaSama1").prop('hidden', false);
                
                //loadBiaya(nmrkj);

                //var latest_valueKJ = $("option:selected:first", "#noKJ").text();
                //$("#noKJ").val(latest_valueKJ);
                //alert(latest_valueKJ);   
                //$("#noKerjaSama").html("<select name='noKerjasama' id='noKerjasama' class='form-control input-lg' required></select>");
                $("#Update").click(function () {
                    //var latest_valueKJ = $("option:selected:first", "#noKJ").text();
                    // $("#noKJ").val(latest_valueKJ);
                    //alert(latest_valueKJ);
                    UpdateKJ(id);
                });
            });
        }, error: function (e) {
            $.LoadingOverlay("hide");
            Swal.fire({
                title: 'Oppss',
                icon: 'error',
                html: 'Coba Reload Page',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            $('.modal').modal('hide');
        }
    })

}
function loadBiaya(nokj) {
    //$("#noKJ").html("<select name='noKerjasama' id='noKJ' class='form-control input-lg' required><option value='starter' selected>Starter </option></select>");
    //var b = $('#noKJ').select2('data')[0].value;
    $.ajax({
        cache: false,
        type: "GET",
        async: false,
        url: '/Admin/DaftarSeluruhMahasiswa/GetBiaya',
        data: { "NoKerjasama": nokj },
        success: function (data) {
            $('input[name=biaya]').val(data);
            //alert(data);
            //alert($('input[name=biaya]').val());
            
        },
        error: function () {
            //alert("error");
        }
    });
}
function loadNoKerjasama() {
    //$("#noKJ").html("<select name='noKerjasama' id='noKJ' class='form-control input-lg' required><option value='starter' selected>Starter </option></select>");
    $("#noKJ").select2({
        placeholder: "-- Pilih Nomor Kerjasama --",
        width: "100%",
        ajax: {
            url: '/Admin/DaftarseluruhMahasiswa/GetNoKerjasama',
            dataType: 'json',
            method: "POST",
            delay: 250,
            cache: false,
            data: function (params) {
                return {
                    Length: "10",
                    Search: params.term || "",
                    Skip: params.page - 1 || 0,
                    NamaInstansi: $('#namaUniversitas').val()
                };
            },
            processResults: function (data, params) {
                var page = params.page || 1;
                return {
                    results: $.map(data, function (item) { return { id: item.NoKerjasama, value: item.NoKerjasama, text: item.NoKerjasama } }),
                    pagination: {
                        more: (page * 10) <= data.length
                    }
                };
            },
            
        }

    });
    $('#noKJ').on('select2:select', function (e) {
        console.log(e.params.data.text);
        //$("#noKJ").html("<select name='noKerjasama' id='noKJ' class='form-control input-lg' required><option value='starter' selected>e.params.data.text </option></select>");
        //$("#noKJ").val(e.params.data.text);
    //    var latest_valueKJ = e.params.data.text;
    ////$("#noKJ").val(latest_valueKJ);
    //alert(latest_valueKJ);
    });
}
function UpdateKJ(id) {
    if (isInternal) {
        UpdateInternal(id);
    }
    dMasterMhs = {}
    //getValueOnForm();
    //var b = $('#namaUniversitas').select2('data')[0].biaya;
    //dMasterMhs.BiayaKuliah = $('#namaUniversitas').select2('data')[0].biaya;
    var latest_valueKJ = $("option:selected:first", "#noKJ").text();
    //var nmrkj = $('#noKJ').select2('data')[0].value;
    //var c = loadBiaya(nmrkj);
    loadBiaya(latest_valueKJ);
    var by = $('input[name=biaya]').val();
    //window.setTimeout(loadBiaya(nmrkj), 1);
    console.log(($('input[name=biaya]').val()));
    var latest_valueskj = $("option:selected:first", "#skj").val();
    console.log(latest_valueskj);
    dMasterMhs.BiayaKuliah = by;
    dMasterMhs.ID = id;
    dMasterMhs.NoKerjasama = latest_valueKJ;
    dMasterMhs.StatusKerjasama = latest_valueskj;
    var cekAktif = $('input[id=inp_status]:checked').val();
    if (cekAktif == 1) {
        dMasterMhs.FlagBayar = "true";
    }
    else { dMasterMhs.FlagBayar = "false"; }
    //alert(latest_valueKJ);
    //alert(id);
    //alert(by);
    /* console.log(dMasterLookup);*/
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/DaftarseluruhMahasiswa/UpdateKJ',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(dMasterMhs),
        contentType: 'application/json',
    }).then(function (response) {
        if (response.status == 200) {
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Kerjasama Berhasil Diedit',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            $('.modal').modal('hide');
            tableUser.ajax.reload(null, false);


        }
    });
        
    
}
function UpdateInternal(id) {
    var data = new Object();
    data.id = id;
    data.JenisPertukaran = $('#jenisProgramMBKM').select2('data')[0].text;
    data.JenisKerjasama = $('#jenisKegiatanMBKM').select2('data')[0].text;
    console.log(id);
    if ($("#jenisProgramMBKM").val().includes('Non Pertukaran')) {
        data.JudulAktivitas = $('#judulAktivitas').val();
        data.LokasiTugas = $('#lokasiTugas').val();
        data.TanggalSK = $('#tanggalSKTugas').val();
        data.NoSK = $('#noSKTugas').val();
    }
    $.ajax({
        type: "POST",
        url: "/Admin/DaftarseluruhMahasiswa/InsertInformasiPertukaran",
        data: data
    });
}
function loadFromLookup(tipe, id, nama) {

    $("#" + id).select2({
        placeholder: "-- Pilih " + nama + " --",
        width: "100%",
        ajax: {
            url: '/Admin/DaftarseluruhMahasiswa/getLookupByTipe',
            dataType: 'json',
            method: "POST",
            delay: 250,
            async: false,
            cache: false,
            data: function (params) {
                return {
                    Tipe: tipe
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) { return { id: item.Nilai, value: item.Nilai, text: item.Nama } })
                };
            },
        }
    });
    var latest_valueskj = $("option:selected:first", "#skj").text();
    console.log(latest_valueskj);
}
function getValueOnForm() {
    dMasterLookup.Tipe = $('input[name=inp_tipe]').val();
    dMasterLookup.Nama = $('input[name=inp_nama]').val();
    dMasterLookup.Nilai = $('input[name=inp_nilai]').val();
    dMasterLookup.IsActive = $('input[name=inp_status]:checked').val();
}
