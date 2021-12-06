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
    /* $("#lokasicari").select2({
        placeholder: "-- pilih lokasi --"
    });*/
    $("#matakuliahCari").select2({
        placeholder: "-- Pilih Mata Kuliah --"
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
        'idProdi=' + $('#prodiCari').val() +
        //'&lokasi=' + $('#prodiCari').select2('data')[0].lokasi +
        '&idFakultas=' + $('#fakultasCari').val() +
        '&jenjangStudi=' + $('#jenjangCari').val() +
        '&idMatakuliah=' + $('#matakuliahCari').val();

    datatable.destroy();
    datatable = $('#table-data-master-mapping-cpl').DataTable({
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
            url: '/Admin/MasterMapingCapaianPembelajaran/SearchList?' + variable,
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
                //"title": "Nama",
                "data": "Status",
                "name": "Status",
                "render": function (data, type, row, meta) {
                    if (data == true) {
                        return '<div class="center"> Aktif</div>';
                    } else {
                        return '<div class="center"> Tidak Aktif</div>';
                    }

                }
            },
            {
                //"title": "Email",
                "data": "Capaian",
                "name": "Capaian",
                "render": function (data, type, row, meta) {
                    return '<div>' + data + '</div>';
                }
            },
        ],
        "createdRow": function (row, data, index) {
            $('td', row).css({
                'border': '1px solid coral',
                'border-collapse': 'collapse',
                'vertical-align': 'center',
            });

        }

    });

    $("#table-data-master-mapping-cpl_filter").hide();
}


