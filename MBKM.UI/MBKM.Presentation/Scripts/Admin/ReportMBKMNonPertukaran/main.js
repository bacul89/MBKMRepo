var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-rekap-non-pertukaran').DataTable({});
    $('#inp_semester').change(function () {
        jsData.semester = $('#inp_semester :selected').val();
        table.destroy();
        table = $('#table-data-rekap-non-pertukaran').DataTable({
            "ajax": {
                url: '/Admin/ReportMBKMNonPertukaran/DataTable',
                dataSrc: '',
                data: {
                    strm: $('#inp_semester :selected').val(),
                },
                type: 'post',
            },
            "columns": [
                {
                    "data": null,
                    "render": function (data, type, full, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
                    }
                },
                {
                    /*Semester*/
                    "data": 0,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*jenjang*/
                    "data": 1,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*nim*/
                    "data": 2,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*nama*/
                    "data": 3,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*prodi*/
                    "data": 4,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*jenis kegiatan*/
                    "data": 5,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*judul*/
                    "data": 6,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*lokasi */
                    "data": 7,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*nomor sk*/
                    "data": 8,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*tanggal*/
                    "data": 9,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*kode mk*/
                    "data": 10,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*nama mk*/
                    "data": 11,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*sks*/
                    "data": 12,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*nilai*/
                    "data": 13,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*Huruf*/
                    "data": 14,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },

            ],
            "createdRow": function (row, data, index) {
                $('td', row).css({
                    'border': '1px solid coral',
                    'border-collapse': 'collapse',
                    'vertical-align': 'center',
                });
            }
        });
    })
})