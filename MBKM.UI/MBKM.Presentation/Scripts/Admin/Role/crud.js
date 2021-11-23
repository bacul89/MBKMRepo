var dRole = {}
function getValueOnForm() {
    dRole.Code = $('input[name=Code]').val();
    dRole.RoleName = $('input[name=RoleName]').val();
    dRole.IsActive = $('input[name=IsActive]:checked').val();
}

function IndexCreateRole() {
    if ($('#created-Role').length) {
        $('#TambahRole').modal('show');
    } else {
        $.ajax({
            url: '/Admin/Role/ModalCreateRole',
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

function IndexUpdateRole(id) {
    $.ajax({
        url: '/Admin/Role/ModalUpdateRole/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
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
    dRole = {}
    getValueOnForm();

    if (validationCustom()) {
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/Role/PostDataRole',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dRole),
            contentType: 'application/json',
            success: function (e) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Role Berhasil Ditambahkan',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                tableMenuRole.ajax.reload(null, false);
                $('.modal').modal('hide');
                clearValueOnForm();
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
    dRole = {}
    getValueOnForm();
    dRole.ID = $('#id_Role').val();
    /* console.log(dMasterLookup);*/
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/Role/PostUpdateRole',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(dRole),
        contentType: 'application/json',
        success: function (e) {
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Data Role Berhasil Diubah',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            tableMenuRole.ajax.reload(null, false);
            $('.modal').modal('hide');
        },
        error: function (e) {
            Swal.fire({
                title: 'Berhasil',
                icon: 'error',
                html: 'Data Role Gagal Diubah',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            tableMenuRole.ajax.reload(null, false);
            $('.modal').modal('hide');
        }
    })



}
function DetailRole(id) {
    $.ajax({
        url: '/Admin/Role/ModalDetailRole/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
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

function DeletedMenuRole(id) {

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
            url: '/Admin/Role/PostDeleteRole',
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
                tableMenuRole.ajax.reload(null, false);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Data Berhasil Terhapus',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                tableMenuRole.ajax.reload(null, false);
            }
        });
    })

}