var dataVerifikasi = {}

$(document).ready(function () {
    $('#inp_noKerjaSama').change(function () {
        $.ajax({
            url: '/Admin/VerifikasiMahasiswa/GetDataBiaya',
            data: {
                id: $('select[name="inp_noKerjaSama"] option').filter(':selected').val()
            },
            dataType: 'json',
            type: 'post',
            success: function (w) {
                $('input[name=inp_biaya]').val(w.BiayaKuliah);
                $('#inp_biaya').focus();
            }
        })
    });
})

function getValueOnForm() {
    dataVerifikasi.StatusKerjasama = $('select[name="inp_statusKerjaSama"] option').filter(':selected').val()
    dataVerifikasi.NoKerjasama = $('select[name="inp_noKerjaSama"] option').filter(':selected').text()

    var biayaTmp = $('input[name=inp_biaya]').val();
    var biayaT1 = biayaTmp.split(" ");
    var biayaT2 = biayaT1[1].split(",");
    var final = biayaT2[0].replace(/\./g, "");
    dataVerifikasi.BiayaKuliah = final;

    var StatusVerifikasi = $('input[name=inp_verifikasi]:checked').val();
    if (StatusVerifikasi) {
        dataVerifikasi.StatusVerifikasi = StatusVerifikasi;
    }

    dataVerifikasi.Approval = $('select[name="inp_approval"] option').filter(':selected').val()
    dataVerifikasi.Catatan = $('textarea[name=inp_catatan]').val();
}

function ValidationStatusApproval() {
    if (!$("input[name=inp_verifikasi]:checked").val()) {
        return false;
    } else {
        return true;
    }
}

function PostDataUpdate() {
    $.LoadingOverlay("show");
    if (ValidationStatusApproval()) {
        getValueOnForm();
        var url = window.location.href;
        dataVerifikasi.ID = url.substring(url.lastIndexOf('/') + 1);
        console.log(dataVerifikasi);
        $.ajax({
            url: '/Admin/VerifikasiMahasiswa/PostDataUpdate',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dataVerifikasi),
            contentType: 'application/json',
            success: function (e) {
                $.LoadingOverlay("hide");
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'Mahasiswa Berhasil Terverifikasi',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                window.location.href = '/Admin/VerifikasiMahasiswa/'
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
            icon: 'warning',
            html: 'Masukkan Status Verifikasi !',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    }
}


function DownloadAttachment(dataID) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/VerifikasiMahasiswa/DownloadFile',
        type: 'get',
        datatype: 'json',
        data: {
            id : dataID
        },
        contentType: 'application/json',
        success: function (e) {
            $.LoadingOverlay("hide");
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Mahasiswa Berhasil Terverifikasi',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            window.location.href = '/Admin/VerifikasiMahasiswa/'
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