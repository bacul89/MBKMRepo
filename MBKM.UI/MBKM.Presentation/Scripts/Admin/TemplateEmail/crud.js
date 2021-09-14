
var dTemplateEmail = {}

function getValueOnForm() {
    dTemplateEmail.TipeMail = $('input[name=inp_tipeEmail]').val();
    dTemplateEmail.SubjectMail = $('input[name=inp_email]').val();
    dTemplateEmail.BodyMail = $('textarea[name=inp_bodyEmail]').val();
    dTemplateEmail.IsActive = $('input[name=inp_status]:checked').val();
}

function IndexCreateEmailTemplate() {
    if ($('#created-email-template').length) {
        $('#TambahMasterLookUp').modal('show');
    }else{
        $.ajax({
            url: '/Admin/TemplateEmail/ModalCreateEmailTemplate',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                console.log(e);
                if ($('.data-content-modal').length) {
                    $('.data-content-modal').remove();
                }
                $('#modal-inner').append(e);
                $('.modal').modal('show');
                $('.summernote').summernote({
                    placeholder: 'Input Body Email',
                    height: 300, // set editor height  
                    minHeight: null, // set minimum height of editor  
                });
            }
        })
    } 
}

function IndexUpdateEmailTemplate(id) {
    $.ajax({
        url: '/Admin/TemplateEmail/ModalUpdateEmailTemplate/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            console.log(e);
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            $('.summernote').summernote({
                placeholder: 'Input Body Email',
                height: 300, // set editor height  
                minHeight: null, // set minimum height of editor  
            });
        }
    })
}

function IndexViewEmailTemplate(id) {
    $.ajax({
        url: '/Admin/TemplateEmail/ModalDetailEmailTemplate/' + id,
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

function PostCreate() {
    getValueOnForm();
    if (validationCustom()) {
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/TemplateEmail/PostDataEmailTemplate',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dTemplateEmail),
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
                window.location.href = "/Admin/TemplateEmail/index"
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

function PostUpdate() {
    getValueOnForm();
    dTemplateEmail.ID = $('#id_emailTemplate').val();
    console.log(dTemplateEmail);
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/TemplateEmail/PostUpdateEmailTemplate',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(dTemplateEmail),
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
/*            window.location.href = "/Admin/TemplateEmail/index"
*/        },
        error: function (e) {
            console.log(e);
        }
    })
    
    /*Swal.fire({
        title: 'Oppss',
        icon: 'warning',
        html: 'Ada beberapa field yang belum kamu isikan',
        showCloseButton: true,
        showCancelButton: false,
        focusConfirm: false,
        confirmButtonText: 'OK'
    })*/
    
}

