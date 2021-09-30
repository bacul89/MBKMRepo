var tableMenuRole = $('#table-role').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "paging": false,
        "targets": 0
    }],
    "proccessing": true,
    "serverSide": true,
    "order": [[1, 'asc']],
    //"aaSorting": [[0, "asc"]],
    "ajax": {
        url: '/Admin/Role/GetDataRole',
        type: 'POST',
        dataSrc: 'data'
    },
    "language": {
        "emptyTable": "No record found.",
        "processing":
            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
        "search": "Search:",
        "searchPlaceholder": ""
    },
    "columns": [
        {
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateRole('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DetailRole('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedMenuRole('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                            </div>
                        </div>`;
            }
        },
        {
            //"title": "No",
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        {
            //"title": "Jabatan",
            "data": "Code",
            "name": "Code",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Jabatan",
            "data": "RoleName",
            "name": "RoleName",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "Status",
            "name": "Status",
            "render": function (data, type, row, meta) {
                if (data === true) {
                    return '<div class="center">Aktif</div>';
                } else {
                    return '<div class="center">Tidak Aktif</div>';
                }

            }
        },
    ],
    "createdRow": function (row, data, index) {
        $('td', row).css({
            'border': '1px solid coral',
            'border-collapse': 'collapse',
            'vertical-align': 'center',
        });

    }//,
    //'columnDefs': [
    //    //hide the second & fourth column
    //    { 'visible': false, 'targets': [5] }
    //]

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