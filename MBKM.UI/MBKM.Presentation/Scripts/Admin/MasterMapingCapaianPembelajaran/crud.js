
var dMasterMapingCapaianPembelajaran = {}

function getValueOnForm() {
    var hllo = $('#inp_kode_capaian_pembelajaran').val();
    console.log(hllo);
    dMasterMapingCapaianPembelajaran.IDMataKUliah = $('input[name=inp_id_mata_kuliah]').val();
    dMasterMapingCapaianPembelajaran.NamaMataKuliah = $('input[name=inp_mata_kuliah]').val();
    dMasterMapingCapaianPembelajaran.KodeMataKuliah = $('input[name=inp_kode_mata_kuliah]').val();
    dMasterMapingCapaianPembelajaran.MasterCapaianPembelajaranID = $('#inp_kode_capaian_pembelajaran').val();
    dMasterMapingCapaianPembelajaran.Kelompok = $('input[name=inp_kelompok]').val();
    dMasterMapingCapaianPembelajaran.IsActive = $('input[name=inp_status]:checked').val();

    /*console.log(dMasterMapingCapaianPembelajaran);*/
    /*dMasterMapingCapaianPembelajaran.IsDeleted =*/

}

function loadFromLookup(tipe, id, nama) {
    console.log(id);
    $("#" + id).select2({
        placeholder: "-- Pilih " + nama + " --",
        width: "100%",
        ajax: {
            url: "/MasterMapingCapaianPembelajaran/getLookupByTipe",
            dataType: 'json',
            method: "POST",
            delay: 250,
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
}

function loadMasterCPL() {

    $('#inp_kode_capaian_pembelajaran').select2({
        placeholder: "-- Pilih Kode CPL --",
        "proccessing": true,
        "serverSide": true,
        ajax: {
            url: '/Admin/MasterMapingCapaianPembelajaran/GetMasterCPL',
            type: 'post',
            dataType: 'json',
            data: function (params) {
                return {
                    //search: params.term,
                    IDProdi: $('#prodiCari').val(),
                    IDFakultas: $('#fakultasCari').val(),
                    Kelompok: $('#inp_kelompok').val()
                    
                    /*IDProdi: "0101",
                    IDFakultas: "0001",
                    Kelompok: "SIKAP"*/

                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.ID } })
                };
            },
        }
    });
    $("#inp_kode_capaian_pembelajaran").change(function () {
        /*dataParam = {};*/
        ID = $('#inp_kode_capaian_pembelajaran').val();
/*        dataParam.IDProdi       = "0101";
        dataParam.IDFakultas    = "0001";
        dataParam.Kelompok      = "SIKAP";*/

        $.ajax({
            url: '/Admin/MasterMapingCapaianPembelajaran/GetMasterCPLByID',
            dataType: 'json',
            method: "GET",
            delay: 250,
            cache: false,
            contentType: 'application/json',
            data: { id: ID},
            success: function (e) {
                console.log(e);
                $('#inp_capaian').val(e.Capaian);
            },
            error: function (e) {
                console.log("matakuliah not found...");
            }
        })
    });

}



function loadControl() {

    console.log("hello");
    loadFromLookup("Kelompok", "inp_kelompok", "Kelompok");
    loadMasterCPL();
    var inp_mata_kuliah =$("#matakuliahNamaCari").val();
    var inp_kode_mata_kuliah = $("#matakuliahKodeCari").val();
    var inp_id_mata_kuliah = $("#matakuliahCari").val();

    

    $('#inp_id_mata_kuliah').val(inp_id_mata_kuliah);
    $('#inp_kode_mata_kuliah').val(inp_kode_mata_kuliah);
    $('#inp_mata_kuliah').val(inp_mata_kuliah);


}



function IndexCreateMasterMapingCapaianPembelajaran() {
    if ($('#created-master-maping-cpl').length) {
        $('#TambahMasterMapingCapaianPembelajaran').modal('show');
    } else {
        $.ajax({
            url: '/Admin/MasterMapingCapaianPembelajaran/ModalCreateMasterMapingCapaianPembelajaran',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                /*console.log(e);*/
                if ($('.data-content-modal').length) {
                    $('.data-content-modal').remove();
                }
                $('#modal-inner').append(e);
                $('.modal').modal('show');
                loadControl();
            }
        })
    }
}

function IndexUpdateMasterMapingCapaianPembelajaran(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterMapingCapaianPembelajaran/ModalUpdateMasterMapingCapaianPembelajaran/' + id,
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
            loadFromLookup("Kelompok", "inp_kelompok", "Kelompok");
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

function IndexViewMasterMapingCapaianPembelajaran(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterMapingCapaianPembelajaran/ModalDetailMasterMapingCapaianPembelajaran/' + id,
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

function PostCreate() {
    dMasterMapingCapaianPembelajaran = {}
    getValueOnForm();

    console.log(getValueOnForm());

    /*console.log(dMasterMapingCapaianPembelajaran);*/
/*    if (validationCustom()) {*/
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/MasterMapingCapaianPembelajaran/PostDataMasterMapingCapaianPembelajaran',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dMasterMapingCapaianPembelajaran),
            contentType: 'application/json',
            success: function (e) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Data Berhasil Ditambahkan',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                dataTable.ajax.reload(null, false);
                $('.modal').modal('hide');
            },
            error: function (e) {
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
/*    } else {
        Swal.fire({
            title: 'Oppss',
            icon: 'warning',
            html: 'Ada beberapa field yang belum kamu isikan',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    }*/
}

function PostUpdate() {
    dMasterMapingCapaianPembelajaran = {}
    getValueOnForm();
    dMasterMapingCapaianPembelajaran.ID = $('#id_MasterMapingCapaianPembelajaran').val();
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/MasterMapingCapaianPembelajaran/PostUpdateMasterMapingCapaianPembelajaran',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(dMasterMapingCapaianPembelajaran),
        contentType: 'application/json',
        success: function (e) {
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Master Lookup Berhasil Diubah',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            dataTable.ajax.reload(null, false);
            $('.modal').modal('hide');
        },
        error: function (e) {
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

function DeletedMasterMapingCapaianPembelajaran(id) {

    Swal.fire({
        title: "Apakah anda yakin?",
        text: "warning",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }).then((result) => {
        $.ajax({
            url: '/Admin/MasterMapingCapaianPembelajaran/PostDeleteMasterMapingCapaianPembelajaran',
            type: "POST",
            data: { id: id }
            ,
            dataType: "json",
            success: function () {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Data Berhasil Terhapus',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                dataTable.ajax.reload(null, false);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                Swal.fire({
                    title: 'Oppss',
                    icon: 'error',
                    html: 'Coba Reload Page',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
            }
        });
    })

}
