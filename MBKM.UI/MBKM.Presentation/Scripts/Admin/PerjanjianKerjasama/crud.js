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
function DeletedFiles(id) {

    Swal.fire({
        title: "Apakah anda yakin?",
        text: "warning",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }).then((result) => {
        $.ajax({
            url: '/Admin/PerjanjianKerjasama/PostDeleteFile',
            type: "POST",
            data: { id: id }
            ,
            dataType: "json",
            success: function () {
                console.log("benar");

            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log("salah");
            }
        });
    })

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
    var TanggalMulai = $("#TanggalMulai").val();
    var TanggalAkhir = $("#TanggalAkhir").val();
    if (TanggalAkhir < TanggalMulai) {
        Swal.fire({

            title: 'Tanggal Error',
            icon: 'error',
            html: 'Tanggal Akhir tidak boleh lebih besar dari tanggal mulai',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
        return;
    }
    var data = new FormData($('#createPerjanjian')[0]);
    var fileInput = document.getElementById('file');
    for (i = 0; i < fileInput.files.length; i++) {
        var sfilename = fileInput.files[i].name;
        var filesize = fileInput.files[i].size / 1024 / 1024;
        if (filesize <= 3) {
            data.append("file", fileInput.files[i]);
        }
        else {
            Swal.fire({

                title: 'File Size Error',
                icon: 'error',
                html: 'File yang terupload lebih dari 3MB',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            return;
        }
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
            location.reload();
        },
        error: function (response) {
            Swal.fire({
                title: 'Oppss',
                icon: 'error',
                html: 'Data Gagal Ditambahkan, Periksa Field dan Ukuran File atau inputan lain',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            //table.ajax.reload(null, false),
            //    $('.modal').modal('hide');
            //location.reload();
        }

    })
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
        var filesize = fileInput.files[i].size / 1024 / 1024;
        if (filesize <= 3) {
            data.append("file", fileInput.files[i]);
        }
        else {
            Swal.fire({

                title: 'File Size Error',
                icon: 'error',
                html: 'File yang terupload lebih dari 3MB',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            return;
        }
    }
    data.append("ID", $("#idKerjasama").val());
    data.append("JenisPertukaran", $("#JenisPertukaran").val());
    data.append("JenisKerjasama", $("#JenisKerjasama").val());
    var TanggalAkhir = $("#tanggalAkhir").val();
    var TanggalMulai = $("#tanggalMulai").val();
    if (TanggalAkhir < TanggalMulai) {
        Swal.fire({

            title: 'Tanggal Error',
            icon: 'error',
            html: 'Tanggal Akhir tidak boleh lebih besar dari tanggal mulai',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
        return;
    }
    
    $.ajax({
        type: "POST",
        url: "/Admin/PerjanjianKerjasama/UpdateKerjasama",
        cache: false,
        contentType: false,
        processData: false,
        data: data,
        success: function (response) {
            Swal.fire({                
                title: 'Berhasil',
                icon: 'success',
                html: 'Data Berhasil Diupdate',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            table.ajax.reload(null, false),
                $('.modal').modal('hide');
            location.reload();
        },
        error: function (response) {
            Swal.fire({
                title: 'Oppss',
                icon: 'error',
                html: 'Data Gagal Diupdate',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            table.ajax.reload(null, false),
                $('.modal').modal('hide');
            //location.reload();
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