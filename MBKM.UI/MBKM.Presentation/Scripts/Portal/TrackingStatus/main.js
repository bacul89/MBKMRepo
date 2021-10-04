var table = $('#table-data-tracking-pendaftaran-makul').DataTable({
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
                                <button type="button" onclick="urlLinkDetailCPMKP()" class="btn btn-warning btn-sm" style="font-size: 0.5vw"><i class="fas fa-search"></i></button>
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

function OpenModal() {
    $('#DetailTrackingStatus').modal('show');
}