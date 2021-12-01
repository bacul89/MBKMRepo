
var dMasterMapingCapaianPembelajaran = {}

function getValueOnForm() {
    dMasterMapingCapaianPembelajaran.IDMataKUliah = $('input[name=inp_id_mata_kuliah]').val();
    dMasterMapingCapaianPembelajaran.NamaMataKuliah = $('input[name=inp_mata_kuliah]').val();
    dMasterMapingCapaianPembelajaran.KodeMataKuliah = $('input[name=inp_kode_mata_kuliah]').val();
    dMasterMapingCapaianPembelajaran.MasterCapaianPembelajaranID = $('#inp_kode_capaian_pembelajaran').val();
    dMasterMapingCapaianPembelajaran.Kelompok = $('#inp_kelompok').val();
    dMasterMapingCapaianPembelajaran.IsActive = $('input[name=inp_status]:checked').val();

    //console.log(dMasterMapingCapaianPembelajaran);
    //dMasterMapingCapaianPembelajaran.IsDeleted =
}

function clearValueOnForm() {
/*    $('input[name=inp_id_mata_kuliah]').val('');
    $('input[name=inp_mata_kuliah]').val('');
    $('input[name=inp_kode_mata_kuliah]').val('');*/
    $('#inp_kode_capaian_pembelajaran').empty();
    $('#inp_kelompok').empty();
    $('#inp_capaian').val('');
    //$('input[name=inp_status]').prop('checked', false);
}


function loadFromLookup(tipe, id, nama) {
    console.log(id);
    $("#" + id).select2({
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
                    Tipe: tipe
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) { return { id: item.Nama, value: item.Nama, text: item.Nama } })
                };
            },
        }
    });
    $("#" + id).change(function () {
        $('#inp_capaian').val('');
        $('#inp_kode_capaian_pembelajaran').empty();

        $("#fakultasCari").select2({
            placeholder: "-- Pilih Kode CPL --"
        });

        loadMasterCPL();


    });

}

function loadMasterCPL() {

    $('#inp_kode_capaian_pembelajaran').select2({
        placeholder: "-- Pilih Kode CPL --",
        "proccessing": true,
        "serverSide": true,
        width: "100%",
        ajax: {
            url: '/Admin/MasterMapingCapaianPembelajaran/GetMasterCPL',
            type: 'post',
            dataType: 'json',
            data: function (params) {
                return {
                    //search: params.term,
                    IDProdi: $('#prodiCari').val(),
                    IDFakultas: $('#fakultasCari').val(),
                    Kelompok: $('#inp_kelompok').val(),
                    Kelompok: $('#inp_kelompok').val(),
                    
                    /*IDProdi: "0101",
                    IDFakultas: "0001",
                    Kelompok: "SIKAP"*/

                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Kode} })
                };
            },
        }
    });
    $("#inp_kode_capaian_pembelajaran").change(function () {
        /*dataParam = {};*/
        ID = $('#inp_kode_capaian_pembelajaran').val();
/*        dataParam.IDProdi       = "0101";
        dataParam.IDFakultas    = "0001";
        dataParam.Kelompok      = "SIKAP";*/

        $.ajax({
            url: '/Admin/MasterMapingCapaianPembelajaran/GetMasterCPLByID',
            dataType: 'json',
            method: "POST",
            delay: 250,
            cache: false,
            contentType: 'application/json',
            data: { id: ID},
            success: function (e) {
                console.log(e);
                $('#inp_capaian').val(e.Capaian);
            },
            error: function (e) {
                console.log("matakuliah not found...");
            }
        })
    });

}



