var datatable = null;

$(document).ready(function () {
    //table = $('#table-data-transkrip').DataTable();



    datatable = $('#table-data-transkrip').DataTable({
        ajax: {
            url: '/Admin/Transkrip/GetTranskripList',
            dataSrc: ''
        },
        "columns": [
            {
                "title": "Action",
                "data": "MahasiswaID",
                "render": function (data, type, row, meta) {

                    var btn = "";
                    if (row.FlagCetak == true) {
                        btn = `<button class="btn btn-md btn-success" style="color: black" onclick="UpdateStatus(' ${data} ')"> Update Status <span class="fa fa-sync"> </span></button>
                               <button class="btn btn-md btn-success" style="color: black" onclick="print(' ${data} ')"> Cetak <span class="fa fa-print"> </span></button>`;
                    } else {
                        btn = `<button class="btn btn-md btn-default" style="color: black" disabled="true"> Update Status <span class="fa fa-sync"></span></button>
                            <button class="btn btn-md btn-success" style = "color: black" onclick = "print(' ${data} ')" > Cetak <span class="fa fa-print"> </span></button>`;
                    }

                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">

                                ${btn}

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
                "data": "JenjangStudi",
                "name": "JenjangStudi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nama",
                "data": "NamaUniversitas",
                "name": "NamaUniversitas",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "NIM",
                "name": "NIM",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "Nama",
                "name": "Nama",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "NoKerjasama",
                "name": "NoKerjasama",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "FlagCetak",
                "name": "FLagCetak",
                "render": function (data, type, row, meta) {
                    var label = "";
                    if (data == true) {
                        label = "<label class='label label-danger'>Sudah Cetak</label>";
                    } else if (data == false) {
                        label = "<label class='label label-success'>Belum Cetak</label>";
                    }
                    return '<div class="center">' + label + '</div>';
                }
            }

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

})


function UpdateStatus(idMahasiswa) {


            $.LoadingOverlay("show");
            $.ajax({
                url: '/Admin/Transkrip/UpdateStatus',
                type: 'post',
                data: {
                    idMahasiswa: idMahasiswa
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
                            html: 'Cetak Status Berhasil Di Update',
                            showCloseButton: true,
                            showCancelButton: false,
                            focusConfirm: false,
                            confirmButtonText: 'OK'
                        })
                        datatable.ajax.reload(null, false);
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