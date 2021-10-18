var url = window.location.href;
var jsData = {};
tempId = url.substring(url.lastIndexOf('/') + 1);

var table = $('#table-data-list-mahasiswa').DataTable({
    "ajax": {
        url: '/Admin/JadwalUjian/GetDataTableMahasiswa/',
        data: {
            dataID: tempId
        },
        dataSrc: "",
        type: 'POST'
    },
    "columns": [
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
            }
        },
        {
            "title": "No Induk",
            "data": 1,
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            "title": "Nama",
            "data": 2,
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
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