function loadControl(param) {

    loadFromLookup("Kelompok", "inp_kelompok", "Kelompok");
    loadMasterCPL();
    var inp_mata_kuliah = $("#matakuliahNamaCari").val();
    var inp_kode_mata_kuliah = $("#matakuliahKodeCari").val();
    var inp_id_mata_kuliah = $("#matakuliahCari").val();

/*    console.log(inp_mata_kuliah);
    console.log(inp_kode_mata_kuliah);
    console.log(inp_id_mata_kuliah);*/


    $('#inp_id_mata_kuliah').val(inp_id_mata_kuliah);
    $('#inp_kode_mata_kuliah').val(inp_kode_mata_kuliah);
    $('#inp_mata_kuliah').val(inp_mata_kuliah);


    if (param == 'Edit') {
        var getKelompok = $('#get_kelompok').val();
        console.log(getKelompok);
        $('#inp_kelompok').select2('data', { id: getKelompok, value: getKelompok, text: getKelompok });
        /*        var ID = parseInt($('#inp_id_capaian_pembelajaran').val());
                console.log(ID);
                $.ajax({
                    url: '/Admin/MasterMapingCapaianPembelajaran/GetMasterCPLByID',
                    dataType: 'json',
                    method: "GET",
                    delay: 250,
                    cache: false,
                    contentType: 'application/json',
                    data: { id: ID },
                    success: function (e) {
        
                        console.log(e);
        *//*                $('#inp_capaian').val(e.Capaian);
                        $('#inp_kode_capaian_pembelajaran').select2("val", e.ID);
                        $("#inp_kelompok").select2("val", e.Kelompok);*//*
        $('#inp_kelompok').select2('data', { id: e.Kelompok, value: e.Kelompok, text: e.Kelompok });
    },
    error: function (e) {
        console.log("matakuliah not found...");
    }
})*/
    } else {
        var inp_mata_kuliah = $("#matakuliahNamaCari").val();
        var inp_kode_mata_kuliah = $("#matakuliahKodeCari").val();
        var inp_id_mata_kuliah = $("#matakuliahCari").val();

/*        console.log(inp_mata_kuliah);
        console.log(inp_kode_mata_kuliah);
        console.log(inp_id_mata_kuliah);*/


        $('#inp_id_mata_kuliah').val(inp_id_mata_kuliah);
        $('#inp_kode_mata_kuliah').val(inp_kode_mata_kuliah);
        $('#inp_mata_kuliah').val(inp_mata_kuliah);
    }

    //console.log("hello");




}



function IndexCreateMasterMapingCapaianPembelajaran() {
    if ($('#created-master-maping-cpl').length) {
        $('#TambahMasterMapingCapaianPembelajaran').modal('show');
        loadControl("Add");
    } else {
        $.ajax({
            url: '/Admin/MasterMapingCapaianPembelajaran/ModalCreateMasterMapingCapaianPembelajaran',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                /*console.log(e);*/
                if ($('.data-content-modal').length) {
                    $('.data-content-modal').remove();
                }
                $('#modal-inner').append(e);
                $('.modal').modal('show');
                loadControl("Add");
            }
        })
    }
}

function IndexUpdateMasterMapingCapaianPembelajaran(id) {

    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterMapingCapaianPembelajaran/ModalUpdateMasterMapingCapaianPembelajaran/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
            /*$.LoadingOverlay("hide");*/
            /*console.log(e);*/
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            loadControl('Edit');


            
            
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
            $('.modal').modal('hide');
        }
    })
}

function IndexViewMasterMapingCapaianPembelajaran(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterMapingCapaianPembelajaran/ModalDetailMasterMapingCapaianPembelajaran/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
            /*console.log(e);*/
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
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
            $('.modal').modal('hide');
        }
    })

}

function PostCreate() {
    
    dMasterMapingCapaianPembelajaran = {}
    getValueOnForm();

    //console.log(getValueOnForm());

    if (dMasterMapingCapaianPembelajaran.Kelompok == null || dMasterMapingCapaianPembelajaran.MasterCapaianPembelajaranID == null) {
        Swal.fire({
            title: 'Oppss',
            icon: 'warning',
            html: 'Input data boleh kosong!',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    } else {
        $.LoadingOverlay("show");

        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/MasterMapingCapaianPembelajaran/PostDataMasterMapingCapaianPembelajaran',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dMasterMapingCapaianPembelajaran),
            contentType: 'application/json',
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
                        html: 'Data Berhasil Dihapus',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                    //datatable.ajax.reload(null, false);
                    clearValueOnForm();
                }
                reloadDatatable();
                $('.modal').modal('hide');
                
            },
            error: function (e) {
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
                $('.modal').modal('hide');
            }
        })

    }
}

function PostUpdate() {
    
    dMasterMapingCapaianPembelajaran = {}
    getValueOnForm();
    dMasterMapingCapaianPembelajaran.ID = $('#inp_id_mapping_capaian_pembelajaran').val();
    var base_url = window.location.origin;

    //console.log(dMasterMapingCapaianPembelajaran);
    if (dMasterMapingCapaianPembelajaran.Kelompok == null || dMasterMapingCapaianPembelajaran.MasterCapaianPembelajaranID == null) {
        Swal.fire({
            title: 'Oppss',
            icon: 'warning',
            html: 'Input data tidak boleh kosong!',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    } else {
        $.LoadingOverlay("show");
        $.ajax({
            url: base_url + '/Admin/MasterMapingCapaianPembelajaran/PostUpdateMasterMapingCapaianPembelajaran',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dMasterMapingCapaianPembelajaran),
            contentType: 'application/json',
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
                        html: 'Data Berhasil Dihapus',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                    //datatable.ajax.reload(null, false);
                    clearValueOnForm();

                }
                $('.modal').modal('hide');
                reloadDatatable();
            },
            error: function (e) {
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
                $('.modal').modal('hide');
            }
        })

    }

}

