
var datatable = null;

$(document).ready(function () {

    var getFakultas = $("#fakultasCari").val();
    var getProdi = $("#prodiCari").val();

    //console.log(getProdi);
    //console.log(getFakultas);

    if (getFakultas != null && getProdi == null) {
        console.log('here');
        $("#lokasiCari").prop("disabled", true);
        $("#tahunAjaranCari").prop("disabled", false);

        $("#prodiCari").select2({
            placeholder: "-- Pilih Program Studi --"
        });
        $("#jenjangCari").select2({
            placeholder: "-- Pilih Jenjang Studi --"
        });
        $("#lokasiCari").select2({
            placeholder: "-- Pilih Lokasi --"
        });

        loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");

        datatable = $('#table-data-jadwal-kuliah').DataTable({
            paging: false,
            ordering: false,
            info: false
        });

        buttonHandler("close");
        $("#prodiIdCari").val('');
        $("#lokasiCari").empty();
        $("#matakuliahCari").empty();
        $("#jenjangCari").empty();
        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);
        $("#matakuliahCari").prop("disabled", true);
        $("#fakultasCari").prop("disabled", true);

    }
    else if (getFakultas != null && getProdi != null) {
        $("#lokasiCari").prop("disabled", true);
        $("#tahunAjaranCari").prop("disabled", false);
        $("#jenjangCari").select2({
            placeholder: "-- Pilih Jenjang Studi --"
        });
        $("#lokasiCari").select2({
            placeholder: "-- Pilih Lokasi --"
        });
        loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");

        datatable = $('#table-data-jadwal-kuliah').DataTable({
            paging: false,
            ordering: false,
            info: false
        });

        buttonHandler("close");
        // $("#prodiIdCari").val('');
        $("#lokasiCari").empty();
        $("#matakuliahCari").empty();
        $("#jenjangCari").empty();
        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);
        $("#matakuliahCari").prop("disabled", true);
        $("#fakultasCari").prop("disabled", true);

    }
    else {

        $("#fakultasCari").select2({
            placeholder: "-- Pilih Fakultas --"
        });
        $("#prodiCari").select2({
            placeholder: "-- Pilih Program Studi --"
        });
        $("#lokasiCari").prop("disabled", true);
        $("#tahunAjaranCari").prop("disabled", false);
        $("#jenjangCari").select2({
            placeholder: "-- Pilih Jenjang Studi --"
        });
        $("#lokasiCari").select2({
            placeholder: "-- Pilih Lokasi --"
        });
        loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");

        datatable = $('#table-data-jadwal-kuliah').DataTable({
            paging: false,
            ordering: false,
            info: false
        });

        buttonHandler("close");
        $("#prodiIdCari").val('');
        $("#lokasiCari").empty();
        $("#matakuliahCari").empty();
        $("#jenjangCari").empty();
        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);
        $("#matakuliahCari").prop("disabled", true);
        $("#fakultasCari").prop("disabled", true);

    }

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
$('#tahunAjaranCari').select2({
    placeholder: "-- Pilih Tahun Ajaran --",
    "proccessing": true,
    "serverSide": true,
    //multiple: true,
    width: "100%",
    ajax: {
        url: "/Admin/JadwalKuliah/GetSemesterAll2",
        type: 'POST',
        dataType: 'json',
        data: function (params) {

            return {
                take: 10,
                search: params.term || "",
                skip: (params.page - 1) * 10 || 0,
            };
        },
        processResults: function (data, params) {

            var page = params.page - 1 || 1;

            return {
                results: $.map(data, function (item) { return { id: item.Nilai, value: item.Nilai, text: item.Nama } }),
                pagination: {
                    more: (page * 10) <= data.length
                }
            }

        },
    }
});
$("#tahunAjaranCari").change(function () {
    var checkFakultas = $('#fakultasCari').prop("disabled");
    var checkProdi = $('#prodiCari').prop("disabled");

    if (checkFakultas == false) {
        $("#fakultasCari").empty();
    }

    if (checkProdi == false) {
        $("#prodiCari").empty();
        $("#prodiIdCari").val('');
    }

    buttonHandler("close");
    $("#jenjangCari").empty();
    //$("#fakultasCari").empty();
    //$("#prodiCari").empty();
    //$("#prodiIdCari").val('');
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

function loadJenjangStudi(tipe, id, nama) {

    var getFakultas = $("#fakultasCari").val();
    var getProdi = $("#prodiCari").val();

    //console.log(getFakultas);
    //console.log(getProdi);

    if (getFakultas != null && getProdi == null) {
        console.log('di here');
        $("#" + id + "Cari").select2({
            placeholder: "-- Pilih " + nama + " --",
            width: "100%",
            ajax: {
                url: "/Admin/JadwalKuliah/getLookupByTipe",
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
                    url: "/Admin/JadwalKuliah/GetProdiByFakultas",
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
                        url: "/Admin/JadwalKuliah/GetLokasiByProdi",
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
    } else if (getFakultas != null && getProdi != null) {
        $("#" + id + "Cari").select2({
            placeholder: "-- Pilih " + nama + " --",
            width: "100%",
            ajax: {
                url: "/Admin/JadwalKuliah/getLookupByTipe",
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
            $("#prodiIdCari").val('');
            $("#lokasiCari").empty();
            $("#matakuliahCari").empty();
            $("#lokasiCari").prop("disabled", true);
            $("#matakuliahCari").prop("disabled", true);


            buttonHandler("close");
            $("#lokasiCari").empty();
            $("#prodiIdCari").val('');
            $("#matakuliahCari").empty();
            $("#matakuliahCari").prop("disabled", true);

            $("#lokasiCari").prop("disabled", false);


            //console.log($('#prodiCari').val());
            //console.log($('#' + id + "Cari").val());

            $("#lokasiCari").select2({
                placeholder: "-- Pilih Lokasi --",
                width: "100%",
                ajax: {
                    url: "/Admin/JadwalKuliah/GetLokasiByProdi",
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

                buttonHandler("open");
                $("#prodiIdCari").val($("#lokasiCari").val());

                $("#kampusCari").removeAttr('value');
                var kampus = $(this).find(":selected").text();
                $("#kampusCari").val(kampus);

            });

        });

    } else {
        $("#" + id + "Cari").select2({
            placeholder: "-- Pilih " + nama + " --",
            width: "100%",
            ajax: {
                url: "/Admin/JadwalKuliah/getLookupByTipe",
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
                    url: "/Admin/JadwalKuliah/GetFakultas",
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
                        url: "/Admin/JadwalKuliah/GetProdiByFakultas",
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
                            url: "/Admin/JadwalKuliah/GetLokasiByProdi",
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


