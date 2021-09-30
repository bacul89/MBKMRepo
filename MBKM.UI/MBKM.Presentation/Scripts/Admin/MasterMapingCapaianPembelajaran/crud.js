
var dMasterMapingCapaianPembelajaran = {}

function getValueOnForm() {
    dMasterMapingCapaianPembelajaran.Tipe = $('input[name=inp_tipe]').val();
    dMasterMapingCapaianPembelajaran.Nama = $('input[name=inp_nama]').val();
    dMasterMapingCapaianPembelajaran.Nilai = $('input[name=inp_nilai]').val();
    dMasterMapingCapaianPembelajaran.IsActive = $('input[name=inp_status]:checked').val();
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

    /*console.log(dMasterMapingCapaianPembelajaran);*/
    if (validationCustom()) {
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
                    html: 'Master Lookup Berhasil Ditambahkan',
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
    } else {
        Swal.fire({
            title: 'Oppss',
            icon: 'warning',
            html: 'Ada beberapa field yang belum kamu isikan',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    }
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

function DeletedMasterMapingCapaianPembelajaran(idLookup) {

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
            data: { id: idLookup }
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
