
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

var formInputUser = {}

//function getValueOnForm() {
//    dTemplateEmail.TipeMail = $('input[name=inp_tipeEmail]').val();
//    dTemplateEmail.SubjectMail = $('input[name=inp_email]').val();
//    dTemplateEmail.BodyMail = $('textarea[name=inp_bodyEmail]').val();
//    dTemplateEmail.IsActive = $('input[name=inp_status]:checked').val();
//}
function getValueOnForm() {
    //model.NoPegawai = model.NoPegawai;
            //model.UserName = model.UserName;
            //model.Email = model.Email;
            //model.Password = model.Password;
            //model.RoleID = model.RoleID
            //model.NamaProdi = model.NamaProdi;
    formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
    formInputUser.UserName = $('input[id=txtnama]').val();
    formInputUser.Email = $('input[id=txtemail]').val();
    formInputUser.Password = $('input[id=txtpassword]').val();
    formInputUser.RoleID= $('#idRole').val();
    formInputUser.IsActive = $('input[id=inp_status]').val();
    //prodi unit belum
    
}
function tesRole(){
    console.log(formInputUser.RoleID = $('#idRole').val());
}
function PostCreate() {
    if (validationCustom()) {
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
                    html: 'Template Email Berhasil Ditambahkan',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                window.location.href = "/Admin/UserManage/index"
            },
            error: function (e) {
                console.log(e);
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