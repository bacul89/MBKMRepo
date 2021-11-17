
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
    } else {
        $.LoadingOverlay("show");
        $.ajax({
            url: '/Admin/TemplateEmail/ModalCreateEmailTemplate',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                $.LoadingOverlay("hide");
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
}

function IndexUpdateEmailTemplate(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/TemplateEmail/ModalUpdateEmailTemplate/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
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

function IndexViewEmailTemplate(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/TemplateEmail/ModalDetailEmailTemplate/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
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
    $.LoadingOverlay("show");
    dTemplateEmail = {}
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
                $.LoadingOverlay("hide");
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Template Email Berhasil Ditambahkan',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                dataTable.ajax.reload(null, false);
                $('.modal').modal('hide');
            },
            error: function (e) {
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
    } else {
        $.LoadingOverlay("hide");
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
    $.LoadingOverlay("show");
    dTemplateEmail = {}
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
            $.LoadingOverlay("hide");
            if (e.status == 500) {
                Swal.fire({
                    title: 'Oppss',
                    icon: 'error',
                    html: e.message,
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
            } else {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Template Email Berhasil Diubah',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                dataTable.ajax.reload(null, false);
                $('.modal').modal('hide');
            }
        },
        error: function (e) {
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

function DeleteDataTemplate(idt) {
    swal.fire({
        title: "Apakah anda yakin?",
        type: "warning",
        icon: "warning", 
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {
            $.LoadingOverlay("show");
            $.ajax({
                url: '/Admin/TemplateEmail/PostDeleteEmailTemplate/',
                type: 'post',
                data: {
                    id: idt
                },
                datatype: 'json',
                success: function (e) {
                    $.LoadingOverlay("hide");
                    if (e.status == 500) {
                        Swal.fire({
                            title: 'Oppss',
                            icon: 'error',
                            html: e.message,
                            showCloseButton: true,
                            showCancelButton: false,
                            focusConfirm: false,
                            confirmButtonText: 'OK'
                        })
                    } else {
                        Swal.fire({
                            title: 'Berhasil',
                            icon: 'success',
                            html: 'Template Email Berhasil Dihapus',
                            showCloseButton: true,
                            showCancelButton: false,
                            focusConfirm: false,
                            confirmButtonText: 'OK'
                        })
                        dataTable.ajax.reload(null, false);
                    }
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
                }
            })
        }
    })
}


