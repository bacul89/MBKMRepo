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
                $("#update-button").click(function () {
                    //var latest_valueKJ = $("option:selected:first", "#noKJ").text();
                    // $("#noKJ").val(latest_valueKJ);
                    //alert(latest_valueKJ);
                    UpdateLink(id);
                });
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
function UpdateLink(id) {
    dMasterLink = {}
   
   
    
    dMasterLink.ID = id;
    dMasterLink.LinkMoodle = $("#inp_moodle").val();
    dMasterLink.LinkAtmaZeds = $("#inp_zeds").val();
    dMasterLink.LinkTeams = $("#inp_teams").val();
    dMasterLink.LinkOthers = $("#inp_other").val();
   
    console.log(dMasterLink);
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/LinkFasilitasAdmin/UpdateLink',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(dMasterLink),
        contentType: 'application/json',
    }).then(function (response) {
        if (response.status == 200) {
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Link Berhasil DiUpdate',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            $('.modal').modal('hide');
            tableUser.ajax.reload(null, false);


        }
    });


}