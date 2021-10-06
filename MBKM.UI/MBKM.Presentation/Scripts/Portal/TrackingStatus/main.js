﻿var table = $('#table-data-tracking-pendaftaran-makul').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": {
        url: '/Portal/TrackingStatusPendaftaran/GetPendaftaranMakul/',
        data: { emailMahasiswa: $('#getEmail').val()},
        type: 'POST'
    },
    "language": {
        "emptyTable": "No record found.",
        "processing": '<div style="padding-top:30px;"><i class="fa fa-spinner fa-spin fa-3x fa-fw" ></i><span class="sr-only" style="color:#2a2b2b;">Loading...</span></div> ',
        "search": "",
        "searchPlaceholder": "Search..."
    },
    "columns": [
        {
            "title": "Action",
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="center vertical-center" style="text-align:center; align-items:center">
                            <a href="javascript:void()">
                                <button type="button" onclick="urlLinkDetailCPMKP('${data}')" class="btn btn-warning btn-sm" style="font-size: 0.5vw"><i class="fas fa-search"></i></button>
                            </a>
                        </div>`;
            }
        },
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + 1) + '</div>';
            }
        },
        {
            "title": "Fakultas",
            "data": "JadwalKuliahs.NamaFakultas",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Program Studi",
            "data": "JadwalKuliahs.NamaProdi",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Kode Mata Kuliah",
            "data": "JadwalKuliahs.KodeMataKuliah",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Nama Mata Kuliah Dituju",
            "data": "JadwalKuliahs.NamaMataKuliah",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Nama Dosen",
            "data": "JadwalKuliahs.NamaDosen",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Status Pendaftaran",
            "data": "StatusPendaftaran",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
    ],
    "createdRow": function (row, data, index) {
        $('td', row).css({
            'border': '1px solid coral',
            'border-collapse': 'collapse',
            'vertical-align': 'center',
        });
    },
});

function urlLinkDetailCPMKP(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Portal/TrackingStatusPendaftaran/_getIndexDetailView/' + id,
        type: 'post',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            console.log($('#statusDisable').val());
            if ($('#statusDisable').val()) {
                $('#linkDetail').attr("onclick", "").unbind("click");
                $('.onOff').attr('disabled', true)
            }
            $('.modal').modal('show');
        }, error: function (e) {
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
            $('.modal').modal('hide');
        }
    })
}

function redirectToDetail(id) {
    window.location.href = "/Portal/TrackingStatusPendaftaran/IndexDetailPendaftaran/" + id;
}