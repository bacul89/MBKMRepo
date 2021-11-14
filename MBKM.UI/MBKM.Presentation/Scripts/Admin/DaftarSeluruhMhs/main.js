var tableUser = $('#TableList').DataTable({
    
    "columnDefs": [{
        //"orderable": false,
        //"paging": false,
        "targets": [0, 1],
        "searchable": false,
        "orderable": false,
        "paging": false
        
       
    }],
    //"order": [[1, 'asc']],
    "proccessing": true,
    "serverSide": true,
    "order": [[2, 'asc']],
    //"aaSorting": [[0, "asc"]],
    "ajax": {
        url: '/Admin/DaftarSeluruhMahasiswa/GetList',
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
            "title": "Action",
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                
                                 <a href="javascript:void(0)" style="color:black" onclick="DetailMhs('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                
                                
                                
                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
            }
        },
        {
            //"title": "No",
            "data": null,
            "render": function (data, type, full, meta) {
                //return'<div class="center vertical-center">' + (meta.row + 1) + '</div>';
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        {
            //"title": "Nomor Induk Pegawai",
            "data": "NamaUniversitas",
            "name": "NamaUniversitas",
            "render": function (data, type, row, meta) {
                if (data != null) {
                    return '<div class="center">' + data + '</div>';
                }
                else {
                    return '<div class="center"> - </div>';
                }
            }
        },
        {
            //"title": "Nama",
            "data": "JenjangStudi",
            "name": "JenjangStudi",
            "render": function (data, type, row, meta) {
                if (data != null) {
                    return '<div class="center">' + data + '</div>';
                }
                else {
                    return '<div class="center"> - </div>';
                }
            }
        },
        {
            //"title": "Email",
            "data": "ProdiAsal",
            "name": "ProdiAsal",
            "render": function (data, type, row, meta) {
                if (data != null) {
                    return '<div class="center">' + data + '</div>';
                }
                else {
                    return '<div class="center"> - </div>';
                }
            }
        },
        {
            //"title": "Password",
            "data": "NIMAsal",
            "name": "NIMAsal",
            "render": function (data, type, row, meta) {
                if (data != null) {
                    return '<div class="center">' + data + '</div>';
                }
                else {
                    return '<div class="center"> - </div>';
                }
            }
        },
        {
            //"title": "Jabatan",
            "data": "Nama",
            "name": "Nama",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },

        {
            //"title": "Jabatan",
            "data": "Gender",
            "name": "Gender",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }


        },
        {
            //"title": "Program Studi/Unit",
            "data": "NoKerjasama",
            "name": "NoKerjasama",
            "render": function (data, type, row, meta) {
                if (row.NamaUniversitas == 'UNIKA Atma Jaya' && row.NoKerjasamaInternal) {
                    console.log(row)
                    return '<div class="center">' + row.NoKerjasamaInternal + '</div>';
                }
                else if (data != null) {
                    return '<div class="center">' + data + '</div>';
                }
                else {
                    return '<div class="center"> - </div>';
                }
            }
        },
        {
            //"title": "Program Studi/Unit",
            "data": "StatusKerjasama",
            "name": "StatusKerjasama",
            "render": function (data, type, row, meta) {
                if (data != null) {
                    return '<div class="center">' + data + '</div>';
                }
                else {
                    return '<div class="center"> - </div>';
                }
            }
        },
        {
            //"title": "Program Studi/Unit",
            "data": "StatusVerifikasi",
            "name": "StatusVerifikasi",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
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