/* --search */
function loadJenjangStudi(tipe, id, nama) {

    var getFakultas = $("#fakultasCari").val();
    var getProdi = $("#prodiCari").val();
    if (getFakultas != null && getProdi == null) {
        $("#" + id + "Cari").select2({
            placeholder: "-- Pilih " + nama + " --",
            width: "100%",
            ajax: {
                url: "/Admin/MasterMapingCapaianPembelajaran/getLookupByTipe",
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
            clearValueOnForm();
            buttonHandler("close");
            //$("#fakultasCari").empty();
            $("#prodiCari").empty();
            $("#prodiIdCari").val('');
            $("#lokasiCari").empty();
            $("#matakuliahCari").empty();
            $("#prodiCari").prop("disabled", true);
            $("#lokasiCari").prop("disabled", true);
            $("#matakuliahCari").prop("disabled", true);

                clearValueOnForm();
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
                        url: "/Admin/MasterMapingCapaianPembelajaran/GetProdiByFakultas",
                        dataType: 'json',
                        method: "POST",
                        delay: 250,
                        cache: false,
                        data: function (params) {
                            return {
                                Search: params.term || "",
                                JenjangStudi: $('#' + id + "Cari").val(),
                                IdFakultas: $('#fakultasCari').val()
                            };
                        },
                        processResults: function (data, params) {
                            return {
                                //results: $.map(data, function (item) { return { id: item.IDProdi, value: item.NamProdi, text: item.NamProdi+' - '+item.Lokasi, lokasi: item.Lokasi } })
                                results: $.map(data, function (item) { return { id: item.Nama, value: item.Nama, text: item.Nama } })
                            };
                        },
                    }
                });

                $("#prodiCari").change(function () {

                    //console.log("select2");
                    clearValueOnForm();
                    buttonHandler("close");
                    $("#lokasiCari").empty();
                    $("#prodiIdCari").val('');
                    $("#matakuliahCari").empty();
                    $("#matakuliahCari").prop("disabled", true);

                    //$("#lokasiCari").prop("disabled", false);

                    //$("#kampusCari").val('');

                    clearValueOnForm();
                    //$("#prodiIdCari").val($("#lokasiCari").val());

                    //$("#kampusCari").removeAttr('value');
                    //var kampus = $(this).find(":selected").text();
                    //console.log(kampus);
                    //$("#kampusCari").val(kampus);

                    buttonHandler("close");
                    $("#matakuliahCari").empty();
                    $("#matakuliahCari").prop("disabled", false);
                    //$("#matakuliahNamaCari").val('');

                    //getLocationByPodiID();

                    //var start = 0;
                    //var end = 0;
                    var pageLength = 0;
                    $('#matakuliahCari').select2({
                        placeholder: "-- Pilih Mata Kuliah --",
                        "proccessing": true,
                        "serverSide": true,
                        //multiple: true,
                        width: "100%",
                        ajax: {
                            url: "/Admin/MasterMapingCapaianPembelajaran/GetMataKuliah",
                            type: 'POST',
                            dataType: 'json',

                            data: function (params) {

                                return {
                                    take: 10,
                                    searchBy: params.term || "",
                                    skip: params.page || 1,
                                    idProdi: $('#prodiCari').val(),
                                    idFakultas: $('#fakultasCari').val()
                                };
                            },

                            processResults: function (data, params) {

                                var page = params.page - 1 || 1;
                                pageLength = pageLength + data.length;

                                return {
                                    results: $.map(data, function (item) { return { id: item.id, value: item.id, text: item.kode + ' - ' + item.text, name: item.text, kode: item.kode } }),
                                    pagination: {
                                        more: data.length > 0 && data.length == 10
                                    }
                                }
                            },
                        }
                    });



                    $("#matakuliahCari").change(function () {

                        $("#matakuliahNamaCari").removeAttr('value');
                        var matakuliah = $(this).select2('data')[0].name;
                        var matakuliahkode = $(this).select2('data')[0].kode;
                        $("#matakuliahNamaCari").val(matakuliah);

                        $("#matakuliahKodeCari").val(matakuliahkode);
                        $("#matakuliahIdCari").val($(this).val());

                        buttonHandler("open");

                    });


                });
         });
    }
    else if (getFakultas != null && getProdi != null) {
        $("#" + id + "Cari").select2({
            placeholder: "-- Pilih " + nama + " --",
            width: "100%",
            ajax: {
                url: "/Admin/MasterMapingCapaianPembelajaran/getLookupByTipe",
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
            clearValueOnForm();
            buttonHandler("close");
            //$("#fakultasCari").empty();
            /*$("#prodiCari").empty();
            $("#prodiIdCari").val('');*/
            $("#lokasiCari").empty();
            $("#matakuliahCari").empty();
            $("#prodiCari").prop("disabled", true);
            $("#lokasiCari").prop("disabled", true);
            $("#matakuliahCari").prop("disabled", true);

            clearValueOnForm();
            buttonHandler("close");
            /*$("#prodiCari").empty();
            $("#prodiIdCari").val('');*/
            $("#lokasiCari").empty();
            $("#matakuliahCari").empty();
            $("#lokasiCari").prop("disabled", true);
            $("#matakuliahCari").prop("disabled", true);



                //console.log("select2");
                clearValueOnForm();
                buttonHandler("close");
                $("#lokasiCari").empty();
                $("#prodiIdCari").val('');
                $("#matakuliahCari").empty();
                $("#matakuliahCari").prop("disabled", true);

                //$("#lokasiCari").prop("disabled", false);

                //$("#kampusCari").val('');

                clearValueOnForm();
                //$("#prodiIdCari").val($("#lokasiCari").val());

                //$("#kampusCari").removeAttr('value');
                //var kampus = $(this).find(":selected").text();
                //console.log(kampus);
                //$("#kampusCari").val(kampus);

                buttonHandler("close");
                $("#matakuliahCari").empty();
                $("#matakuliahCari").prop("disabled", false);
                //$("#matakuliahNamaCari").val('');

                //getLocationByPodiID();

                //var start = 0;
                //var end = 0;
                var pageLength = 0;
                $('#matakuliahCari').select2({
                    placeholder: "-- Pilih Mata Kuliah --",
                    "proccessing": true,
                    "serverSide": true,
                    //multiple: true,
                    width: "100%",
                    ajax: {
                        url: "/Admin/MasterMapingCapaianPembelajaran/GetMataKuliah",
                        type: 'POST',
                        dataType: 'json',

                        data: function (params) {

                            return {
                                take: 10,
                                searchBy: params.term || "",
                                skip: params.page || 1,
                                idProdi: $('#prodiCari').val(),
                                idFakultas: $('#fakultasCari').val()
                            };
                        },

                        processResults: function (data, params) {

                            var page = params.page - 1 || 1;
                            pageLength = pageLength + data.length;

                            return {
                                results: $.map(data, function (item) { return { id: item.id, value: item.id, text: item.kode + ' - ' + item.text, name: item.text, kode: item.kode } }),
                                pagination: {
                                    more: data.length > 0 && data.length == 10
                                }
                            }
                        },
                    }
                });



                $("#matakuliahCari").change(function () {

                    $("#matakuliahNamaCari").removeAttr('value');
                    var matakuliah = $(this).select2('data')[0].name;
                    var matakuliahkode = $(this).select2('data')[0].kode;
                    $("#matakuliahNamaCari").val(matakuliah);

                    $("#matakuliahKodeCari").val(matakuliahkode);
                    $("#matakuliahIdCari").val($(this).val());

                    buttonHandler("open");

                });


            
        });
    }
    else {
        $("#" + id + "Cari").select2({
            placeholder: "-- Pilih " + nama + " --",
            width: "100%",
            ajax: {
                url: "/Admin/MasterMapingCapaianPembelajaran/getLookupByTipe",
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
            clearValueOnForm();
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
                    url: "/Admin/MasterMapingCapaianPembelajaran/GetFakultas",
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
                clearValueOnForm();
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
                        url: "/Admin/MasterMapingCapaianPembelajaran/GetProdiByFakultas",
                        dataType: 'json',
                        method: "POST",
                        delay: 250,
                        cache: false,
                        data: function (params) {
                            return {
                                Search: params.term || "",
                                JenjangStudi: $('#' + id + "Cari").val(),
                                IdFakultas: $('#fakultasCari').val()
                            };
                        },
                        processResults: function (data, params) {
                            return {
                                //results: $.map(data, function (item) { return { id: item.IDProdi, value: item.NamProdi, text: item.NamProdi+' - '+item.Lokasi, lokasi: item.Lokasi } })
                                results: $.map(data, function (item) { return { id: item.Nama, value: item.Nama, text: item.Nama } })
                            };
                        },
                    }
                });

                $("#prodiCari").change(function () {

                    //console.log("select2");
                    clearValueOnForm();
                    buttonHandler("close");
                    $("#lokasiCari").empty();
                    $("#prodiIdCari").val('');
                    $("#matakuliahCari").empty();
                    $("#matakuliahCari").prop("disabled", true);

                    //$("#lokasiCari").prop("disabled", false);

                    //$("#kampusCari").val('');

                    clearValueOnForm();
                    //$("#prodiIdCari").val($("#lokasiCari").val());

                    //$("#kampusCari").removeAttr('value');
                    //var kampus = $(this).find(":selected").text();
                    //console.log(kampus);
                    //$("#kampusCari").val(kampus);

                    buttonHandler("close");
                    $("#matakuliahCari").empty();
                    $("#matakuliahCari").prop("disabled", false);
                    //$("#matakuliahNamaCari").val('');

                    //getLocationByPodiID();

                    //var start = 0;
                    //var end = 0;
                    var pageLength = 0;
                    $('#matakuliahCari').select2({
                        placeholder: "-- Pilih Mata Kuliah --",
                        "proccessing": true,
                        "serverSide": true,
                        //multiple: true,
                        width: "100%",
                        ajax: {
                            url: "/Admin/MasterMapingCapaianPembelajaran/GetMataKuliah",
                            type: 'POST',
                            dataType: 'json',

                            data: function (params) {

                                return {
                                    take: 10,
                                    searchBy: params.term || "",
                                    skip: params.page || 1,
                                    idProdi: $('#prodiCari').val(),
                                    idFakultas: $('#fakultasCari').val()
                                };
                            },

                            processResults: function (data, params) {

                                var page = params.page - 1 || 1;
                                pageLength = pageLength + data.length;

                                return {
                                    results: $.map(data, function (item) { return { id: item.id, value: item.id, text: item.kode + ' - ' + item.text, name: item.text, kode: item.kode } }),
                                    pagination: {
                                        more: data.length > 0 && data.length == 10
                                    }
                                }
                            },
                        }
                    });



                    $("#matakuliahCari").change(function () {

                        $("#matakuliahNamaCari").removeAttr('value');
                        var matakuliah = $(this).select2('data')[0].name;
                        var matakuliahkode = $(this).select2('data')[0].kode;
                        $("#matakuliahNamaCari").val(matakuliah);

                        $("#matakuliahKodeCari").val(matakuliahkode);
                        $("#matakuliahIdCari").val($(this).val());

                        buttonHandler("open");

                    });


                });
            });
        });
    }



}


//--<> responsive
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





$(document).ready(function () {
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


//---<> helper
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