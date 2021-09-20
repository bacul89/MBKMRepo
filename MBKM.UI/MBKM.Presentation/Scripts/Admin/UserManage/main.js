var tableUser = $('#TableList').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "targets": 0
    }],
    "order": [[1, 'asc']],
    "proccessing": true,
    "serverSide": true,
    "ajax": {
        url: '/Admin/UserManage/GetList',
        type: 'POST'
    },
    "language": {
        "emptyTable": "No record found.",
        "processing":
            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
        "search": "",
        "searchPlaceholder": "Search..."
    },
    "columns": [
        {
            //"title": "No",
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        {
            //"title": "Nomor Induk Pegawai",
            "data": "NoPegawai",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Nama",
            "data": "Nama",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Password",
            "data": "Password",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Jabatan",
            "data": "RoleID",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Program Studi/Unit",
            "data": "NamaProdi",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + row.NamaProdi + '(' + row.KodeProdi + ')' + '</div>';
            }
        },

        {
            "title": "Action",
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="EditUserTemplate('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DetailUserTemplate('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedTemplateEmail('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                            </div>
                        </div>`;
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
function validationCustom2() {
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
