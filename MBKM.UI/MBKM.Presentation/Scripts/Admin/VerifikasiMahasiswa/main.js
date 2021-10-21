var table = $('#table-data-verifikasi-mahasiswa').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": {
        url: '/Admin/VerifikasiMahasiswa/GetAllMahasiswa',
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
                                <button type="button" onclick="IndexDetailVerifikasiMahasiswa('${data}')" class="btn btn-warning btn-sm" style="font-size: 0.5vw"><i class="fas fa-search"></i></button>
                            </a>
                        </div>`;
            }
        },
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
            }
        },
        {
            "title": "Universitas Asal",
            "data": "NamaUniversitas",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Jenjang Studi",
            "data": "JenjangStudi",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Program Studi Asal",
            "data": "ProdiAsal",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "NIM Asal",
            "data": "NIMAsal",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Nama Mahasiswa",
            "data": "Nama",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Jenis Kelamin",
            "data": "Gender",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Email",
            "data": "Email",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "No. Hp",
            "data": "NoHp",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Status Verifikasi",
            "data": "StatusVerifikasi",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                }
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

function IndexDetailVerifikasiMahasiswa(id) {
    var baseUrl = window.location.href;
    console.log(baseUrl);
    window.location.href = baseUrl + "/IndexDetailMahasiswa/" + id
}
