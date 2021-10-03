$(document).ready(function () {
    loadFakultas();
    $("#prodi").select2({
        dropdownParent: $("#TambahCPL"),
        placeholder: "-- Pilih Program Studi --"
    });
    $("#lokasi").select2({
        dropdownParent: $("#TambahCPL"),
        placeholder: "-- Pilih Lokasi --"
    });
});
function loadFakultas() {
    $("#fakultas").select2({
        dropdownParent: $("#TambahCPL"),
        placeholder: "-- Pilih Fakultas --",
        width: "100%",
        ajax: {
            url: '/Admin/MasterCPL/GetFakultas',
            dataType: 'json',
            method: "POST",
            delay: 250,
            cache: false,
            data: function (params) {
                return {
                    Search: params.term || ""
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                };
            },
        }
    });
    $("#fakultas").change(function () {
        $("#prodi").select2({
            dropdownParent: $("#TambahCPL"),
            placeholder: "-- Pilih Program Studi --",
            width: "100%",
            ajax: {
                url: '/Admin/MasterCPL/GetProdiByFakultas',
                dataType: 'json',
                method: "POST",
                delay: 250,
                cache: false,
                data: function (params) {
                    return {
                        Search: params.term || "",
                        IDFakultas: $('#fakultas').val()
                    };
                },
                processResults: function (data, params) {
                    return {
                        results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                    };
                },
            }
        });
        $("#prodi").change(function () {
            $("#lokasi").select2({
                dropdownParent: $("#TambahCPL"),
                placeholder: "-- Pilih Lokasi --",
                width: "100%",
                ajax: {
                    url: '/Admin/MasterCPL/GetLokasiByProdi',
                    dataType: 'json',
                    method: "POST",
                    delay: 250,
                    cache: false,
                    data: function (params) {
                        return {
                            Search: params.term || "",
                            IDProdi: $('#prodi').val()
                        };
                    },
                    processResults: function (data, params) {
                        return {
                            results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Kampus } })
                        };
                    },
                }
            });
        });
    });
}
function AddCPLTemplate() {
    if ($('#created-cpl-template').length) {
        $('#TambahCPL').modal('show');
    } else {
        $.ajax({
            url: '/Admin/MasterCPL/ModaladdCPL',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                console.log(e);
               if ($('.data-content-modal').length) {
                   $('.data-content-modal').remove();
                }
                $('#modal-inner').append(e);
                $('.modal').modal('show');
            }
        })
    }
}
function UpdateMasterCPL(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterCpl/ModalUpdateMasterCpl/' + id,
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
function DetailMasterCPL(id) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/MasterCPL/ModalDetailMasterCpl/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
            /*console.log(e);*/
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('#detail-cpl-template').modal('show');
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