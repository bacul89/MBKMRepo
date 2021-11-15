


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
        /*"columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0,
            //"visible": false, 'targets': [4, 6]
        }],

        $('#example').DataTable({
            "paging": false,
            "ordering": false,
            "info": false
        });*/
        //"order": [[1, 'asc']],
        paging : false,
        ordering : false,
        info : false,
        /*"proccessing": true,
        "serverSide": true,*/
        //"order": [[1, 'asc']],
        //"aaSorting": [[0, "asc"]],
        ajax: {
            url: '/Admin/JadwalKuliah/GetMataKuliah?' + variable,
            dataSrc: '',
            type: 'POST'
        },
/*        "language": {
            "emptyTable": "No record found.",
            "processing":
                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
            "search": "Search:",
            "searchPlaceholder": ""
        },*/
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
                "title": "MBKM",
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <input type="checkbox" id="mbkm-flag" value="${data}">
                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                    //<a href="javascript:void(0)" style="color:black" onclick="DetailMasterCPL('${data}')"> <i class="fas fa-file-search coral"></i></a>
                }
            },

        ],
/*        "createdRow": function (row, data, index) {
            $('td', row).css({
                'border': '1px solid coral',
                'border-collapse': 'collapse',
                'vertical-align': 'center',
            });

        }*/
    });

    setTimeout(function () {
        controlButtonAdd();
    }, 300);
}


$('#add').click(function () {
    var list = [];

    //var n = $('input:checkbox[id^="mbkm-flag"]:checked').length;

    $('input:checkbox[id^="mbkm-flag"]:checked').each(function () {
        //var num = $(this).attr("id");

        var id = parseInt($(this).val());
        //console.log(id);
        list.push(id);
        //console.log(num);
    });


    //console.log(list);
    swal.fire({
        title: "Apakah Anda Yakin \n Submit Jadwal Untuk Perkuliah MBKM ?",
        type: "warning",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#06a956",
        confirmButtonText: "Yes",
        closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {
            $.LoadingOverlay("show");
            $.ajax({
                url: '/Admin/JadwalKuliah/Publish',
                type: 'post',
                data: {
                    list: list
                },
                datatype: 'json',
                success: function (e) {
                    $.LoadingOverlay("hide");
                    if (e.status == 500) {
                        Swal.fire({
                            title: 'Oppss',
                            icon: 'error',
                            html: e.message,
                            showCloseButton: true,
                            showCancelButton: false,
                            focusConfirm: false,
                            confirmButtonText: 'OK'
                        })
                    } else {
                        Swal.fire({
                            title: 'Berhasil',
                            icon: 'success',
                            html: 'Data Berhasil Tersubmit',
                            showCloseButton: true,
                            showCancelButton: false,
                            focusConfirm: false,
                            confirmButtonText: 'OK'
                        })
                        reloadDatatable();
                    }
                }, error: function (e) {
                    $.LoadingOverlay("hide");
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


});


function controlButtonAdd(){
    console.log(datatable.data().count());
    if (datatable.data().count() > 0) {
        $('#add').removeAttr("disabled");
        $('#add').removeAttr("title");
    } else {
        $('#add').attr("disabled", "disabled");
        //$().attr("disabled", "disabled");
        $('#add').attr("title", "Mata Kuliah tidak ditemukan!");
    }
}