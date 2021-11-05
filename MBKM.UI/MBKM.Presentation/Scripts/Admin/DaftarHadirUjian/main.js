var datatable = null;
$(document).ready(function () {
    $("#jenjangCari").prop("disabled", true);
    $("#fakultasCari").prop("disabled", true);
    $("#fakultasCari").prop("disabled", true);
    $("#prodiCari").prop("disabled", true);
    $("#lokasiCari").prop("disabled", true);
    $("#matakuliahCari").prop("disabled", true);
    $("#seksiCari").prop("disabled", true);
    $("#jenjangCari").select2({
        placeholder: "-- Pilih Jenjang Studi --"
    });

    

    $("#fakultasCari").select2({
        placeholder: "-- Pilih Fakultas --"
    });
    $("#prodiCari").select2({
        placeholder: "-- Pilih Program Studi --"
    });
    $("#lokasiCari").select2({
        placeholder: "-- Pilih Lokasi --"
    });

    $("#matakuliahCari").select2({
        placeholder: "-- Pilih Mata Kuliah --"
    });
    $("#seksiCari").select2({
        placeholder: "-- Pilih Seksi --"
    });


    datatable = $('#table-data-daftar-hadir-ujian').DataTable();


});

//---<> search
$('#tahunAjaranCari').select2({

    placeholder: "-- Pilih Tahun Ajaran --",
    "proccessing": true,
    "serverSide": true,
    //multiple: true,
    width: "100%",
    ajax: {
        url: "/DaftarHadirUjian/GetSemesterAll",
        type: 'POST',
        dataType: 'json',
        //quietMillis: 50,
        data: function (params) {

            return {

                //search: params.term,
                //instansi: $('#namaUniversitas').val(),
                //length: params.length || 10,
                //skip: params.skip || 0

                take: 10,
                search: params.term || "",
                skip: (params.page - 1) * 10 || 0,
                // searchBy: params.term,

            };
        },
        processResults: function (data, params) {

            var page = params.page - 1 || 1;
            //var pageLength = pageLength + data.length || 10;
            //console.log('page : ' + params.page);
            //console.log(page);
            //console.log('------------------------');
            //console.log(page * 10);
            //console.log(pageLength);

            return {
                results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } }),
                pagination: {
                    more: (page * 10) <= data.length
                }
            }

        },
    }
});
$("#tahunAjaranCari").change(function () {
    $("#jenjangCari").prop("disabled", false);
    loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");


});



