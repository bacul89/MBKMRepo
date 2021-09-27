$(document).ready(function () {
    var Id = $("#idKerjasama").val();
    console.log(Id)
    $('#table-data-attachment').DataTable({
    "proccessing": true,
    "serverSide": true,
    "ajax": {
        url: '/Admin/PerjanjianKerjasama/GetAllPKAttachment/${Id}',
        dataSrc: '',
        type: 'post',
    },
    "columns": [
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return '<div class="center">' + (meta.row + 1) + '</div>';
            }
        },
        {
            "title": "Nama File",
            "data": "FileName",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "title": "Download",
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="/Admin/PerjanjianKerjasama/DownloadFile/${data}"" style="color:coral"> <i class="fas fa-file-download"></i></a>
                            </div>
                        </div>`;
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
    searching: false,
    paging: false,
    info: false
    });
    function DownloadAttachment(dataID) {
        $.ajax({
            url: '/Admin/PerjanjianKerjasama/DownloadFile',
            type: 'get',
            datatype: 'json',
            data: {
                id: dataID
            },
            contentType: 'application/json',
            success: function (e) {
                console.log("cobaa")
            }
        })
    }
});