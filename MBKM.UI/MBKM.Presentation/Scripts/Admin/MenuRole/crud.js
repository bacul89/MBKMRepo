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
                var selElem = document.getElementById('MenuID');
                sortSelect(selElem);
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

function sortSelect(selElem) {
    var tmpAry = new Array();
    for (var i = 0; i < selElem.options.length; i++) {
        tmpAry[i] = new Array();
        tmpAry[i][0] = selElem.options[i].text;
        tmpAry[i][1] = selElem.options[i].value;
    }
    tmpAry.sort();
    while (selElem.options.length > 0) {
        selElem.options[0] = null;
    }
    for (var i = 0; i < tmpAry.length; i++) {
        var op = new Option(tmpAry[i][0], tmpAry[i][1]);
        selElem.options[i] = op;
    }
    return;
}
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
        }).then(function (response) {
            if (response.status == 400) {
                Swal.fire({
                    title: 'Gagal!',
                    icon: 'error',
                    html: 'Menu Role Tersebut Telah Tersedia!',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })

                $('.modal').modal('hide');

                //$('#TambahUser')[0].reset();
            }
            else
                if (response.status == 200) {

                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: 'Menu Role Berhasil Ditambahkan!',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })

                    $('.modal').modal('hide');
                    //$("#fakultas").empty();
                    //$("#fakultas").prop("disabled", true);
                    //$("#prodi").empty();
                    //$("#prodi2").empty();
                    //$("#prodi").prop("disabled", true);
                    //$("#prodi2").prop("disabled", true);
                    //$("#jenjang").empty();
                    //$("#lokasi").empty();
                    //$("#lokasi").prop("disabled", true);
                    //$("#kelompok").empty();
                    //$("#kode").val("");
                    //$('textarea[name=txtCPL]').val("");
                    tableMenuRole.ajax.reload(null, false);
                }

        });
            //success: function (e) {
            //    Swal.fire({
            //        title: 'Berhasil',
            //        icon: 'success',
            //        html: 'Menu Berhasil Ditambahkan',
            //        showCloseButton: true,
            //        showCancelButton: false,
            //        focusConfirm: false,
            //        confirmButtonText: 'OK'
            //    })
            //    location.reload();
            //    tableMenuRole.ajax.reload(null, false);
            //    $('.modal').modal('hide');

            //},
            //error: function (e) {
            //    Swal.fire({
            //        title: 'Oppss',
            //        icon: 'error',
            //        html: 'Coba Reload Page',
            //        showCloseButton: true,
            //        showCancelButton: false,
            //        focusConfirm: false,
            //        confirmButtonText: 'OK'
            //    })
            //    $('.modal').modal('hide');
            //}
       // })
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
function sortlist() {
    var lb = document.getElementById('mylist');
    arrTexts = new Array();

    for (i = 0; i < lb.length; i++) {
        arrTexts[i] = lb.options[i].text;
    }

    arrTexts.sort();

    for (i = 0; i < lb.length; i++) {
        lb.options[i].text = arrTexts[i];
        lb.options[i].value = arrTexts[i];
    }
}
function alphabetizeList() {
    var sel = $('#MenuID');
    var selected = sel.val(); // cache selected value, before reordering
    var opts_list = sel.find('option:not(:selected)');
    opts_list.sort(function (a, b) {
        return $(a).text() > $(b).text() ? 1 : -1;
    });
    sel.html('').append(opts_list);
    sel.val(selected); // set cached selected value
    
}


function UpdateMenuRole(id) {
/*    $.LoadingOverlay("show");*/
    $.ajax({
        url: '/Admin/MenuRole/ModalUpdateMenuRole/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            /*$.LoadingOverlay("hide");*/
            /*console.log(e);*/
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            alphabetizeList('#MenuID');
            //$(".select option").each(function () {
            //    $(this).siblings('[value="' + this.value + '"]').remove();
            //});
            //var selElem1 = document.getElementById('MenuID2');
            //sortSelect(selElem1);
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
function PostUpdateMenuRole() {
    var dMenuRole = new Object();
    dMenuRole.ID = $('#id_MenuRole').val();
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
            url: base_url + '/Admin/MenuRole/PostUpdateMenuRole',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dMenuRole),
            contentType: 'application/json',
        }).then(function (response) {
            if (response.status == 400) {
                Swal.fire({
                    title: 'Gagal!',
                    icon: 'error',
                    html: 'Menu Role Tersebut Telah Tersedia!',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                $('.modal').modal('hide');
            }
            else
                if (response.status == 200) {

                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: 'Menu Role Berhasil Diubah!',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })

                    $('.modal').modal('hide');
                    
                    tableMenuRole.ajax.reload(null, false);
                }

        });
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
