var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-feedback-makul').DataTable({
        "ajax": {
            url: '/Portal/FeedBackMatakuliah/GetDataTable',
            dataSrc: '',
            data: {
                semester: 2110,
            },
            type: 'post',
        },
        "columns": [
            {
                "data": 0,
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                                <div class="col" style="text-align:center">
                                    <a href="javascript:void(0)" style="color:black" onclick="redirectData('${row[0]}','${row[7]}','${row[8]}')"> <i class="fas fa-edit coral" ></i></a>
                                 </div>
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
                /*tahun ujian*/
                "data": 1,
                "render": function (data, type, row, meta) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                /*mata kuliah*/
                "data": 2,
                "render": function (data, type, row, meta) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                /*SKS*/
                "data": 3,
                "render": function (data, type, row, meta) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                /*Tanggal ujian*/
                "data": 4,
                "render": function (data, type, row, meta) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                /*waktu*/
                "data": 5,
                "render": function (data, type, row, meta) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                /*lokasi*/
                "data": 6,
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
    $('#inp_semester').change(function () {
        jsData.semester = $('#inp_semester :selected').val();
        table.destroy();
        table = $('#table-data-feedback-makul').DataTable({
            "ajax": {
                url: '/Portal/FeedBackMatakuliah/GetDataTable',
                dataSrc: '',
                data: {
                    semester: jsData.semester,
                },
                type: 'post',
            },
            "columns": [
                {
                    "data": 0,
                    "render": function (data, type, row, meta) {
                        return `<div class="row justify-content-center">
                                <div class="col" style="text-align:center">
                                    <a href="javascript:void(0)" style="color:black" onclick="redirectData('${row[0]}','${row[7]}','${row[8]}')"> <i class="fas fa-edit coral" ></i></a>
                                 </div>
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
                    /*tahun ujian*/
                    "data": 1,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*mata kuliah*/
                    "data": 2,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*SKS*/
                    "data": 3,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Tanggal ujian*/
                    "data": 4,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*waktu*/
                    "data": 5,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*lokasi*/
                    "data": 6,
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
        })
    })
});

function redirectData(id, ff, strm) {
    window.location.href = "/Portal/FeedBackMatakuliah/DetailFeedBackMatakuliah/?id=" + id + "&ff=" + ff + "&strm=" + strm;
}