function DetailMhs(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/DaftarSeluruhMahasiswa/ModalDetailMhs/' + id,
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
            $("#Update").hide();
            $("#Edit").click(function () {
                //$("input").removeAttr("disabled");
                //$("select").removeAttr("disabled");
                //$("textarea").removeAttr("disabled");
                //$("#simpan").show();
                $("#Update").show();
                //loadNoKerjasama();
                $("#Edit").hide();
                //$("#statusKJ").removeAttr("disabled");
                //$("#noKJ").removeAttr("disabled");

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
//function loadNoKerjasama() {
//    $("#noKerjasama").select2({
//        placeholder: "-- Pilih Nomor Kerjasama --",
//        width: "100%",
//        ajax: {
//            url: '/Admin/DaftarseluruhMahasiswa/GetNoKerjasama',
//            dataType: 'json',
//            method: "POST",
//            delay: 250,
//            cache: false,
//            data: function (params) {
//                return {
//                    Length: "10",
//                    Search: params.term || "",
//                    Skip: params.page - 1 || 0,
//                    NamaInstansi: $('#namaUniversitas').val()
//                };
//            },
//            processResults: function (data, params) {
//                var page = params.page || 1;
//                return {
//                    results: $.map(data, function (item) { return { id: item.NoKerjasama, value: item.NoKerjasama, text: item.NoKerjasama } }),
//                    pagination: {
//                        more: (page * 10) <= data.length
//                    }
//                };
//            },
//        }
//    });
//}