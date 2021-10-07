$(document).ready(function () {
/*    var t = $('#TableList').DataTable({
        "scrollX": true,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0
        }],
        "order": [[2, 'asc']],
        "columnDefs": [{ "orderable": false, "targets": 1 }],
        "createdRow": function (row, data, index) {

        }
    });
   

/*    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();*/

    $('.js-example-basic-single').select2({
        placeholder: "-- Kode Mata Kuliah --",
        "proccessing": true,
        "serverSide": true,
        ajax: {
            url: '/Admin/MasterMapingCapaianPembelajaran/GetMataKuliah',
            type: 'post',
            dataType: 'json',
            data: function (params) {
                return {
                    search: params.term,
                    length: params.length || 10,
                    skip: params.skip || 0
                };
            },
            processResults: function (data, page) {
                return {
                    results: data
                }
            }
        }
    });


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


//batas main
/*var datatable = $('#table-data-master-mapping-cpl').DataTable({
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
        url: '/Admin/MasterMapingCapaianPembelajaran/GetList',
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
        {
            "title": "Action",
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">

                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateMasterMapingCapaianPembelajaran('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewMasterMapingCapaianPembelajaran('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedMasterMapingCapaianPembelajaran('${data}')">  <i class="fas fa-trash-alt coral"></i></a>



                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                //<a href="javascript:void(0)" style="color:black" onclick="DetailMasterCPL('${data}')"> <i class="fas fa-file-search coral"></i></a>
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
            //"title": "Nomor Induk Pegawai",
            "data": "Kode",
            "name": "Kode",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Nama",
            "data": "Kelompok",
            "name": "Kelompok",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            //"title": "Email",
            "data": "Capaian",
            "name": "Capaian",
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
//tes detail overtime
$("#tableList").on('click', '#btnDetail', function () {
    var data = $("#tableList").DataTable().row($(this).parents('tr')).data();
    console.log(data);
    //alert("tes aaaaaa dong bro");
    //$("#staticBackdropLabel").text(data.firstName + " " + data.lastName);

    $("#kodeDetail").val(data.Kode);

});*/


/*var dataTable = $('#table-data-master-mapping-cpl').DataTable({
    ajax: {
        url: '/Admin/MasterMapingCapaianPembelajaran/GetDataMasterMapingCapaianPembelajaran',
        dataSrc: ''
    },

    "columns": [
        {
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateMasterMapingCapaianPembelajaran('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewMasterMapingCapaianPembelajaran('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedMasterMapingCapaianPembelajaran('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                            </div>
                        </div>`;
            }
        },
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        {
            "data": "KodeMataKuliah",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "NamaMataKuliah",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "MasterCapaianPembelajaranID",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "Kelompok",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
*//*        {
            "data": "err",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },*//*
*//*        {
            "data": "f",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + convertMilisecondToDate(data) + '</div>';
            }
        },*//*
*//*        {
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
        },*//*


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
});*/