function loadJenjangStudi(tipe, id, nama) {
    $("#" + id + "Cari").select2({
        placeholder: "-- Pilih " + nama + " --",
        width: "100%",
        ajax: {
            url: "/Admin/DaftarHadirUjian/getLookupByTipe",
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
        //clearValueOnForm();
        buttonHandler("close");
        $("#fakultasCari").empty();
        $("#prodiCari").empty();
        $("#prodiIdCari").val('');
        $("#lokasiCari").empty();
        $("#matakuliahCari").empty();
        $("#seksiCari").empty();
        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);        
        $("#matakuliahCari").prop("disabled", true);
        $("#seksiCari").prop("disabled", true);
        $("#fakultasCari").prop("disabled", false);
        $("#fakultasCari").select2({
            placeholder: "-- Pilih Fakultas --",
            width: "100%",
            ajax: {
                url: "/Admin/DaftarHadirUjian/GetFakultas",
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
            //clearValueOnForm();
            buttonHandler("close");
            $("#prodiCari").empty();
            $("#prodiIdCari").val('');
            $("#lokasiCari").empty();
            $("#matakuliahCari").empty();
            $("#seksiCari").empty();
            $("#lokasiCari").prop("disabled", true);
            $("#matakuliahCari").prop("disabled", true);
            $("#seksiCari").prop("disabled", true);

            $("#prodiCari").prop("disabled", false);
            /*$("#prodiCari").select2({
                placeholder: "-- Pilih Program Studi --",
                width: "100%",
                ajax: {
                    url: "s",
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
                            results: $.map(data, function (item) { return { id: item.IDProdi, value: item.NamProdi, text: item.NamProdi + ' - ' + item.Lokasi, lokasi: item.Lokasi } })
                        };
                    },
                }
            });*/

            $("#prodiCari").select2({
                placeholder: "-- Pilih Program Studi --",
                width: "100%",
                ajax: {
                    url: "/Admin/DaftarHadirUjian/GetProdiByFakultas",
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
                        console.log(data);
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

                //$("#tahunAjaranCari").empty();
                //$("#tahunAjaranCari").prop("disabled", true);

                //$("#kampusCari").val('');
                $("#lokasiCari").select2({
                    placeholder: "-- Pilih Lokasi --",
                    width: "100%",
                    ajax: {
                        url: "/Admin/DaftarHadirUjian/GetLokasiByProdi",
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

                            //console.log(data);
                            return {
                                results: $.map(data, function (item) { return { id: item.ID, value: item.Kampus, text: item.Kampus } })
                            };
                        },
                    }
                });



                $("#lokasiCari").change(function () {
                    //console.log("select2");
                    //clearValueOnForm();
                    buttonHandler("close");
                    //$("#lokasiCari").empty();
                    //$("#prodiIdCari").val('');
                    $("#matakuliahCari").empty();
                    $("#seksiCari").empty();
                    $("#matakuliahCari").prop("disabled", true);

                    //$("#lokasiCari").prop("disabled", false);

                    //$("#kampusCari").val('');

                    //clearValueOnForm();
                    //$("#prodiIdCari").val($("#lokasiCari").val());

                    //$("#kampusCari").removeAttr('value');
                    //var kampus = $(this).find(":selected").text();
                    //console.log(kampus);
                    //$("#kampusCari").val(kampus);

                    buttonHandler("close");
                    $("#matakuliahCari").empty();
                    $("#matakuliahCari").prop("disabled", false);
                    $("#seksiCari").prop("disabled", true);
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
                            url: "/Admin/DaftarHadirUjian/GetMataKuliah",
                            type: 'POST',
                            dataType: 'json',

                            data: function (params) {
                                // console.log('page : ' + params.page);
                                // console.log('p number : ' + (params.page - 1) * 10);

                                return {
                                    take: 10,
                                    searchBy: params.term || "",
                                    skip: params.page || 1,                                    
                                    //idProdi: $('#lokasiCari').select2('data')[0].id,
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

                            /*
                               data: function (params) {
    
    
                                params.length = 10;
                                if (params.page > 1) {
                                    params.skip = params.page * params.length - (params.length - 1);
                                    start = params.skip - 1;
                                    end = params.skip - 1 + params.length;
                                } else {
                                    start = 0;
                                    end = 10;
                                }
    
                                return {
    
                                    //search: params.term,
                                    //instansi: $('#namaUniversitas').val(),
                                    //length: params.length || 10,
                                    //skip: params.skip || 0
    
    
                                     page: params.page || 0,
                                     skip: params.skip || 10,
                                     take: params.length || 10,
                                     searchBy: params.term,
                                     idProdi: $('#prodiIdCari').val(),
                                     idFakultas: $('#fakultasCari').val()
    
                                };
                            },
                            processResults: function (data, page) {
    
    
                                //console.log(data);
                                //console.log(data[0]);
                                //console.log(data.slice(start, end));
                                //console.log("st"+start);
                                //console.log("ed"+end);
                                //console.log(data.slice(start, end));
    
                                return {
                                    results: $.map(data.slice(start, end), function (item) { return { id: item.id, value: item.id, text: item.text, title: item.kode } }),
                                    pagination: {
                                        more: true
                                    }
                                }
    
                            },*/
                        }
                    });



                    $("#matakuliahCari").change(function () {
                        $("#seksiCari").prop("disabled", false);
                        /*
                            dataParam.NamaProdi = $('#prodiCari').val();
                            dataParam.lokasi = $('#lokasiCari').val();
                            dataParam.MataKuliahID = $('#matakuliahCari').val();
                        */

                        $("#matakuliahNamaCari").removeAttr('value');
                        var matakuliah = $(this).select2('data')[0].name;
                        var matakuliahkode = $(this).select2('data')[0].kode;
                        $("#matakuliahNamaCari").val(matakuliah);





                        $("#matakuliahKodeCari").val(matakuliahkode);
                        $("#matakuliahIdCari").val($(this).val());

                       


                        $('#seksiCari').select2({

                            placeholder: "-- Pilih Seksi --",
                            "proccessing": true,
                            "serverSide": true,
                            //multiple: true,
                            width: "100%",
                            ajax: {
                                url: "/DaftarHadirUjian/GetSection",
                                type: 'POST',
                                dataType: 'json',
                                //quietMillis: 50,
                                data: function (params) {

                                },
                                processResults: function (data, params) {

                                    return {
                                        results: $.map(data, function (item) { return { id: item.Nama, value: item.Nama, text: item.Nama } })
                                    };

                                },
                            }
                        });
                        $("#seksiCari").change(function () {
                            buttonHandler("open");
                            //$("#matakuliahCari").empty();
                            //$("#jenjangCari").prop("disabled", false);
                            //loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");


                        });

                    });


                });

            });



        });
    });
}

function buttonHandler(param) {
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


//---<> datatable
$('#cari').click(function () {

    console.log('halloo');
    reloadDatatable();
});

function reloadDatatable() {
    console.log($('#lokasiCari').select2('data')[0].id);
    console.log($('#lokasiCari').select2('data')[0].Kampus);
    console.log($('#lokasiCari').select2('data')[0].kampus);
    console.log($('#lokasiCari').val());

    var variable =
        'idProdi=' + $('#lokasiCari').select2('data')[0].id +
        '&lokasi=' + $('#lokasiCari').select2('data')[0].value +
        '&idFakultas=' + $('#fakultasCari').val() +
        '&jenjangStudi=' + $('#jenjangCari').val() +
        '&idMatakuliah=' + $('#matakuliahCari').val() +
        '&seksi=' + $('#seksiCari').val() +
        '&strm=' + $('#tahunAjaranCari').val();

    datatable.destroy();
    datatable = $('#table-data-daftar-hadir-ujian').DataTable({
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
            url: '/Admin/DaftarHadirUjian/SearchList?' + variable,
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

        //ID

        "columns": [
            {
                "title": "Action",
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="printDHU('${data}')"> <i class="fas fa-print coral" ></i></a>
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
                "data": "STRM",
                "name": "STRM",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nama",
                "data": "JenjangStudi",
                "name": "JenjangStudi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "NamaProdi",
                "name": "NamaProdi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "KodeMatkul",
                "name": "KodeMatkul",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "NamaMatkul",
                "name": "NamaMatkul",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "ClassSection",
                "name": "ClassSection",
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




//---<> responsive
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
