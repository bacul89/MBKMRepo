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
    var data = new FormData($('#createPerjanjian')[0]);
    var fileInput = document.getElementById('file');
    for (i = 0; i < fileInput.files.length; i++) {
        var sfilename = fileInput.files[i].name;
        data.append("file", fileInput.files[i]);
    }
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