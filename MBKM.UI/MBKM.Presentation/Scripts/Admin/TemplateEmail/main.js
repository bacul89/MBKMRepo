$(document).ready(function () {
    var t = $('#TableList').DataTable({
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0
        }],
        "order": [[1, 'asc']],
        "createdRow": function (row, data, index) {
            $('td', row).css({
                'border': '1px solid coral',
            });
        }
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function validationCustom() {
    var isValid;
    $(".input-data").each(function () {
        var element = $(this);
        if (element.val() == "") {
            return isValid = false;
        } else {
            return isValid = true;
        }
    });
    return isValid;
}

var dataTable = $('#table-data-email-template').DataTable({
    ajax: {
        url: '/Admin/TemplateEmail/GetDataEmailTemplate',
        dataSrc: ''
    },
    "columns": [
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        {
            "data": "TipeMail",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "SubjectMail",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "BodyMail",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "IsActive",
            "render": function (data, type, row, meta) {
                if (data) {
                    return '<div class="center">Aktif</div>';
                } else {
                    return '<div class="center">Tidak Aktif</div>';
                }

            }
        },
        {
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateEmailTemplate('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewEmailTemplate('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeleteDataTemplate('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                            </div>
                        </div>`;
            }
        },

    ],
    "createdRow": function (row, data, index) {
        $('td', row).css({
            'border': '1px solid coral',
            'border-collapse': 'collapse',
            'vertical-align' : 'center',
        });
    }
});
