function AddCPLTemplate() {
   // if ($('#created-cpl-template').length) {
      //  $('#TambahCPL').modal('show');
    //} else {
        $.ajax({
            url: '/Admin/MasterCPL/ModaladdCPL',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                console.log(e);
               // if ($('.data-content-modal').length) {
                 //   $('.data-content-modal').remove();
               // }
               // $('#modal-inner').append(e);
               // $('.modal').modal('show');
            }
        })
    }
}