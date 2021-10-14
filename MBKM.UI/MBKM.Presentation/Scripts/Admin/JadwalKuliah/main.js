var datatable = null;
$(document).ready(function () {
    $("#fakultasCari").prop("disabled", true);
    $("#prodiCari").prop("disabled", true);
    $("#lokasiCari").prop("disabled", true);
    $("#matakuliahCari").prop("disabled", true);
    $("#fakultasCari").select2({
        placeholder: "-- Pilih Fakultas --"
    });
    $("#prodiCari").select2({
        placeholder: "-- Pilih Program Studi --"
    });
    $("#lokasiCari").select2({
        placeholder: "-- Pilih Lokasi --"
    });
    $("#tahunAjaranCari").select2({
        placeholder: "-- Pilih Tahun Ajaran --"
    });

    loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");

    datatable = $('#table-data-master-mapping-cpl').DataTable();
    $("#table-data-master-mapping-cpl_filter").hide();

});
function buttonHandler(param) {
    /*title='This is my title'*/
    //console.log(param);
    /*            if (access == "cari") {
        if (param == "open") {
            $('#cari').removeAttr("disabled");
            $('#cari').removeAttr("title");
        } else if (param == "close") {
            $('#cari, #add').attr("disabled", "disabled");
            $('#cari').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
            $('#add').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
        } else {
            console.log("error : param");
        }



    } elseif(access == "action") {

    } else {
        console.log("error : access");
    }
*/

            if (param == "open") {
        $('#cari, #add').removeAttr("disabled");
        $('#cari, #add').removeAttr("title");
    } else if (param == "close") {
        $('#cari, #add').attr("disabled", "disabled");
        //$().attr("disabled", "disabled");
        $('#cari').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
        $('#add').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
    } else {
        console.log("error : access");
    }

}

$('#cari').click(function () {

    console.log("comeback to live");
    reloadDatatable();
});
function convertMilisecondToDate(value) {
    var num = parseInt(value.match(/\d+/), 10)
    var date = new Date(num);
    var result = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return result;

}

function reloadDatatable() {

    console.log("hello");
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
            url: '/JadwalKuliah/SearchList?' + variable,
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
                "data": "SKS",
                "name": "SKS",
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
                "name": "Hari",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
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

    $("#table-data-master-mapping-cpl_filter").hide();
}


/* search */
function loadJenjangStudi(tipe, id, nama) {
    $("#" + id + "Cari").select2({
        placeholder: "-- Pilih " + nama + " --",
        width: "100%",
        ajax: {
            url: "/JadwalKuliah/getLookupByTipe",
            dataType: 'json',
            method: "POST",
            delay: 250,
            cache: false,
            data: function (params) {
                return {
                    Search: params.term || "",
                    Tipe: tipe
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) { return { id: item.Nilai, value: item.Nilai, text: item.Nama } })
                };
            },
        }
    });
    $("#" + id + "Cari").change(function () {
        
        buttonHandler("close");
        $("#fakultasCari").empty();
        $("#prodiCari").empty();
        $("#prodiIdCari").val('');
        $("#lokasiCari").empty();
        $("#matakuliahCari").empty();
        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);
        $("#matakuliahCari").prop("disabled", true);
        $("#fakultasCari").prop("disabled", false);
        $("#fakultasCari").select2({
            placeholder: "-- Pilih Fakultas --",
            width: "100%",
            ajax: {
                url: "/JadwalKuliah/GetFakultas",
                dataType: 'json',
                method: "POST",
                delay: 250,
                cache: false,
                data: function (params) {
                    return {
                        Search: params.term || "",
                        JenjangStudi: $('#' + id + "Cari").val(),
                    };
                },
                processResults: function (data, params) {
                    return {
                        results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                    };
                },
            }
        });
        $("#fakultasCari").change(function () {
            
            buttonHandler("close");
            $("#prodiCari").empty();
            $("#prodiIdCari").val('');
            $("#lokasiCari").empty();
            $("#matakuliahCari").empty();
            $("#lokasiCari").prop("disabled", true);
            $("#matakuliahCari").prop("disabled", true);

            $("#prodiCari").prop("disabled", false);
            $("#prodiCari").select2({
                placeholder: "-- Pilih Program Studi --",
                width: "100%",
                ajax: {
                    url: "/JadwalKuliah/GetProdiByFakultas",
                    dataType: 'json',
                    method: "POST",
                    delay: 250,
                    cache: false,
                    data: function (params) {
                        return {
                            Search: params.term || "",
                            JenjangStudi: $('#' + id + "Cari").val(),
                            IDFakultas: $('#fakultasCari').val()
                        };
                    },
                    processResults: function (data, params) {
                        return {
                            results: $.map(data, function (item) { return { id: item.Nama, value: item.Nama, text: item.Nama } })
                        };
                    },
                }
            });

            $("#prodiCari").change(function () {
                //console.log("select2");
                
                buttonHandler("close");
                $("#lokasiCari").empty();
                $("#prodiIdCari").val('');
                $("#matakuliahCari").empty();
                $("#matakuliahCari").prop("disabled", true);

                $("#lokasiCari").prop("disabled", false);

                //$("#kampusCari").val('');
                $("#lokasiCari").select2({
                    placeholder: "-- Pilih Lokasi --",
                    width: "100%",
                    ajax: {
                        url: "/JadwalKuliah/GetLokasiByProdi",
                        dataType: 'json',
                        method: "POST",
                        delay: 250,
                        cache: false,
                        data: function (params) {
                            return {
                                Search: params.term || "",
                                NamaProdi: $('#prodiCari').val(),
                                JenjangStudi: $('#' + id + "Cari").val()
                            };
                        },
                        processResults: function (data, params) {
                            return {
                                results: $.map(data, function (item) { return { id: item.ID, value: item.Kampus, text: item.Kampus } })
                            };
                        },
                    }
                });



                $("#lokasiCari").change(function () {
                    
                    $("#prodiIdCari").val($("#lokasiCari").val());

                    $("#kampusCari").removeAttr('value');
                    var kampus = $(this).find(":selected").text();
                    //console.log(kampus);
                    $("#kampusCari").val(kampus);


                    //getLocationByPodiID();


                    });

                 });
                });
            });
        }


function getLocationByPodiID() {
    //console.log("ajax");
    var dataParam = {};
    dataParam.NamaProdi = $('#prodiCari').val();
    dataParam.JenjangStudi = $('#jenjangCari').val();
    dataParam.Search = "";

    //console.log(dataParam);

    $.ajax({
        url: "/JadwalKuliah/GetLokasiByProdi",
        dataType: 'json',
        method: "POST",
        //delay: 250,
        //cache: false,
        contentType: 'application/json',
        data: JSON.stringify(dataParam),
        success: function (e) {
            //console.log(e[0]);
            $("#prodiIdCari").val(e[0].ID);
            //console.log("Lokasi");
        }
    });
}

$(document).ready(function () {
    isZooming();
});


$(window).resize(function () {
    isZooming();
});


function isZooming() {
    var defaultH = 700;
    var square = $('.responsive-content');
    var screenH = $(document).height();
    //console.log("dfH :"+defaultH);
    //console.log("wnH :"+screenH);


    if (defaultH < screenH) {

        var footer = 39;
        var header = 105;
        var contentHeight = screenH - header - footer;

        if (defaultH < contentHeight) {
            square.css('min-height', contentHeight);
        } else {
            square.css('min-height', defaultH);
        }


    } else {
        square.css('min-height', defaultH);
    }
}


