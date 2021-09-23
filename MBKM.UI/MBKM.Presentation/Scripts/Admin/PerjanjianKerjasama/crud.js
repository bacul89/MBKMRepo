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
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Data Berhasil Ditambahkan',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            table.ajax.reload(null, false),
                $('.modal').modal('hide');
        },
        error: function (response) {
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
    });
}
//function SubmitPerjanjian() {
//    var data = new FormData($('#createPerjanjian')[0]);
//    var fileInput = document.getElementById('file');
//    for (i = 0; i < fileInput.files.length; i++) {
//        var sfilename = fileInput.files[i].name;
//        data.append("file", fileInput.files[i]);
//    }

//    $.ajax({
//        url: base_url + '/Admin/PerjanjianKerjasama/SavePerjanjian',
//        type: 'post',
//        dataType: 'json',
//        data: JSON.stringify(data),
//        contentType: 'application/json',
//        success: function (e) {
//            console.log("cek")
//        }
//    });
//}
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
    var data = new FormData($('#UpdatePerjanjian')[0]);
    var fileInput = document.getElementById('file');
    for (i = 0; i < fileInput.files.length; i++) {
        var sfilename = fileInput.files[i].name;
        data.append("file", fileInput.files[i]);
    }
    data.append("ID", $("#idKerjasama").val())
    $.ajax({
        type: "POST",
        url: "/Admin/PerjanjianKerjasama/UpdateKerjasama",
        cache: false,
        contentType: false,
        processData: false,
        data: data,
        success: function (e) {
            console.log("coba isi")
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Data Berhasil Diubah',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            tableUser.ajax.reload(null, false);
            $('.modal').modal('hide');
        },
        error: function (e) {
            console.log("coba lagi")
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