function DeletedMasterMapingCapaianPembelajaran(idMapping) {
    swal.fire({
        title: "Apakah anda yakin?",
        type: "warning",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {
            $.LoadingOverlay("show");
            $.ajax({
                url: '/Admin/MasterMapingCapaianPembelajaran/PostDeleteMasterMapingCapaianPembelajaran',
                type: 'post',
                data: {
                    id: idMapping
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
                            html: 'Data Berhasil Dihapus',
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

/*
    Swal.fire({
        title: "Apakah anda yakin?",
        text: "warning",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }).then((result) => {
        $.ajax({
            
            type: "POST",
            data: { id: id }
            ,
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
                        html: 'Data Berhasil Dihapus',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                    reloadDatatable();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
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
        });
    })*/

}


// searchEdit
function loadJenjangStudiEdit(tipe, id, nama) {
    $("#" + id + "Edit").select2({
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
    $("#" + id + "Edit").change(function () {
        $("#fakultasEdit").prop("disabled", false);
        $("#fakultasEdit").select2({
            placeholder: "-- Fakultas --",
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
                        JenjangStudi: $('#' + id + "Edit").val(),
                    };
                },
                processResults: function (data, params) {
                    return {
                        results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                    };
                },
            }
        });
        $("#fakultasEdit").change(function () {
            $("#prodiEdit").prop("disabled", false);
            $("#prodiEdit").select2({
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
                            JenjangStudi: $('#' + id + "Edit").val(),
                            IDFakultas: $('#fakultasEdit').val()
                        };
                    },
                    processResults: function (data, params) {
                        return {
                            results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                        };
                    },
                }
            });

            $("#prodiEdit").change(function () {

                $("#lokasiEdit").prop("disabled", false);
                $("#lokasiEdit").select2({
                    placeholder: "-- Lokasi Studi --",
                    width: "100%",
                    ajax: {
                        url: "/Admin/MasterMapingCapaianPembelajaran/GetLokasiByProdi",
                        dataType: 'json',
                        method: "POST",
                        delay: 250,
                        cache: false,
                        data: function (params) {
                            return {
                                Search: params.term || "",
                                IDProdi: $('#prodiEdit').val(),
                                JenjangStudi: $('#' + id + "Edit").val()
                            };
                        },
                        processResults: function (data, params) {
                            return {
                                results: $.map(data, function (item) { return { id: item.Kampus, value: item.Kampus, text: item.Kampus } })
                            };
                        },
                    }
                });
                $("#lokasiEdit").change(function () {

                    $("#matakuliahEdit").prop("disabled", false);
                    $("#matakuliahEdit").select2({
                        placeholder: "-- Mata Kuliah Studi --",
                        width: "100%",
                        ajax: {
                            url: "/Admin/MasterMapingCapaianPembelajaran/GetMataKuliahByProdi",
                            dataType: 'json',
                            method: "POST",
                            delay: 250,
                            cache: false,
                            data: function (params) {
                                return {
                                    Search: params.term || "",
                                    IDProdi: $('#prodiEdit').val(),
                                    lokasi: $('#lokasiEdit').val()
                                };
                            },
                            processResults: function (data, params) {
                                return {
                                    results: $.map(data, function (item) { return { id: item.MataKuliahID, value: item.MataKuliahID, text: item.NamaMataKuliah } })
                                };
                            },
                        }
                    });
                    $("#matakuliahEdit").change(function () {
                        dataParam = {};

                        dataParam.IDProdi = $('#prodiEdit').val();
                        dataParam.lokasi = $('#lokasiEdit').val();
                        dataParam.MataKuliahID = $('#matakuliahEdit').val();

                        $.ajax({
                            url: "/Admin/MasterMapingCapaianPembelajaran/GetMataKuliahByID",
                            dataType: 'json',
                            method: "POST",
                            delay: 250,
                            cache: false,
                            contentType: 'application/json',
                            data: JSON.stringify(dataParam),
                            success: function (e) {
                                //console.log(e[0]);
                                $("#matakuliahNamaEdit").val(e[0].NamaMataKuliah);
                                $("#matakuliahKodeEdit").val(e[0].KodeMataKuliah);
                                $("#matakuliahIdEdit").val(e[0].MataKuliahID);
                            },
                            error: function (e) {
                                console.log("matakuliah not found...");
                            }
                        })


                    });
                });





            });
        });
    });
}