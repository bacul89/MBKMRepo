$(document).ready(function () {
    var t = $('#TableList').DataTable({
        "scrollX": true,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0
        }],
        "order": [[1, 'asc']],
        "columnDefs": [{ "orderable": false, "targets": 9 }],
        "createdRow": function (row, data, index) {

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

function convertMilisecondToDate(value) {
    var num = parseInt(value.match(/\d+/), 10)

    var date = new Date(num); 
    var result = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return result;

}


var dataTable = $('#table-data-master-mapping-cpl').DataTable({
    ajax: {
        url: '/Admin/MasterMapingCpl/GetDataMasterMapingCpl',
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
            "data": "arr",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "brr",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "crr",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "drr",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "err",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
/*        {
            "data": "f",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + convertMilisecondToDate(data) + '</div>';
            }
        },*/
/*        {
            "data": "UpdatedBy",
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
        },*/
        {
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateMasterMapingCpl('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewMasterMapingCpl('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedMasterMapingCpl('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                            </div>
                        </div>`;
            }
        },

    ],
    "createdRow": function (row, data, index) {
        $('td', row).css({
            'font-size': '1vw',
            'border-top': '1px solid coral',
            'border-bottom': '1px solid coral',
            'border-left': 'none',
            'border-right': 'none',
            'border-collapse': 'collapse',
            'vertical-align': 'center',
            'background': '#fff'
        });

        $('td:eq(0)', row).css({
            'border-left': '1px solid coral',
            'border-right': 'none'
        });
        $('td:eq(6)', row).css({
            'border-left': 'none',
            'border-right': '1px solid coral'
        });
    }
});