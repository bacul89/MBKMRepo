
var datatable = null;

$(document).ready(function () {
    //$("#jenjangCari").prop("disabled", true);
    $("#fakultasCari").prop("disabled", true);
    $("#prodiCari").prop("disabled", true);
    $("#lokasiCari").prop("disabled", true);
    $("#tahunAjaranCari").prop("disabled", false);
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
    loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");
    /*$("#tahunAjaranCari").select2({
        placeholder: "-- Pilih Tahun Ajaran --"
    });*/



    datatable = $('#table-data-jadwal-kuliah').DataTable({
        paging: false,
        ordering: false,
        info: false
    });
    //$("#table-data-master-mapping-cpl_filter").hide();
    buttonHandler("close");
    $("#fakultasCari").empty();
    $("#prodiCari").empty();
    $("#prodiIdCari").val('');
    $("#lokasiCari").empty();
    $("#matakuliahCari").empty();
    //$("#tahunAjaranCari").empty();
    $("#jenjangCari").empty();
    $("#prodiCari").prop("disabled", true);
    $("#lokasiCari").prop("disabled", true);
    $("#matakuliahCari").prop("disabled", true);
    //$("#tahunAjaranCari").prop("disabled", true);
    $("#fakultasCari").prop("disabled", true);
    $('#tahunAjaranCari').select2({


        placeholder: "-- Pilih Tahun Ajaran --",
        "proccessing": true,
        "serverSide": true,
        //multiple: true,
        width: "100%",
        ajax: {
            url: "/Admin/ManageJadwalKuliah/GetSemesterAll2",
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
        buttonHandler("close");
        $("#jenjangCari").empty();
        $("#fakultasCari").empty();
        $("#prodiCari").empty();
        $("#prodiIdCari").val('');
        $("#lokasiCari").empty();
        $("#matakuliahCari").empty();
        //$("#tahunAjaranCari").empty();

        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);
        $("#matakuliahCari").prop("disabled", true);
        //$("#tahunAjaranCari").prop("disabled", true);
        $("#fakultasCari").prop("disabled", true);
        $("#jenjangCari").prop("disabled", false);


    });


});


function buttonHandler(param) {
    if (param == "open") {
        $('#cari').removeAttr("disabled");
        $('#cari').removeAttr("title");
    } else if (param == "close") {
        $('#cari, #add').attr("disabled", "disabled");
        //$().attr("disabled", "disabled");
        $('#cari').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
        $('#add').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
    } else {
        console.log("error : access");
    }
}

/* search */
function loadJenjangStudi(tipe, id, nama) {
    $("#" + id + "Cari").select2({
        placeholder: "-- Pilih " + nama + " --",
        width: "100%",
        ajax: {
            url: "/Admin/ManageJadwalKuliah/getLookupByTipe",
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
        //$("#tahunAjaranCari").empty();
        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);
        $("#matakuliahCari").prop("disabled", true);
        //$("#tahunAjaranCari").prop("disabled", true);
        $("#fakultasCari").prop("disabled", false);
        $("#fakultasCari").select2({
            placeholder: "-- Pilih Fakultas --",
            width: "100%",
            ajax: {
                url: "/Admin/ManageJadwalKuliah/GetFakultas",
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

            // $("#tahunAjaranCari").empty();
            //$("#tahunAjaranCari").prop("disabled", true);
            $("#prodiCari").select2({
                placeholder: "-- Pilih Program Studi --",
                width: "100%",
                ajax: {
                    url: "/Admin/ManageJadwalKuliah/GetProdiByFakultas",
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

                //$("#tahunAjaranCari").empty();
                //$("#tahunAjaranCari").prop("disabled", true);

                //$("#kampusCari").val('');
                $("#lokasiCari").select2({
                    placeholder: "-- Pilih Lokasi --",
                    width: "100%",
                    ajax: {
                        url: "/Admin/ManageJadwalKuliah/GetLokasiByProdi",
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

                    buttonHandler("open");
                    $("#prodiIdCari").val($("#lokasiCari").val());

                    $("#kampusCari").removeAttr('value');
                    var kampus = $(this).find(":selected").text();
                    //console.log(kampus);
                    $("#kampusCari").val(kampus);
                    //$("#tahunAjaranCari").empty();
                    //$("#tahunAjaranCari").prop("disabled", false);


                    //getLocationByPodiID();


                });

            });
        });
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


