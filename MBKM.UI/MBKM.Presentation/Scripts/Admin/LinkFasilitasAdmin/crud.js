function IndexUpdateLinkFasilitas(id) {

    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/LinkFasilitasAdmin/ModalUpdateLinkFasilitas/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
            /*$.LoadingOverlay("hide");*/
            /*console.log(e);*/
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            $("#update-button").hide();
            //loadControl('Edit');
            $("#edit-button").click(function () {
                $("#inp_moodle").removeAttr("disabled");
                $("#inp_zeds").removeAttr("disabled");
                $("#inp_teams").removeAttr("disabled");
                $("#inp_other").removeAttr("disabled");
                $("#edit-button").hide();
                $("#update-button").show();
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