var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-report-internal-mahasiswa').DataTable({
        "ajax": {
            url: '/Admin/ReportMBKMInternalPertukaran/DataTable',
            dataSrc: '',
            data: {
                strm: $('#firstSemester').val(),
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
                /*Kode Makul*/
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
    $('#inp_semester').change(function () {
        jsData.semester = $('#inp_semester :selected').val();
        table.destroy();
        table = $('#table-data-report-internal-mahasiswa').DataTable({
            "ajax": {
                url: '/Admin/ReportMBKMInternalPertukaran/DataTable',
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
                    /*Kode Makul*/
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
        window.location.href = "/Admin/ReportMBKMInternalPertukaran/GetFilePdf/" + $('#inp_semester :selected').val();
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
        window.location.href = "/Admin/ReportMBKMInternalPertukaran/GetFileExcel/" + $('#inp_semester :selected').val();
    }
}
