function IndexViewLink(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Portal/LinkFasilitasMahasiswa/ModalDetailLinkMahasiswa/' + id,
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
            //alert("aaa");
            //var a = e.LinkTeams;
            //console.log(e);
           // alert(e);
        }, error: function (e) {
            console.log(e);
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