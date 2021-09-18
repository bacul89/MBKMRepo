var table = $('#TableList').DataTable({
    "proccessing": true,
    "serverSide": true,
    "ajax": {
        url: '/Admin/PerjanjianKerjasama/GetListPKGrid',
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
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + 1) + '</div>';
            }
        },
        {
            "title": "No Kerjasama",
            "data": "NoKerjasama",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Tanggal Mulai",
            "data": "TanggalMulai",
            "render": function (data, type, row, meta) {
                if (data === null) return "";
                return moment(data).format('DD/MM/YYYY');
            }
        },
        {
            "title": "Tanggal Akhir",
            "data": "TanggalAkhir",
            "render": function (data, type, row, meta) {
                if (data === null) return "";
                return moment(data).format('DD/MM/YYYY');
            }
        },
        {
            "title": "Instansi",
            "data": "Instansi",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Nama Unit",
            "data": "NamaUnit",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Nama Instansi",
            "data": "NamaInstansi",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Jenis Pertukaran",
            "data": "JenisPertukaran",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Jenis Kerjasama",
            "data": "JenisKerjasama",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Inputer",
            "data": "Inputer",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Action",
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="UpdateKerjasama('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewKerjasama('${data}')"> <i class="fas fa-file-search coral"></i></a>
                            </div>
                        </div>`;
            }
        }
    ],
    "createdRow": function (row, data, index) {
        $('td', row).css({
            'border': '1px solid coral',
            'border-collapse': 'collapse',
            'vertical-align': 'center',
        });
    },
});