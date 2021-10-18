var table = null;
$(document).ready(function () {
    table = $('#table-data-jadwal-ujian-mahasiswa').DataTable();

    $('#inp_jenjang').change(function () {
        $("#inp_fakultas").prop('selectedIndex', 0);
        $("#inp_semester").prop('selectedIndex', 0);
    })

    $('#inp_jenjang').change(function () {
        $("#inp_fakultas").select2({
            placeholder: "-- Pilih Fakultas --",
            width: "100%",
            ajax: {
                url: "/Admin/JadwalUjian/GetFakultas",
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
        $("#inp_semester").select2({
            placeholder: "-- Pilih Semester --",
            width: "100%",
            ajax: {
                url: "/Admin/JadwalUjian/GetSemester",
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
            html: 'Masukkan Fiter Pencarian',
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: 'OK'
        })
    } else {
        table.destroy();
        table = $('#table-data-jadwal-ujian-mahasiswa').DataTable({
            "proccessing": true,
            "serverSide": true,
            "order": [[1, 'asc']],
            "ajax": {
                url: '/Admin/JadwalUjian/GetDataTable',
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
                                <a href="javascript:void(0)" style="color:black" onclick="indexDetailJadwalUjian('${data}')"> <i class="fas fa-file-search coral"></i></a>
                            </div>
                        </div>`;
                    }
                },
                {
                    //"title": "kode matakuliah",
                    "data": "KodeMatkul",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": "nama matakuliah",
                    "data": "NamaMatkul",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": "seksi",
                    "data": "ClassSection",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": "Tanggal",
                    "data": "TanggalUjian",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + convertMilisecondToDate(data) + '</div>';
                    }
                },
                {
                    //"title": Jam",
                    "data": "JamMulai",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + row.JamMulai + ' - ' + row.JamAkhir + '</div>';
                    }
                },
                {
                    //"title": Ruang Ujian",
                    "data": "RuangUjian",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    //"title": Lokasi",
                    "data": "Lokasi",
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
            }
        });
    }
}

function indexDetailJadwalUjian(id) {
    window.location.href = "/Admin/JadwalUjian/DetailJadwalUjian/" + id;
}