var dMenuRole = {}

function getValueOnForm() {
    dMenuRole.MenuID = $('input[name=MenuID]').val();
    dMenuRole.RoleID = $('textarea[name=RoleID]').val();
    dMenuRole.IsActive = $('input[name=IsActive]:checked').val();
    dMenuRole.IsCreate = $('input[name=IsCreate]:checked').val();
    dMenuRole.IsView = $('input[name=IsView]:checked').val();
    dMenuRole.IsUpdate = $('input[name=IsUpdate]:checked').val();
    dMenuRole.IsDelete = $('input[name=IsDelete]:checked').val();
}

function AddMenuRole() {
    if ($('#created-MenuRole').length) {
        $('#TambahMenuRole').modal('show');
    } else {
        $.ajax({
            url: '/Admin/MenuRole/ModalAddMenuRole',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                console.log(e);
                if ($('.data-content-modal').length) {
                    $('.data-content-modal').remove();
                }
                $('#modal-inner').append(e);
                $('.modal').modal('show');
            }
        })
    }
}

//function PostCreate2() {
//    dMenuRole = {}
//    getValueOnForm();

//    if (validationCustom()) {
//        var base_url = window.location.origin;
//        $.ajax({
//            url: base_url + '/Admin/MenuRole/PostDataMenuRole',
//            type: 'post',
//            datatype: 'json',
//            data: JSON.stringify(dMenuRole),
//            contentType: 'application/json',
//            success: function (e) {
//                Swal.fire({
//                    title: 'Berhasil',
//                    icon: 'success',
//                    html: 'Menu Berhasil Ditambahkan',
//                    showCloseButton: true,
//                    showCancelButton: false,
//                    focusConfirm: false,
//                    confirmButtonText: 'OK'
//                })
//                location.reload();
//                tableMenuRole.ajax.reload(null, false);
//                $('.modal').modal('hide');

//            },
//            error: function (e) {
//                Swal.fire({
//                    title: 'Oppss',
//                    icon: 'error',
//                    html: 'Coba Reload Page',
//                    showCloseButton: true,
//                    showCancelButton: false,
//                    focusConfirm: false,
//                    confirmButtonText: 'OK'
//                })
//                $('.modal').modal('hide');
//            }
//        })
//    } else {
//        Swal.fire({
//            title: 'Oppss',
//            icon: 'warning',
//            html: 'Ada beberapa field yang belum kamu isikan',
//            showCloseButton: true,
//            showCancelButton: false,
//            focusConfirm: false,
//            confirmButtonText: 'OK'
//        })
//    }
//}
function PostCreate2() {
    var dMenuRole = new Object();
    dMenuRole.MenuID = $('#MenuID').val();
    dMenuRole.RoleID = $('#RoleID').val();
    dMenuRole.IsCreate = $('#IsCreate').val();
    dMenuRole.IsView = $('#IsView').val();
    dMenuRole.IsUpdate = $('#IsUpdate').val();
    dMenuRole.IsDelete = $('#IsDelete').val();
    var cekAktif = $('input[id=IsActive]:checked').val();
    if (cekAktif == 1) {
        dMenuRole.IsActive = "true";
    }
    else { dMenuRole.IsActive = "false"; }

    var cekCreate = $('input[id=IsCreate]:checked').val();
    if (cekCreate == 1) {
        dMenuRole.IsCreate = "true";
    }
    else { dMenuRole.IsCreate = "false"; }

    var cekView = $('input[id=IsView]:checked').val();
    if (cekView == 1) {
        dMenuRole.IsView = "true";
    }
    else { dMenuRole.IsView = "false"; }

    var cekUpdate = $('input[id=IsUpdate]:checked').val();
    if (cekUpdate == 1) {
        dMenuRole.IsUpdate = "true";
    }
    else { dMenuRole.IsUpdate = "false"; }

    var cekDelete = $('input[id=IsDelete]:checked').val();
    if (cekDelete == 1) {
        dMenuRole.IsDelete = "true";
    }
    else { dMenuRole.IsDelete = "false"; }
        if (validationCustom()) {
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/MenuRole/PostDataMenuRole',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dMenuRole),
            contentType: 'application/json',
            success: function (e) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Menu Berhasil Ditambahkan',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                location.reload();
                tableMenuRole.ajax.reload(null, false);
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
function DetailMenuRole(id) {
    $.ajax({
        url: '/Admin/MenuRole/ModalDetailMenuRole/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            console.log(e);
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
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
            url: '/Admin/MenuRole/PostDeleteMenuRole',
            type: "POST",
            data: { id: id }
            ,
            dataType: "json",
            success: function () {
                Swal.fire({
                    title: 'Oppss',
                    icon: 'error',
                    html: 'Coba Reload Page',
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
                location.reload();
            }
        });
    })

}