function AddCPLTemplate() {
    if ($('#created-cpl-template').length) {
        $('#TambahCPL').modal('show');
    } else {
        $.ajax({
            url: '/Admin/MasterCPL/ModaladdCPL',
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
function UpdateMasterCPL(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterCpl/ModalUpdateMasterCpl/' + id,
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
function DetailMasterCPL(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterCPL/ModalDetailMasterCpl/' + id,
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