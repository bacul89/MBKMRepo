var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-jadwal-ujian-mahasiswa').DataTable();
    $("#inp_semester").select2({
        placeholder: "-- Pilih Fakultas --",
        width: "100%",
    });
    $('#inp_semester').change(function () {
    jsData.semester = $('#inp_semester :selected').val();
        $.ajax({
            url: '/Portal/JadwalUjian/CheckData/',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(jsData),
            contentType: 'application/json',
            success: function (e) {
                if (e.status == 500) {
                    $('#data_table').addClass('hidden');
                    Swal.fire({
                        title: 'Oppss',
                        icon: 'error',
                        html: e.message,
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                } else if (e.status == 200) {
                    $('#data_table').removeClass('hidden');
                    $('#announced').addClass('hidden');
                    table.destroy();
                    table = $('#table-data-jadwal-ujian-mahasiswa').DataTable({
                        "ajax": {
                            url: '/Portal/JadwalUjian/DaftarJadwalUjian/',
                            dataSrc: '',
                            data: {
                                semester: $('#inp_semester :selected').val(),
                            },
                            type: 'post',
                        },
                        "columns": [
                            {
                                "data": null,
                                "render": function (data, type, full, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + meta.settings._iDisplayStart + 1) + '</div>';
                                }
                            },
                            {
                                /*tahun ujian*/
                                "data": 1,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                                }
                            },
                            {
                                /*mata kuliah*/
                                "data": 2,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                                }
                            },
                            {
                                /*SKS*/
                                "data": 3,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + 3 + '</div>';
                                }
                            },
                            {
                                /*Tanggal ujian*/
                                "data": 3,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                                }
                            },
                            {
                                /*waktu*/
                                "data": 5,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + row[4] + ' - ' + row[5]  + '</div>';
                                }
                            },
                            {
                                /*lokasi*/
                                "data": 7,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                                }
                            },
                            {
                                /*Ruang Ujian*/
                                "data": 8,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                                }
                            },
                            {
                                /*Seksi*/
                                "data": 9,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                                }
                            },
                            {
                                /*prodi*/
                                "data": 10,
                                "render": function (data, type, row, meta) {
                                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
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
            }, error: function (e) {
                Swal.fire({
                    title: 'Oppss',
                    icon: 'error',
                    html: 'Mohon reload page',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
            }
        })
    })
})