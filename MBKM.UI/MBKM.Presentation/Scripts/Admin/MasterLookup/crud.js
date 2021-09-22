
var dMasterLookup = {}

function getValueOnForm() {
    dMasterLookup.Tipe = $('input[name=inp_tipe]').val();
    dMasterLookup.Nama = $('input[name=inp_nama]').val();
    dMasterLookup.Nilai = $('input[name=inp_nilai]').val();
    dMasterLookup.IsActive = $('input[name=inp_status]:checked').val();
}

function IndexCreateMasterLookup() {
    if ($('#created-master-lookup').length) {
        $('#TambahMasterLookUp').modal('show');
    } else {
        $.ajax({
            url: '/Admin/MasterLookup/ModalCreateMasterLookup',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                /*console.log(e);*/
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

function IndexUpdateMasterLookup(id) {
    $.ajax({
        url: '/Admin/MasterLookup/ModalUpdateMasterLookup/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            /*console.log(e);*/
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

function IndexViewMasterLookup(id) {
    $.ajax({
        url: '/Admin/MasterLookup/ModalDetailMasterLookup/' + id,
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

function PostCreate() {
    dMasterLookup = {}
    getValueOnForm();

    /*console.log(dMasterLookup);*/
    if (validationCustom()) {
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/MasterLookup/PostDataMasterLookup',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dMasterLookup),
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
    dMasterLookup = {}
    getValueOnForm();
    dMasterLookup.ID = $('#id_MasterLookup').val();
   /* console.log(dMasterLookup);*/
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/MasterLookup/PostUpdateMasterLookup',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(dMasterLookup),
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

function DeletedMasterLookup(idLookup) {

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
            url:'/Admin/MasterLookup/PostDeleteMasterLookup',
            type: "POST",
            data: { id: idLookup}
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
