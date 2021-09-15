var table = $('#table-data-verifikasi-mahasiswa').DataTable({
    "proccessing": true,
    "serverSide": true,
    "ajax": {
        url: '/Admin/VerifikasiMahasiswa/GetAllMahasiswa',
        type: 'POST'
    },
    "language": {
        "emptyTable": "No record found.",
        "processing":
            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
        "search": "",
        "searchPlaceholder": "Search..."
    },
    "columns": [
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        {
            "title": "Universitas Asal",
            "data": "Universitas",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "Jenjang Studi",
            "data": "Jenjang",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "Program Studi Asal",
            "data": "Prodi",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "NIM Asal",
            "data": "NIM",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "Nama Mahasiswa",
            "data": "Nama",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "Jenis Kelamin",
            "data": "Gender",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "Email",
            "data": "Email",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "No. Hp",
            "data": "HP",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "Status Verifikasi",
            "data": "StatusVerifikasi",
            "render": function (data, type, row, meta) {
                if (data) {
                    return '<div class="center">Aktif</div>';
                } else {
                    return '<div class="center">Tidak Aktif</div>';
                }

            }
        },
        {
            "title": "Action",
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<a href="javascript:void()">
                            <button type="button" class="btn btn-warning btn-sm"><i class="fas fa-search coral"></i></button>
                        </a>`;
            }
        }
    ],
    "createdRow": function (row, data, index) {
        $('td', row).css({
            'border': '1px solid coral',
            'border-collapse': 'collapse',
            'vertical-align': 'center',
        });
    }
});