var tableUser = $('#TableList').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "paging": false,
        "targets": 0,
        //"visible": false, 'targets': [4, 6]
    }],
    //"order": [[1, 'asc']],
    //"proccessing": true,
    //"serverSide": true,
    "order": [[2, 'asc']],
    //"aaSorting": [[0, "asc"]],
    "ajax": {
        url: '/Admin/MasterCPL/GetDataMasterCpl',
        dataSrc: ''
        //type: 'POST'
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
                                
                               <a href="javascript:void(0)" style="color:black" onclick="UpdateMasterCPL('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DetailMasterCPL('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a type="button"  id="btnDel"> <i class="fas fa-trash-alt coral"></i> </button >
                                
                                
                                
                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
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
            "data": "CapaianPembelajaran",
            "name": "CapaianPembelajaran",
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
//$(document).ready(function () {
//    loadFakultas();
//    $("#prodi").select2({
//        dropdownParent: $("#TambahCPL"),
//        placeholder: "-- Pilih Program Studi --"
//    });
//    $("#lokasi").select2({
//        dropdownParent: $("#TambahCPL"),
//        placeholder: "-- Pilih Lokasi --"
//    });
//});
//function loadFakultas() {
//    $("#fakultas").select2({
//        dropdownParent: $("#TambahCPL"),
//        placeholder: "-- Pilih Fakultas --",
//        width: "100%",
//        ajax: {
//            url: '/Admin/MasterCPL/GetFakultas',
//            dataType: 'json',
//            method: "POST",
//            delay: 250,
//            cache: false,
//            data: function (params) {
//                return {
//                    Search: params.term || ""
//                };
//            },
//            processResults: function (data, params) {
//                return {
//                    results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
//                };
//            },
//        }
//    });
//    $("#fakultas").change(function () {
//        $("#prodi").select2({
//            dropdownParent: $("#TambahCPL"),
//            placeholder: "-- Pilih Program Studi --",
//            width: "100%",
//            ajax: {
//                url: '/Admin/MasterCPL/GetProdiByFakultas',
//                dataType: 'json',
//                method: "POST",
//                delay: 250,
//                cache: false,
//                data: function (params) {
//                    return {
//                        Search: params.term || "",
//                        IDFakultas: $('#fakultas').val()
//                    };
//                },
//                processResults: function (data, params) {
//                    return {
//                        results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
//                    };
//                },
//            }
//        });
//        $("#prodi").change(function () {
//            $("#lokasi").select2({
//                dropdownParent: $("#TambahCPL"),
//                placeholder: "-- Pilih Lokasi --",
//                width: "100%",
//                ajax: {
//                    url: '/Admin/MasterCPL/GetLokasiByProdi',
//                    dataType: 'json',
//                    method: "POST",
//                    delay: 250,
//                    cache: false,
//                    data: function (params) {
//                        return {
//                            Search: params.term || "",
//                            IDProdi: $('#prodi').val()
//                        };
//                    },
//                    processResults: function (data, params) {
//                        return {
//                            results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Kampus } })
//                        };
//                    },
//                }
//            });
//        });
//    });
//}