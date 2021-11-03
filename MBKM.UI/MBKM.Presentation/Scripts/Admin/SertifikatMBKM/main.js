var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-sertifikat-mahasiswa').DataTable({});
    $('#inp_semester').change(function () {
        jsData.semester = $('#inp_semester :selected').val();
        table.destroy();
        table = $('#table-data-sertifikat-mahasiswa').DataTable({
            "ajax": {
                url: '/Admin/SertifikatMBKM/GetDataTable',
                dataSrc: '',
                data: {
                    strm: $('#inp_semester :selected').val(),
                },
                type: 'post',
            },
            "columns": [
                {
                    "title": "Action",
                    "render": function (data, type, row, meta) {
                        if (row[7] == "Sudah Feedback" && row[8] == "Sudah Bayar") {
                            return `<div class="row justify-content-center">
                                        <div class="col" style="text-align:center">
                                            <a href="javascript:void(0)" style="color:black" onclick="GetSertificate('${row[0]}')"> <i class="fas fa-file-search coral"></i></a>
                                        </div>
                                    </div>`;
                        } else {
                            return `<div class="row justify-content-center">
                                        <div class="col" style="text-align:center">
                                            <a href="javascript:void(0)" style="color:black" disabled> <i class="fas fa-file-search coral"></i></a>
                                        </div>
                                    </div>`;
                        }
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
                    }
                },
                {
                    /*Semester*/
                    "data": 1,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*jenjang*/
                    "data": 2,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*universitas*/
                    "data": 3,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Nim*/
                    "data": 4,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*nama mahasiswa*/
                    "data": 5,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*No Kerjasama*/
                    "data": 6,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + row[4] + ' - ' + row[5] + '</div>';
                    }
                },
                {
                    /*Status Feedback*/
                    "data": 7,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Status Pembayaran*/
                    "data": 8,
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
    })
})

function GetSertificate(dd) {
    window.location.href = "/Admin/SertifikatMBKM/GetFile/" + dd;
}