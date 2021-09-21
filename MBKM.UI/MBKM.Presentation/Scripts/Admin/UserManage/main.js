

var tableUser = $('#TableList').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "paging": false,
        "targets": 0,
         "visible": false, 'targets': [5] 
    }],
    //"order": [[1, 'asc']],
    "proccessing": true,
    "serverSide": true,
    "order": [[1, 'asc']],
    //"aaSorting": [[0, "asc"]],
    "ajax": {
        url: '/Admin/UserManage/GetList',
        type: 'POST'
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
            //"title": "No",
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        {
            //"title": "Nomor Induk Pegawai",
            "data": "NoPegawai",
            "name": "NoPegawai",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Nama",
            "data": "Nama",
            "name": "Nama",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Password",
            "data": "Password",
            "name": "Password",
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
            //"title": "Jabatan",
            "data": "RoleID",
            "name": "RoleID",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
            
            
        },
        {
            //"title": "Program Studi/Unit",
            "data": "NamaProdi",
            "name": "NamaProdi",
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
                                <a type="button"  id="btnDel"> <i class="fas fa-trash-alt coral"></i> </button >
                                
                                
                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
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
function validationCustomEditUser() {
    var isValid;
    $(".asd").each(function () {
        var element = $(this);
        if (element.val() == "") {
            return isValid = false;
        } else {
            return isValid = true;
        }
    });
    return isValid;
}
$("#TableList").on('click', '#btnDel', function () {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var data = $("#TableList").DataTable().row($(this).parents('tr')).data();
            console.log(data); 
            
            var base_url = window.location.origin;
            $.ajax({
                url: base_url + '/Admin/UserManage/PostDeleteUser',
                type: 'post',
                datatype: 'json',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (e) {
                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: 'Data User Berhasil Dihapus',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                    tableUser.ajax.reload(null, false);
                   
                },
                error: function (e) {
                    Swal.fire({
                        title: 'Oppss',
                        icon: 'error',
                        html: 'Coba Reload Page',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                  
                }
            })
           
                }
            })
        })

