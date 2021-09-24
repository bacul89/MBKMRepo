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
//function SubmitPerjanjian() {
//    var data = new FormData($('#createPerjanjian')[0]);
//    var fileInput = document.getElementById('file');
//    for (i = 0; i < fileInput.files.length; i++) {
//        var sfilename = fileInput.files[i].name;
//        data.append("file", fileInput.files[i]);
//    }
//    if (validationCustom2()) {
//        var base_url = window.location.origin;
//        $.ajax({
//            url: base_url + "/Admin/PerjanjianKerjasama/SavePerjanjian",
//            type: 'post',
//            datatype: 'json',
//            data: JSON.stringify(data),
//            //contentType: 'application/json'
//            contentType: 'application/json',
//        }).then(function (response) {
//            if (response.status == 400) {
//                Swal.fire({
//                    title: 'Gagal!',
//                    icon: 'error',
//                    html: 'EMAIL atau NIP telah terdaftar!',
//                    showCloseButton: true,
//                    showCancelButton: false,
//                    focusConfirm: false,
//                    confirmButtonText: 'OK'
//                })
//                tableUser.ajax.reload(null, false);
//                $('.modal').modal('hide');
//            } else
//                if (response.status == 200) {
//                    Swal.fire({
//                        title: 'Berhasil',
//                        icon: 'success',
//                        html: 'User Baru Berhasil Ditambahkan',
//                        showCloseButton: true,
//                        showCancelButton: false,
//                        focusConfirm: false,
//                        confirmButtonText: 'OK'
//                    })
//                    tableUser.ajax.reload(null, false);
//                    $('.modal').modal('hide');
//                }
//        });
//    } else {
//        Swal.fire({
//            title: 'Oppss',
//            icon: 'warning',
//            html: 'Ada beberapa field yang belum kamu isikan',
//            showCloseButton: true,
//            showCancelButton: false,
//            focusConfirm: false,
//            confirmButtonText: 'OK'
//        })
//    }
//}


function SubmitPerjanjian() {
    var data = new FormData($('#createPerjanjian')[0]);
    var fileInput = document.getElementById('file');
    for (i = 0; i < fileInput.files.length; i++) {
        var sfilename = fileInput.files[i].name;
        data.append("file", fileInput.files[i]);
    }
    if (fileInput.size < 1042157) {
        $.ajax({
            type: "POST",
            url: "/Admin/PerjanjianKerjasama/SavePerjanjian",
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            success: function (response) {
                location.reload();
                Swal.fire({
                    title: 'Oppss',
                    icon: 'error',
                    html: 'Coba Reload Page',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                table.ajax.reload(null, false),
                    $('.modal').modal('hide');
            },
            error: function (response) {
                location.reload();
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
            }

        })
    } else {
        alert("File Upload Lebih dari 1MB");
        location.reload();
    }
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

function showFileSize() {
    let file = document.getElementById("file").files[0];
    if (file) {
        alert(file.size + " in bytes");
        location.reload();
    } else {
        alert("select a file... duh");
    }
}