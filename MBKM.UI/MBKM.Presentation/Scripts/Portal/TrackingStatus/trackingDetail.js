function AcceptedCPLPendaftaran() {
    $.LoadingOverlay("show");
    id = $('#inp_ID').val();
    $.ajax({
        url: '/Portal/TrackingStatusPendaftaran/PostApprovalAccepted/',
        type: 'post',
        datatype: 'json',
        data: {
            id : $('#inp_ID').val()
        },
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
                        window.location.href = '/Portal/TrackingStatusPendaftaran/'
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

function RejectCPLPendaftaran() {
    swal.fire({
        title: "Apakah anda yakin?",
        type: "warning",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Rejected!",
        closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {
            $.LoadingOverlay("show");
            id = $('#inp_ID').val();
            $.ajax({
                url: '/Portal/TrackingStatusPendaftaran/PostApprovalRejected/',
                type: 'post',
                datatype: 'json',
                data: {
                    id: $('#inp_ID').val()
                },
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
                                window.location.href = '/Portal/TrackingStatusPendaftaran/'
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
    })
}