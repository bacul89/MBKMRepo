var input = {}
function getValueOnForm() {
    input.NoPerjanjian = $("#NoPerjanjian").val();
    input.TanggalMulai = $("#TanggalMulai").val();
    input.TanggalAkhir = $("#TanggalAkhir").val();
    input.Instansi = $("#instansi").val();
    input.NamaInstansi = $("#NamaUniversitas").val();
    input.NamaUnit = $("#Namaunit").val();
    input.JenisPertukaran = $("#JenisPertukaran").val();
    input.JenisKerjasama = $("#JenisKerjasama").val();
    input.CreatedBy = $("#inputer").val();
    input.BiayaKuliah = $("#biaya").val();
}
function IndexCreateKerjasama() {
    if ($('#created-Kerjasama').length) {
        $('#TambahKerjasama').modal('show');
    } else {
        $.ajax({
            url: '/Admin/PerjanjianKerjasama/ModalCreatePerjanjianKerjasama',
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
function SubmitPerjanjian() {
    //perjanjian = new Object;
    //perjanjian.NoPerjanjian = $("#NoPerjanjian").val();
    //perjanjian.TanggalAkhir = $("#TanggalAkhir").val();
    //perjanjian.TanggalMulai = $("#TanggalMulai").val();
    //perjanjian.CreatedBy = $("#inputer").val();
    //perjanjian.UpdatedBy = $("#inputer").val();
    //perjanjian.NamaInstansi = $("#NamaUniversitas").val();
    //perjanjian.NamaUnit = $("#Namaunit").val();
    //perjanjian.JenisPertukaran = $(".pertukaran").val();
    //perjanjian.JenisKerjasama = $(".kerjasama").val();
    //perjanjian.BiayaKuliah = $("#biaya").val();
    //perjanjian.Instansi = $("#instansi").val();
    debugger;
    var data = new FormData();
    //var files1 = $("#file").get(0).files;
    var fileInput = document.getElementById('file');
    //Iterating through each files selected in fileInput  
    for (i = 0; i < fileInput.files.length; i++) {

        var sfilename = fileInput.files[i].name;
        data.append("file", fileInput.files[i]);

    }  

    //if (files1.length > 0) {
    //    data.append("file", files1[0]);

    //}
    console.log(data);
    $.ajax({
        type: "POST",
        url: "/Admin/PerjanjianKerjasama/SavePerjanjian",
        cache: false,
        contentType: false,
        processData: false,
        data: data,
        success: function (response) {
            console.log("coba insert")
        }
    });
}
function IndexViewKerjasama(id) {
    $.ajax({
        url: '/Admin/PerjanjianKerjasama/ModalDetailKerjasama/' + id,
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

function UpdateKerjasama(id) {
    $.ajax({
        url: '/Admin/PerjanjianKerjasama/ModalUpdateKerjasama/' + id,
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
//function UpdatePerjanjian() {
//    data = new Object;
//    data.ID = $("idKerjasama").val();
//    data.NoPerjanjian = $("#NoPerjanjian").val();
//    data.TanggalAkhir = $("#TanggalAkhir").val();
//    data.TanggalMulai = $("#TanggalMulai").val();
//    data.UpdatedBy = $("#inputer").val();
//    data.NamaInstansi = $("#NamaUniversitas").val();
//    data.NamaUnit = $("#Namaunit").val();
//    data.JenisPertukaran = $("#JenisPertukaran").val();
//    data.JenisKerjasama = $("#JenisKerjasama").val();
//    data.BiayaKuliah = $("#biaya").val();
//    data.Instansi = $("#instansi").val();
//    $.ajax({
//        type: "POST",
//        url: "/Admin/PerjanjianKerjasama/UpdateKerjasama",
//        data: data,
//        success: function (response) {
//            console.log("coba update")
//        }
//    });
//}
function UpdatePerjanjian() {
    getValueOnForm();
    input.ID = $("#idKerjasama").val();
    console.log(input);
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/PerjanjianKerjasama/UpdateKerjasama',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(input),
        contentType: 'application/json',
        success: function (e) {
            console.log("berhasil")
        }
    })
}