var table = null;
var jsData = {};
$(document).ready(function () {

    table = $('#table-data-summary-feedback-admin').DataTable({});
    table = $('#table-data-summary-feedback-dosen').DataTable({});
    $('.select2').select2({});

    $('#inp_jenjang').change(function () {
        $("#inp_fakultas").prop('selectedIndex', 0);
        $("#inp_semester").prop('selectedIndex', 0);
    })

    $('#inp_jenjang').change(function () {
        $("#inp_fakultas").select2({
            placeholder: "-- Pilih Fakultas --",
            width: "100%",
            ajax: {
                url: "/Admin/SummaryFeedback/GetFakultas",
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
});

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
        table = $('#table-data-summary-feedback-admin').DataTable({
            "ajax": {
                url: '/Admin/SummaryFeedback/GetDataTableAdmin',
                dataSrc: '',
                data: {
                    jenjangStudi: $('#inp_jenjang :selected').val(),
                    fakultas: $('#inp_fakultas :selected').val(),
                    tahunSemester: $('#inp_semester :selected').val(),
                },
                type: 'post',
            },
            "columns": [
                {
                    "data": 0,
                    "render": function (data, type, row, meta) {
                        return `<div class="row justify-content-center">
                                <div class="col" style="text-align:center">
                                    <a href="javascript:void(0)" style="color:black" onclick="redirectData('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                 </div>
                            </div>`;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
                    }
                },
                {
                    /*Semester*/
                    "data": 1,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*NIP*/
                    "data": 2,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Nama Dosen*/
                    "data": 3,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Kode MK*/
                    "data": 4,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Nama Matkul*/
                    "data": 5,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Seksi*/
                    "data": 6,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    /*Jumlah mahasiswa*/
                    "data": 7,
                    "render": function (data, type, row, meta) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                }

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
