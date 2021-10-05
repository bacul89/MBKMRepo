var dMasterMhs = {}
function DetailMhs(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/DaftarSeluruhMahasiswa/ModalDetailMhs/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
            /*console.log(e);*/
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            $("#Update").hide();
            //alert($("#noKJs").val());
            loadBiaya($("#noKJs").val());
            $("#Edit").click(function () {
                //$("input").removeAttr("disabled");
            
                //$("select").removeAttr("disabled");
                //$("textarea").removeAttr("disabled");
                //$("#simpan").show();
                $("#Update").show();
                loadNoKerjasama();
                $("#Edit").hide();
                $("#statusKJ").removeAttr("disabled");
                //$("#noKJs").removeAttr("hidden");
                $("#noKJ").removeAttr("disabled");
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
    dMasterMhs.BiayaKuliah = by;
    dMasterMhs.ID = id;
    dMasterMhs.NoKerjasama = latest_valueKJ;
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
                html: 'User Baru Berhasil Diedit',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            tableUser.ajax.reload(null, false);
            $('#detail-seluruhMHS-template').modal('hide');


        }
    });
        
    
}
function getValueOnForm() {
    dMasterLookup.Tipe = $('input[name=inp_tipe]').val();
    dMasterLookup.Nama = $('input[name=inp_nama]').val();
    dMasterLookup.Nilai = $('input[name=inp_nilai]').val();
    dMasterLookup.IsActive = $('input[name=inp_status]:checked').val();
}