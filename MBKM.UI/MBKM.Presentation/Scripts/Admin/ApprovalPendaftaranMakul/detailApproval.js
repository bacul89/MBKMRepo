$(document).ready(function (e) {
    $('.js-example-basic-single').select2({
        placeholder: "Masukkan Nama Dosen",
        "proccessing": true,
        "serverSide": true,
        ajax: {
            url: '/Admin/ApprovalPendaftaranMatakuliah/GetAllDataDosen/',
            type: 'post',
            dataType: 'json',
            data: function (params) {
                return {
                    search: params.term,
                    length: params.length || 10,
                    skip: params.skip || 0
                };
            },
            processResults: function (data, page) {
                return {
                    results: data
                }
            }
        }
    });
})
dCPL = {};
dApproval = {};
dMahasiswa = {};

function AcceptedPendaftaranEksternal() {
    $.LoadingOverlay("show");
    dApproval.Kesenjangan = $('#inp_kesenjangan').val();
    dApproval.Hasil = $('#inp_hasil').val();
    dApproval.Konversi = $('select[name="inp_konversi"] option').filter(':selected').val()
    dApproval.DosenID = $('select[name="inp_dosbing"] option').filter(':selected').val()
    dApproval.DosenPembimbing = $('select[name="inp_dosbing"] option').filter(':selected').text()
    dMahasiswa.Catatan = $('#inp_catatan').val();
    dApproval.mahasiswas = dMahasiswa;
    dCPL.ID = $('#inp_ID').val();
    dCPL.PendaftaranMataKuliahs = dApproval;
    $.ajax({
        url: '/Admin/ApprovalPendaftaranMatakuliah/PostDataApprovalAccepted/',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(dCPL),
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
                        window.location.href = '/Admin/ApprovalPendaftaranMatakuliah/'
                    }
                })
            } else {
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
        },
        error: function (e) {
            $.LoadingOverlay("hide");
            Swal.fire({
                title: 'Oppss',
                icon: 'error',
                html: 'Terjadi Kesalahan dalam Proses',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
        }
    })
}