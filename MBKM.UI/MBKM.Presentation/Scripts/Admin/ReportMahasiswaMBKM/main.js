var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-rekap-mahasiswa').DataTable({});
    $('#inp_semester').change(function () {
        jsData.semester = $('#inp_semester :selected').val();
        table.destroy();
        table = $('#table-data-rekap-mahasiswa').DataTable({
            paging: false,
            info: false,
            "ajax": {
                url: '/Admin/ReportMahasiswaMBKM/TableData',
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
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
                    }
                },
                {
                    /*Semester*/
                    "data": 0,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*jenjang*/
                    "data": 1,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Fakultas*/
                    "data": 2,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*prodi*/
                    "data": 3,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*lokasi*/
                    "data": 4,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*pertukaran*/
                    "data": 5,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*keluar atma*/
                    "data": 6,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*eksternal */
                    "data": 7,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*non pertukaran */
                    "data": 8,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },

            ],
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api(), data;

                // converting to interger to find total
                var intVal = function (i) {
                    return typeof i === 'string' ?
                        i.replace(/[\$,]/g, '') * 1 :
                        typeof i === 'number' ?
                            i : 0;
                };

                // computing column Total of the complete result 

                var tueTotal = api
                    .column(6)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                var wedTotal = api
                    .column(7)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                var thuTotal = api
                    .column(8)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                var friTotal = api
                    .column(9)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);


                // Update footer by showing the total with the reference of the column index 
                $(api.column(6).footer()).css({
                    'border': '1px solid black',
                    'border-collapse': 'collapse',
                    'vertical-align': 'center',
                }).html(tueTotal);
                $(api.column(7).footer()).css({
                    'border': '1px solid black',
                    'border-collapse': 'collapse',
                    'vertical-align': 'center',
                }).html(wedTotal);
                $(api.column(8).footer()).css({
                    'border': '1px solid black',
                    'border-collapse': 'collapse',
                    'vertical-align': 'center',
                }).html(thuTotal);
                $(api.column(9).footer()).css({
                    'border': '1px solid black',
                    'border-collapse': 'collapse',
                    'vertical-align': 'center',
                }).html(friTotal);
            },

            "createdRow": function (row, data, index) {
                $('td', row).css({
                    'border': '1px solid coral',
                    'border-collapse': 'collapse',
                    'vertical-align': 'center',
                });
            }
        });
    })

    $('#exportPDF').click(function () {
        GetFilePDF();
    })
    $('#exportExcel').click(function () {
        GetFileExcel();
    })

})


function GetFilePDF() {
    if ($('#inp_semester :selected').val() == "") {
        Swal.fire({
            title: 'Oppss',
            icon: 'warning',
            html: 'Masukkan Inputan Semester!',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    } else {
        window.location.href = "/Admin/ReportMahasiswaMBKM/getDataPdf/" + $('#inp_semester :selected').val();
    }
}

function GetFileExcel() {
    if ($('#inp_semester :selected').val() == "") {
        Swal.fire({
            title: 'Oppss',
            icon: 'warning',
            html: 'Masukkan Inputan Semester!',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    } else {
        window.location.href = "/Admin/ReportMahasiswaMBKM/GetFileExcel/" + $('#inp_semester :selected').val();
    }
}
