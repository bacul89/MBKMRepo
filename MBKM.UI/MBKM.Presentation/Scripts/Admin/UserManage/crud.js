
function AddUserTemplate() {
    if ($('#created-user-template').length) {
        $('#TambahUser').modal('show');
    } else {
        $.ajax({
            url: '/Admin/UserManage/ModaladdUser',
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

function EditUserTemplate(id) {
    $.ajax({
        url: '/Admin/UserManage/ModalEditUser/' + id,
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

function DetailUserTemplate(id) {
    $.ajax({
        url: '/Admin/UserManage/ModalDetailUser/' + id,
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
function PostCreate2() {
    //getValueOnForm();
    var formInputUser = new Object();
    var namaProdi = document.getElementById("idProdi");
    var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
    var namaProdi = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
    formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
    formInputUser.UserName = $('input[id=txtnama]').val();
    formInputUser.Email = $('input[id=txtemail]').val();
    formInputUser.Password = $('input[id=txtpassword]').val();
    formInputUser.RoleID = $('#idRole').val();
    formInputUser.KodeProdi = $('#idProdi').val();
    //formInputUser.NamaProdi = $('#idProdi').val();
    formInputUser.NamaProdi = namaProdi;
    var cekAktif = $('input[id=inp_status]:checked').val();
    if (cekAktif == 1) {
        formInputUser.IsActive = "true";
    }
    else { formInputUser.IsActive = "false";}
    //formInputUser.IsActive = $('input[id=inp_status]:checked').val();

    if (validationCustom2()) {
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/UserManage/PostDataUser',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(formInputUser),
            contentType: 'application/json',
            success: function (e) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'User Baru Berhasil Ditambahkan',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                tableUser.ajax.reload(null, false);
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
function PostUpdateUser() {
    var formInputUser = new Object();
    var namaProdi = document.getElementById("idProdi");
    var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
    var namaProdis = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
    formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
    formInputUser.UserName = $('input[id=txtnama]').val();
    formInputUser.Email = $('input[id=txtemail]').val();
    formInputUser.Password = $('input[id=txtpassword]').val();
    formInputUser.RoleID = $('#idRole').val();
    formInputUser.KodeProdi = $('#idProdi').val();
    //formInputUser.NamaProdi = $('#idProdi').val();
    formInputUser.NamaProdi = namaProdis;
    var cekAktif = $('input[id=inp_status]:checked').val();
    if (cekAktif == 1) {
        formInputUser.IsActive = "true";
    }
    else { formInputUser.IsActive = "false"; }
    formInputUser.ID = $('#id_userTemplate').val();
    console.log(formInputUser);
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/UserManage/PostUpdateDataUser',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(formInputUser),
        contentType: 'application/json',
        success: function (e) {
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Data User Berhasil Diubah',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            tableUser.ajax.reload(null, false);
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
