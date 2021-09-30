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
                dataTable.ajax.reload(null, false);
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
                location.reload();
                dataTable.ajax.reload(null, false);
            }
        });
    })

}