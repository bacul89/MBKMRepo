var answer = {};
var listAnswer = [];
function CustomValidationCheck() {
    var i = 1;
    var hasil;
    $('.data_radio').each(function () {
        hasil = 0;

        if ($('#inp_kritik').val() == "") {
            hasil = false;
            return hasil
        }

        if ($('input[name=' + i + ']:checked').val() == null) {
            hasil = false;
            return hasil
        } else {
            hasil = true;
        }
        i++;
    });
    return hasil;
}


function postDataQuitionare() {
    $.LoadingOverlay("show");
    var listAnswer = [];
    var i = 1;
    var checkData = {};
    if (CustomValidationCheck()) {
        checkData.dosenID = $('#inp_dosenID').val();
        checkData.jadwalID = $('#inp_jadwalID').val();
        $.ajax({
            url: '/Portal/FeedBackMatakuliah/CheckDosenAnswer',
            data: JSON.stringify(checkData),
            type: 'post',
            contentType: 'application/json',
            dataType: 'json',
            success: function (e) {
                if (e.status == 200) {
                    $('.data_radio').each(function () {
                        var answer = {};
                        answer.nilai = $('input[name=' + i + ']:checked').val();
                        answer.data = $('input[name=' + i + ']:checked').attr('id');
                        answer.kritik = $('#inp_kritik').val();
                        answer.dosenID = $('#inp_dosenID').val();
                        answer.namaDosen = $('#inp_namaDosen').val();
                        answer.JadwalKuliahID = $('#inp_jadwalID').val();
                        listAnswer.push(answer);
                        i++;
                    });
                    $.ajax({
                        url: '/Portal/FeedBackMatakuliah/PostDataAnswer',
                        data: JSON.stringify({ 'answer': listAnswer }),
                        type: 'post',
                        contentType: 'application/json',
                        dataType: 'json',
                        success: function (e) {
                            $.LoadingOverlay("hide");
                            if (e.status == 200) {
                                Swal.fire({
                                    title: 'Berhasil',
                                    icon: 'success',
                                    html: e.message,
                                    showCloseButton: true,
                                    showCancelButton: false,
                                    focusConfirm: false,
                                    confirmButtonText: 'OK'
                                })
                                window.location.href = "/Portal/FeedBackMatakuliah/"
                            } else {
                                $.LoadingOverlay("hide");
                                Swal.fire({
                                    title: 'Oppss',
                                    icon: 'error',
                                    html: 'Terjadi Kesalahan',
                                    showCloseButton: true,
                                    showCancelButton: false,
                                    focusConfirm: false,
                                    confirmButtonText: 'OK'
                                })
                            }
                        },
                        error: function (e) {
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
                        }
                    })
                } else {
                    $.LoadingOverlay("hide");
                    Swal.fire({
                        title: 'Oppss',
                        icon: 'error',
                        html: e.message,
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                }
            },
            error: function (e) {
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
            }
        })
    }
    else {
        $.LoadingOverlay("hide");
        Swal.fire({
            title: 'Oppss',
            icon: 'error',
            html: 'Mohon isi seluruh pertanyaan!',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    };
   

}


function NextDosenFeedback() {
    $.LoadingOverlay("show");
    var tmpData = {};
    tmpData.listDosen = $('#inp_listDosen').val();
    tmpData.urutan = $('#inp_urutan').val();
    tmpData.idJadwalKuliah = $('#inp_jadwalID').val();
    $.ajax({
        url: '/Portal/FeedBackMatakuliah/NextPageFeedBack',
        data: JSON.stringify(tmpData),
        type: 'post',
        contentType: 'application/json',
        dataType: 'html',
        success: function (e) {
            $('.data_masukk').remove();
            $('#modal-inner').append(e);
            $.LoadingOverlay("hide");
        },
        error: function (e) {
            $.LoadingOverlay("hide");
            Swal.fire({
                title: 'Oppss',
                icon: 'error',
                html: 'Sudah Paling Akhir',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })

        }

    })
}


function PreviousDosenFeedback() {
    $.LoadingOverlay("show");
    var tmpData = {};
    tmpData.listDosen = $('#inp_listDosen').val();
    tmpData.urutan = $('#inp_urutan').val();
    tmpData.idJadwalKuliah = $('#inp_jadwalID').val();
    $.ajax({
        url: '/Portal/FeedBackMatakuliah/PreviousPageFeedback',
        data: JSON.stringify(tmpData),
        type: 'post',
        contentType: 'application/json',
        dataType: 'html',
        success: function (e) {
            $('.data_masukk').remove();
            $('#modal-inner').append(e);
            $.LoadingOverlay("hide");
        },
        error: function (e) {
            $.LoadingOverlay("hide");
            Swal.fire({
                title: 'Oppss',
                icon: 'error',
                html: 'Sudah Paling Awal',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })

        }
    })
}