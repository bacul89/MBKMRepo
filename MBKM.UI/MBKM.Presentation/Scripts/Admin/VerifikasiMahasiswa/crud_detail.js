var dataVerifikasi = {}

$(document).ready(function () {
    $("#inp_noKerjaSama").change(function () {
        $('input[name=inp_biaya]').focus();
    })


    var biayaAwal = $('input[name=inp_biaya]').val();
    var textNoKerjasamaAwal = $('select[name="inp_noKerjaSama"] option').filter(':selected').text()
    $('#inp_statusKerjaSama').change(function () {
        if ($('select[name="inp_statusKerjaSama"] option').filter(':selected').val().includes("TIDAK")) {
            $('#inp_noKerjaSama')
                .find('option')
                .remove()
                .end();

            $('input[name=inp_biaya]').val(0).prop('disabled', true);
            $('select[name="inp_noKerjaSama"]').prop('disabled', true);
        } else if ($('select[name="inp_statusKerjaSama"] option').filter(':selected').val().includes("ADA KERJASAMA")) {
            var option = $("<option selected='selected'></option>").val(textNoKerjasamaAwal).text(textNoKerjasamaAwal);
            $("#inp_noKerjaSama").append(option).trigger('change');
            if ($('#editVerifikasiButton').hasClass("hidden")) {
                $('input[name=inp_biaya]').val(0).prop('disabled', false);
                $('select[name="inp_noKerjaSama"]').prop('disabled', false);
            }
            $('input[name=inp_biaya]').val(biayaAwal);
        }
        else {
            var option = $("<option selected='selected'></option>").val(textNoKerjasamaAwal).text(textNoKerjasamaAwal);
            $("#inp_noKerjaSama").append(option).trigger('change');
            if ($('#editVerifikasiButton').hasClass("hidden")) {
                $('input[name=inp_biaya]').val(0).prop('disabled', false);
                $('select[name="inp_noKerjaSama"]').prop('disabled', false);
            }
            $('input[name=inp_biaya]').val(0);
        }
    })
    
    $('#inp_noKerjaSama').change(function () {
            $.ajax({
                url: '/Admin/VerifikasiMahasiswa/GetDataBiaya',
                data: {
                    id: $('select[name="inp_noKerjaSama"] option').filter(':selected').val()
                },
                dataType: 'json',
                type: 'post',
                success: function (w) {
                    $('input[name=inp_biaya]').val(w);
                    biayaAwal = w;
                    $('#inp_biaya').focus();
                }
            })
    });
    
})

function getValueOnForm() {
    dataVerifikasi.StatusKerjasama = $('select[name="inp_statusKerjaSama"] option').filter(':selected').val()
    if ($('select[name="inp_statusKerjaSama"] option').filter(':selected').val().includes("TIDAK")) {
        dataVerifikasi.NoKerjasama = null
    } else {
        dataVerifikasi.NoKerjasama = $('select[name="inp_noKerjaSama"] option').filter(':selected').text()
    }
    dataVerifikasi.FlagBayar = $("input[name=inp_pembayaran]:checked").val();

    var biayaTmp = $('input[name=inp_biaya]').val();
    if (biayaTmp != 0) {
        var biayaT1 = biayaTmp.split(" ");
        var biayaT2 = biayaT1[1].split(",");
        var final = biayaT2[0].replace(/\./g, "");
    } else {
        var final = biayaTmp;
    }
    
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
    } else if ($("input[name=inp_verifikasi]:checked").val().includes("DITOLAK")) {
        return true;
    } else if (!$("input[name=inp_pembayaran]:checked").val()) {
        return false;
    } else if (!$('select[name="inp_approval"] option').filter(':selected').val()) {
        return false;
    }
    else {
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
                if (e.status == 200) {
                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: 'Mahasiswa Berhasil Terverifikasi',
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.href = '/Admin/VerifikasiMahasiswa/'
                        }
                    })
                } else if (e.status == 300) {
                    Swal.fire({
                        title: 'Oppss',
                        icon: 'error',
                        html: e.message,
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                } else if (e.status == 500) {
                    Swal.fire({
                        title: 'Oppss',
                        icon: 'error',
                        html: e.message,
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.href = '/Admin/VerifikasiMahasiswa/'
                        }
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
            icon: 'warning',
            html: 'Masukkan Status Verifikasi, Status Pembayaran dan Tujuan Approval!',
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