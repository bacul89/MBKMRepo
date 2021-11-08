var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-report-internal-mahasiswa').DataTable({
       
    });
    $('#inp_semester').change(function () {
        jsData.semester = $('#inp_semester :selected').val();
        table.destroy();
        table = $('#table-data-report-internal-mahasiswa').DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'excel',
                    text: '<i class="fas fa-file-download"></i> Cetak Excel</button>',
                    titleAttr: 'Excel',
                    className: 'btn btn-success',
                    exportOptions: {
                        orthogonal: 'export'
                        //columns: [0, ':visible'],
                        //stripHtml:false

                    }
                },
                {
                    extend: 'pdf',
                    text: '<i class="fas fa-file-download"></i> Cetak PDF</button>',
                    titleAttr: 'Excel',
                    className: 'btn btn-danger',
                    exportOptions: {
                        orthogonal: 'export'
                        //columns: [0, ':visible'],
                        //stripHtml:false

                    }
                }
            ],
            "ajax": {
                url: '/Admin/ReportInternalKeluar/DataTable',
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
                    /*Kampus tempat MBKM*/
                    "data": 5,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*Nama MK*/
                    "data": 6,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*SKS */
                    "data": 7,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*Nilai asal*/
                    "data": 8,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*Nilai kode makul*/
                    "data": 9,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*kjode Makul diakui */
                    "data": 10,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*nama mk diakui mk*/
                    "data": 11,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.6vw">' + data + '</div>';
                    }
                },
                {
                    /*huruf*/
                    "data": 12,
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
        //table.buttons().container()
        //    .appendTo('#table-data-report-internal-mahasiswa_wrapper .fas');
    })
})