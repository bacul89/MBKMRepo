


$('#cari').click(function () {
    reloadDatatable();
});
function convertMilisecondToDate(value) {
    var num = parseInt(value.match(/\d+/), 10)
    var date = new Date(num);
    var result = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return result;

}

function reloadDatatable() {
    var variable =
        'idProdi=' + $('#prodiIdCari').val() +
        '&lokasi=' + $('#kampusCari').val() +
        '&idFakultas=' + $('#fakultasCari').val() +
        '&jenjangStudi=' + $('#jenjangCari').val() +
        '&strm=' + $('#tahunAjaranCari').val();

    datatable.destroy();
    datatable = $('#table-data-jadwal-kuliah').DataTable({
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0,
            //"visible": false, 'targets': [4, 6]
        }],
        //"order": [[1, 'asc']],
        "proccessing": true,
        "serverSide": true,
        "order": [[1, 'asc']],
        //"aaSorting": [[0, "asc"]],
        "ajax": {
            url: '/Admin/JadwalKuliah/SearchList?' + variable,
            //dataSrc: ''
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
            /*{
                "title": "Action",
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">

                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateJadwalKuliah('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewJadwalKuliah('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedJadwalKuliah('${data}')">  <i class="fas fa-trash-alt coral"></i></a>



                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                    //<a href="javascript:void(0)" style="color:black" onclick="DetailMasterCPL('${data}')"> <i class="fas fa-file-search coral"></i></a>
                }
            },*/
            {
                //"title": "No",
                "data": null,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "KodeMataKuliah",
                "name": "KodeMataKuliah",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nama",
                "data": "NamaMataKuliah",
                "name": "NamaMataKuliah",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "SKS",
                "name": "SKS",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "ClassSection",
                "name": "ClassSection",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "Hari",
                "name": "Hari",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "JamMasuk",
                "name": "JamMasuk",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + row.JamMasuk + ' - ' + row.JamSelesai + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "RuangKelas",
                "name": "RuangKelas",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "Lokasi",
                "name": "Lokasi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "NamaDosen",
                "name": "NamaDosen",
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
    /* datatable = $('#table-data-master-mapping-cpl').DataTable({
        ajax: {
            url: '@Url.Action("SearchList", "JadwalKuliah")?' + varibale,
            dataSrc: ''
        },
        "columns": [
            {
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="col" style="text-align:center"><a href="javascript:void(0)" style="color:black" onclick="javascript:$('#idMatkul').val(${data}); $('#daftarMatkul').submit();"><i class="fas fa-edit"></i></a></div>`;
                }
            },
            {
                "data": null,
                "render": function (data, type, full, meta) {
                    return '<div style="text-align:center; vertical-align: middle;">' + (meta.row + 1) + '</div>';
                }
            },
            {
                "data": "KodeMataKuliah",
                "render": function (data, type, row, meta) {
                    return '<div style="text-align:center; vertical-align: middle;">' + data + '</div>';
                }
            },
            {
                "data": "NamaMataKuliah",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                "data": null,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": null,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": null,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            }

        ]
    });*/
}


