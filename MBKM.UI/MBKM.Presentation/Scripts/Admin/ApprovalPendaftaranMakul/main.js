var table = null;
var jsData = {};
$(document).ready(function () {
    table = $('#table-data-approval-pendaftaran-makul').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            url: '/Admin/ApprovalPendaftaranMatakuliah/GetAllApprovalPMK',
            data: {
                strm: $('#firstSemester').val()
            },
            type: 'POST'
        },
        "language": {
            "emptyTable": "No record found.",
            "processing": '<div style="padding-top:30px;"><i class="fa fa-spinner fa-spin fa-3x fa-fw" ></i><span class="sr-only" style="color:#2a2b2b;">Loading...</span></div> ',
            "search": "",
            "searchPlaceholder": "Search..."
        },
        "columns": [
            {
                "title": "Action",
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="center vertical-center" style="text-align:center; align-items:center">
                            <a href="javascript:void()">
                                <button type="button" onclick="urlLinkDetailCPMKP('${data}')" class="btn btn-warning btn-sm" style="font-size: 0.5vw"><i class="fas fa-search"></i></button>
                            </a>
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
                "title": "Universitas Asal",
                "data": "mahasiswas.NamaUniversitas",
                "render": function (data, type, row, meta) {
                    if (data == null) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                    }
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                "title": "Prodi Asal",
                "data": "mahasiswas.ProdiAsal",
                "render": function (data, type, row, meta) {
                    if (data == null) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                    }
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                "title": "NIM Asal",
                "data": "mahasiswas.NIMAsal",
                "render": function (data, type, row, meta) {
                    if (data == null) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                    }
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                "title": "Nama Mahasiswa",
                "data": "mahasiswas.Nama",
                "render": function (data, type, row, meta) {
                    if (data == null) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                    }
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                "title": "Jenjang Studi",
                "data": "mahasiswas.JenjangStudi",
                "render": function (data, type, row, meta) {
                    if (data == null) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                    }
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                }
            },
            {
                "title": "Kode Mata Kuliah Asal",
                "data": "MatkulKodeAsal",
                "render": function (data, type, row, meta) {
                    if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                        if (row.JadwalKuliahs.KodeMataKuliah == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.JadwalKuliahs.KodeMataKuliah + '</div>';
                    } else {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                }
            },
            {
                "title": "Nama Mata Kuliah Asal",
                "data": "MatkulAsal",
                "render": function (data, type, row, meta) {
                    if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                        if (row.JadwalKuliahs.NamaMataKuliah == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.JadwalKuliahs.NamaMataKuliah + '</div>';
                    } else {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                }
            },
            {
                "title": "Program Studi Tujuan",
                "data": "JadwalKuliahs.NamaProdi",
                "render": function (data, type, row, meta) {
                    if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                        return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                    } else {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                }
            },
            {
                "title": "Kode Mata Kuliah Dituju",
                "data": "JadwalKuliahs.KodeMataKuliah",
                "render": function (data, type, row, meta) {
                    if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                        if (row.MatkulKodeAsal == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.MatkulKodeAsal + '</div>';
                    } else {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                }
            },
            {
                "title": "Nama Mata Kuliah Dituju",
                "data": "JadwalKuliahs.NamaMataKuliah",
                "render": function (data, type, row, meta) {
                    if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                        if (row.MatkulAsal == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.MatkulAsal + '</div>';
                    } else {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                }
            },
            {
                "title": "No. Kerjasama",
                "data": "mahasiswas.NoKerjasama",
                "render": function (data, type, row, meta) {
                    if (data == null) {
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.noKerjasama +'</div>';
                    }
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
        },
        "order": [[0, "desc"]]
    });

    $('#inp_semester').change(function () {
        jsData.semester = $('#inp_semester :selected').val();
        table.destroy();
        table = $('#table-data-approval-pendaftaran-makul').DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                url: '/Admin/ApprovalPendaftaranMatakuliah/GetAllApprovalPMK',
                data: {
                    strm: $('#inp_semester :selected').val(),
                },
                type: 'POST'
            },
            "language": {
                "emptyTable": "No record found.",
                "processing": '<div style="padding-top:30px;"><i class="fa fa-spinner fa-spin fa-3x fa-fw" ></i><span class="sr-only" style="color:#2a2b2b;">Loading...</span></div> ',
                "search": "",
                "searchPlaceholder": "Search..."
            },
            "columns": [
                {
                    "title": "Action",
                    "data": "ID",
                    "render": function (data, type, row, meta) {
                        return `<div class="center vertical-center" style="text-align:center; align-items:center">
                            <a href="javascript:void()">
                                <button type="button" onclick="urlLinkDetailCPMKP('${data}')" class="btn btn-warning btn-sm" style="font-size: 0.5vw"><i class="fas fa-search"></i></button>
                            </a>
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
                    "title": "Universitas Asal",
                    "data": "mahasiswas.NamaUniversitas",
                    "render": function (data, type, row, meta) {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    "title": "Prodi Asal",
                    "data": "mahasiswas.ProdiAsal",
                    "render": function (data, type, row, meta) {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    "title": "NIM Asal",
                    "data": "mahasiswas.NIMAsal",
                    "render": function (data, type, row, meta) {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    "title": "Nama Mahasiswa",
                    "data": "mahasiswas.Nama",
                    "render": function (data, type, row, meta) {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    "title": "Jenjang Studi",
                    "data": "mahasiswas.JenjangStudi",
                    "render": function (data, type, row, meta) {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        }
                        return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                    }
                },
                {
                    "title": "Kode Mata Kuliah Asal",
                    "data": "MatkulKodeAsal",
                    "render": function (data, type, row, meta) {
                        if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                            if (row.JadwalKuliahs.KodeMataKuliah == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.JadwalKuliahs.KodeMataKuliah + '</div>';
                        } else {
                            if (data == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                        }
                    }
                },
                {
                    "title": "Nama Mata Kuliah Asal",
                    "data": "MatkulAsal",
                    "render": function (data, type, row, meta) {
                        if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                            if (row.JadwalKuliahs.NamaMataKuliah == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.JadwalKuliahs.NamaMataKuliah + '</div>';
                        } else {
                            if (data == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                        }
                    }
                },
                {
                    "title": "Program Studi Tujuan",
                    "data": "JadwalKuliahs.NamaProdi",
                    "render": function (data, type, row, meta) {
                        if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                            return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                        } else {
                            if (data == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                        }
                    }
                },
                {
                    "title": "Kode Mata Kuliah Dituju",
                    "data": "JadwalKuliahs.KodeMataKuliah",
                    "render": function (data, type, row, meta) {
                        if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                            if (row.MatkulKodeAsal == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.MatkulKodeAsal + '</div>';
                        } else {
                            if (data == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                        }
                    }
                },
                {
                    "title": "Nama Mata Kuliah Dituju",
                    "data": "JadwalKuliahs.NamaMataKuliah",
                    "render": function (data, type, row, meta) {
                        if (row.JenisKerjasama == "Internal ke Luar Atma Jaya") {
                            if (row.MatkulAsal == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.MatkulAsal + '</div>';
                        } else {
                            if (data == null) {
                                return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>';
                            }
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
                        }
                    }
                },
                {
                    "title": "No. Kerjasama",
                    "data": "mahasiswas.NoKerjasama",
                    "render": function (data, type, row, meta) {
                        if (data == null) {
                            return '<div class="center vertical-center" style="font-size: 0.8vw">' + row.noKerjasama + '</div>';
                        }
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
            },
            "order": [[0, "desc"]]
        });
    })
})

function OpenModal() {
    $('#DetailTrackingStatus').modal('show');
}

function urlLinkDetailCPMKP(id) {
    window.location.href = "/Admin/ApprovalPendaftaranMatakuliah/DetailCPL/" + id;
}