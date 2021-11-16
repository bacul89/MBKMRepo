var datatable = null;
$(document).ready(function () {
    datatable = $('#table-data-manage-ujian-mahasiswa').DataTable();
    $("#inp_semester").select2({});
    $('#inp_jenjang').change(function () {
        $("#inp_fakultas").prop('selectedIndex', 0);
        /*$("#inp_semester").prop('selectedIndex', 0);*/
    })

    $('#inp_jenjang').change(function () {
        $("#inp_fakultas").select2({
            placeholder: "-- Pilih Fakultas --",
            width: "100%",
            ajax: {
                url: "/Admin/ManageJadwalUjian/GetFakultas",
                dataType: 'json',
                method: "POST",
                delay: 250,
                cache: false,
                data: function (params) {
                    return {
                        Search: params.term || "",
                        JenjangStudi: $('#inp_jenjang :selected').val(),
                    };
                },
                processResults: function (data, params) {
                    return {
                        results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                    };
                },
            }
        });
        /*$("#inp_semester").select2({
            placeholder: "-- Pilih Semester --",
            width: "100%",
            ajax: {
                url: "/Admin/ManageJadwalUjian/GetSemester",
                dataType: 'json',
                method: "POST",
                delay: 250,
                cache: false,
                data: function (params) {
                    return {
                        Search: params.term || "",
                        JenjangStudi: $('#inp_jenjang :selected').val(),
                    };
                },
                processResults: function (data, params) {
                    return {
                        results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                    };
                },
            }
        });*/
    })

})

function convertMilisecondToDate(value) {
    var num = parseInt(value.match(/\d+/), 10)
    var date = new Date(num);
    var result = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return result;

}

function CustomValidation() {
    var isValid;
    $(".input-data :selected").each(function () {
        var element = $(this);
        if (element.val() == "") {
            return isValid = false;
        } else {
            return isValid = true;
        }
    });
    return isValid;
}

function GenerateDataTable() {
    if (!CustomValidation()) {
        $.LoadingOverlay("hide");
        Swal.fire({
            title: 'Oppss',
            icon: 'warning',
            html: 'Masukkan Status Verifikasi !',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    } else {
        datatable.destroy();
        datatable = $('#table-data-manage-ujian-mahasiswa').DataTable({
            "proccessing": true,
            "serverSide": true,
            "order": [[1, 'asc']],
            "ajax": {
                url: '/Admin/ManageJadwalUjian/GetDataTable',
                dataSrc: 'data',
                data: {
                    jenjangStudi: $('#inp_jenjang :selected').val(),
                    fakultas: $('#inp_fakultas :selected').val(),
                    jenisUjian: $('#inp_jenis :selected').val(),
                    tahunSemester: $('#inp_semester :selected').val(),
                },
                type: 'post',
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
                                <a href="javascript:void(0)" style="color:black" onclick="PopUpModal('${row.KodeMatkul}','${row.NamaMatkul}','${row.ClassSection}','${row.TanggalUjian}','${row.JamMulai}',)"> <i class="fas fa-file-search coral"></i></a>
                            </div>
                        </div>`;
                    }
                },
                {
                    //"title": "Jenis Ujian",
                    "data": "KodeTipeUjian",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": "Nomor Induk Pegawai",
                    "data": "KodeMatkul",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": "Nama",
                    "data": "NamaMatkul",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": "Nomor Induk Pegawai",
                    "data": "ClassSection",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": "Nomor Induk Pegawai",
                    "data": "TanggalUjian",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + convertMilisecondToDate(data) + '</div>';
                    }
                },
                {
                    //"title": "Nomor Induk Pegawai",
                    "data": "JamMulai",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + row.JamMulai + ' - ' + row.JamAkhir + '</div>';
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
    }
}

function indexDetailManageJadwalUjian(idManageUjian) {
    window.location.href = "/Admin/ManageJadwalUjian/DetailManageUjian/" + idManageUjian;
}

function PopUpModal(tempKodeMatkul, tempNamaMatkul, tempClassSection, tempTanggalUjian, tempJamMulai) {
    $.LoadingOverlay("show");
    $.ajax({
        url: '/Admin/ManageJadwalUjian/_getIndexModal',
        type: 'post',
        data: {
            KodeMatkul      : tempKodeMatkul,
            NamaMatkul      : tempNamaMatkul,
            ClassSection    : tempClassSection,
            TanggalUjian    : tempTanggalUjian,
            JamMulai        : tempJamMulai,
        },
        datatype: 'html',
        success: function (e) {
            $.LoadingOverlay("hide");
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