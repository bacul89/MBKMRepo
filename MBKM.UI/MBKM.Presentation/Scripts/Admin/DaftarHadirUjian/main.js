var datatable = null;
$(document).ready(function () {
    var getFakultas = $("#fakultasCari").val();
    var getProdi = $("#prodiCari").val();

    if (getFakultas != null && getProdi == null) {
        //$("#fakultasCari").empty();
        $("#prodiCari").empty();
        /*$("#jenjangCari").prop("disabled", true);*/
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
        loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");
    }
    else if (getFakultas != null && getProdi != null) {
        /*$("#jenjangCari").prop("disabled", true);*/
        $("#fakultasCari").prop("disabled", true);
        $("#prodiCari").prop("disabled", true);
        $("#lokasiCari").prop("disabled", true);
        $("#matakuliahCari").prop("disabled", true);
        $("#seksiCari").prop("disabled", true);

        $("#jenjangCari").select2({
            placeholder: "-- Pilih Jenjang Studi --"
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
        loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");
    }
    else {
        $("#fakultasCari").empty();
        $("#prodiCari").empty();
        /*$("#jenjangCari").prop("disabled", true);*/
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
        loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");
    }

});

function convertMilisecondToDate(value) {
    var num = parseInt(value.match(/\d+/), 10)
    var date = new Date(num);
    var result = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return result;

}

//---<> search
$('#tahunAjaranCari').select2({

    placeholder: "-- Pilih Tahun Ajaran --",
    "proccessing": true,
    "serverSide": true,
    //multiple: true,
    width: "100%",
    ajax: {
        url: "/Admin/DaftarHadirUjian/GetSemesterAll2",
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

    $("#jenjangCari").prop("disabled", false);
    loadJenjangStudi("JenjangStudi", "jenjang", "Jenjang Studi");

    /*$("#fakultasCari").prop("disabled", true);*/
    $("#fakultasCari").prop("disabled", true);
    $("#prodiCari").prop("disabled", true);
    $("#lokasiCari").prop("disabled", true);
    $("#matakuliahCari").prop("disabled", true);
    $("#seksiCari").prop("disabled", true);

    $("#jenjangCari").empty();
    

    $("#lokasiCari").empty();
    $("#matakuliahCari").empty();
    $("#seksiCari").empty();



});



function loadJenjangStudi(tipe, id, nama) {

    var getFakultas = $("#fakultasCari").val();
    var getProdi = $("#prodiCari").val();

    //console.log(getFakultas);

    if (getFakultas != null  && getProdi == null) {
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
                            //console.log(data);
                            return {
                                results: $.map(data, function (item) { return { id: item.Nama, value: item.Nama, text: item.Nama } })
                            };
                        },
                    }
                });

                $("#prodiCari").change(function () {

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
                            //"proccessing": true,
                            //"serverSide": true,
                            //multiple: true,
                            width: "100%",
                            ajax: {
                                //url: "/Admin/DaftarHadirUjian/GetMataKuliah",
                                url: "/Admin/DaftarHadirUjian/GetMataKuliahFlag",
                                type: 'POST',
                                dataType: 'json',

                                data: function (params) {
                                    

                                    return {
                                        searchBy: params.term || "",
                                        idProdi: $('#prodiCari').val(),
                                        idProdi: $('#lokasiCari').select2('data')[0].id,
                                        idFakultas: $('#fakultasCari').val(),
                                        jenjangStudi: $('#jenjangCari').val(),
                                        strm: $('#tahunAjaranCari').select2('data')[0].id,
                                        lokasi: $('#lokasiCari').select2('data')[0].value
                                    };
                                },
                                processResults: function (data, params) {

                                    /*var page = params.page - 1 || 1;
                                    pageLength = pageLength + data.length;

                                    return {
                                        results: $.map(data, function (item) { return { id: item.MataKuliahID, value: item.MataKuliahID, text: item.KodeMataKuliah + ' - ' + item.NamaMataKuliah, name: item.NamaMataKuliah, kode: item.KodeMataKuliah } }),
                                        pagination: {
                                            more: data.length > 0 && data.length == 10
                                        }
                                    }*/
                                    

                                        return {
                                            results: $.map(data, function (item) { return { id: item.MataKuliahID, value: item.MataKuliahID, text: item.KodeMataKuliah + ' - ' + item.NamaMataKuliah, name: item.NamaMataKuliah, kode: item.KodeMataKuliah } })
                                        };

                             


                                },


                            }
                        });



                        $("#matakuliahCari").change(function () {
                            buttonHandler("open");
                            $("#seksiCari").empty();
                            $("#seksiCari").prop("disabled", false);

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
                                    url: "/Admin/DaftarHadirUjian/GetSection",
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
    }
    else if (getFakultas != null && getProdi != null) {


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
            buttonHandler("close");
            $("#matakuliahCari").empty();
            $("#seksiCari").empty();

            $("#lokasiCari").prop("disabled", false);
            $("#matakuliahCari").prop("disabled", true);
            $("#seksiCari").prop("disabled", true);

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
                    //"proccessing": true,
                    //"serverSide": true,
                    //multiple: true,
                    width: "100%",
                    ajax: {
                        //url: "/Admin/DaftarHadirUjian/GetMataKuliah",
                        url: "/Admin/DaftarHadirUjian/GetMataKuliahFlag",
                        type: 'POST',
                        dataType: 'json',

                        data: function (params) {
                            // console.log('page : ' + params.page);
                            // console.log('p number : ' + (params.page - 1) * 10);

                            return {
                                //take: 10,
                                searchBy: params.term || "",
                                //skip: params.page || 0,
                                //idProdi: $('#lokasiCari').select2('data')[0].id,
                                idProdi: $('#prodiCari').val(),
                                idProdi: $('#lokasiCari').select2('data')[0].id,
                                idFakultas: $('#fakultasCari').val(),
                                jenjangStudi: $('#jenjangCari').val(),
                                strm: $('#tahunAjaranCari').select2('data')[0].id,
                                lokasi: $('#lokasiCari').select2('data')[0].value
                            };
                        },

                        processResults: function (data, params) {

                            //var page = params.page - 1 || 1;
                            //pageLength = pageLength + data.length;
                            return {
                                results: $.map(data, function (item) { return { id: item.MataKuliahID, value: item.MataKuliahID, text: item.KodeMataKuliah + ' - ' + item.NamaMataKuliah, name: item.NamaMataKuliah, kode: item.KodeMataKuliah } })
                            };


                            /*return {
                                results: $.map(data, function (item) { return { id: item.MataKuliahID, value: item.MataKuliahID, text: item.KodeMataKuliah + ' - ' + item.NamaMataKuliah, name: item.NamaMataKuliah, kode: item.KodeMataKuliah } }),
                                pagination: {
                                    more: data.length > 0 && data.length == 10
                                }
                            }*/
                        },

                        
                    }
                });



                $("#matakuliahCari").change(function () {
                    buttonHandler("open");
                    $("#seksiCari").empty();
                    $("#seksiCari").prop("disabled", false);

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
                            url: "/Admin/DaftarHadirUjian/GetSection",
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
    } else {

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

            $("#fakultasCari").prop("disabled", false);
            $("#prodiCari").prop("disabled", true);
            $("#lokasiCari").prop("disabled", true);
            $("#matakuliahCari").prop("disabled", true);
            $("#seksiCari").prop("disabled", true);

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
                            //console.log(data);
                            return {
                                results: $.map(data, function (item) { return { id: item.Nama, value: item.Nama, text: item.Nama } })
                            };
                        },
                    }
                });

                $("#prodiCari").change(function () {

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
                                //url: "/Admin/DaftarHadirUjian/GetMataKuliah",
                                url: "/Admin/DaftarHadirUjian/GetMataKuliahFlag",
                                type: 'POST',
                                dataType: 'json',

                                data: function (params) {
                                    // console.log('page : ' + params.page);
                                    // console.log('p number : ' + (params.page - 1) * 10);

                                    return {
                                        //take: 10,
                                        searchBy: params.term || "",
                                        //skip: params.page || 0,
                                        //idProdi: $('#lokasiCari').select2('data')[0].id,
                                        idProdi: $('#prodiCari').val(),
                                        idProdi: $('#lokasiCari').select2('data')[0].id,
                                        idFakultas: $('#fakultasCari').val(),
                                        jenjangStudi: $('#jenjangCari').val(),
                                        strm: $('#tahunAjaranCari').select2('data')[0].id,
                                        lokasi: $('#lokasiCari').select2('data')[0].value
                                    };
                                },

                                processResults: function (data, params) {
                                    return {
                                        results: $.map(data, function (item) { return { id: item.MataKuliahID, value: item.MataKuliahID, text: item.KodeMataKuliah + ' - ' + item.NamaMataKuliah, name: item.NamaMataKuliah, kode: item.KodeMataKuliah } })
                                    };


                                    /*var page = params.page - 1 || 1;
                                    pageLength = pageLength + data.length;

                                    return {
                                        results: $.map(data, function (item) { return { id: item.MataKuliahID, value: item.MataKuliahID, text: item.KodeMataKuliah + ' - ' + item.NamaMataKuliah, name: item.NamaMataKuliah, kode: item.KodeMataKuliah } }),
                                        pagination: {
                                            more: data.length > 0 && data.length == 10
                                        }
                                    }*/
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
                            buttonHandler("open");
                            $("#seksiCari").empty();
                            $("#seksiCari").prop("disabled", false);

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
                                    url: "/Admin/DaftarHadirUjian/GetSection",
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

}

function buttonHandler(param) {
    if (param == "open") {
        $('#cari, #add').removeAttr("disabled");
        $('#cari, #add').removeAttr("title");
    } else if (param == "close") {
        $('#cari, #add').attr("disabled", "disabled");
        $('#cari').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
        $('#add').attr("title", "Mohon untuk melengkapi form pencarian terlebih dahulu!");
    } else {
        console.log("error : access");
    }
}


//---<> datatable
$('#cari').click(function () {
    reloadDatatable();
});

function reloadDatatable() {
    var base_url = window.location.origin;

    var seksi = $('#seksiCari').val();
    if (seksi== null) {
        seksi = "";
    }
    var variable =
        'idProdi=' + $('#lokasiCari').select2('data')[0].id +
        '&lokasi=' + $('#lokasiCari').select2('data')[0].value +
        '&idFakultas=' + $('#fakultasCari').val() +
        '&jenjangStudi=' + $('#jenjangCari').val() +
        '&idMatakuliah=' + $('#matakuliahCari').val() +
        '&seksi=' + seksi +
        '&strm=' + $('#tahunAjaranCari').val();

    datatable.destroy();
    datatable = $('#table-data-daftar-hadir-ujian').DataTable({
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0,
        }],
        "proccessing": true,
        "serverSide": true,
        "order": [[1, 'asc']],
        "ajax": {
            url: '/Admin/DaftarHadirUjian/SearchList?' + variable,
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
                               
                                <a javascript:void(0) onclick="printDHU(${data})"  style="color:black" target="_blank"> <i class="fas fa-print coral" ></i></a>
                            </div>
                        </div>`;
                }//javascript:void(0) // onclick="printDHU()" /* <a href="${base_url}/Admin/DaftarHadirUjian/PrintDHU/${data}"  style="color:black" target="_blank"> <i class="fas fa-print coral" ></i></a>*/
            },
            {
                "data": null,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": "STRM",
                "name": "STRM",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + $('#tahunAjaranCari').select2('data')[0].text + '</div>';
                }
            },
            {
                "data": "JenjangStudi",
                "name": "JenjangStudi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + $('#jenjangCari').select2('data')[0].text + '</div>';
                }
            },
            {
                "data": "NamaProdi",
                "name": "NamaProdi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                "data": "KodeMatkul",
                "name": "KodeMatkul",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                "data": "NamaMatkul",
                "name": "NamaMatkul",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                "data": "SKS",
                "name": "SKS",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            /*{
                //"title": "Email",
                "data": "TipeUjian",
                "name": "TipeUjian",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "TanggalUjian",
                "name": "TanggalUjian",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + convertMilisecondToDate(data) + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "JamAkhir",
                "name": "JamAkhir",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + row.JamMulai + ' - ' + row.JamAkhir + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "RuangUjian",
                "name": "RuangUjian",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },*/

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
        //]

    });

    setTimeout(function () {
        controlTableResponsive();
    }, 300);
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


function controlTableResponsive() {
    if (datatable.data().count() > 0) {
        $('#table-data-daftar-hadir-ujian').addClass('table-responsive');
    } else {
        $('#table-data-daftar-hadir-ujian').removeClass('table-responsive');
    }
}