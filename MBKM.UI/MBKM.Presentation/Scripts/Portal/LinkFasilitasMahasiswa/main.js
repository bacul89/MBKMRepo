
var datatable = null;

$(document).ready(function () {

    $('#tahunAjaranCari').select2({

        placeholder: "-- Pilih Tahun Ajaran --",
        "proccessing": true,
        "serverSide": true,
        width: "100%",
        ajax: {
            url: "/LinkFasilitasMahasiswa/GetSemesterAll",
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
                /* console.log('page : ' + params.page);
                console.log(page);
                console.log('------------------------');
                console.log(page * 10);*/
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
        reloadDatatable();
        /*
        dataParam.NamaProdi = $('#prodiCari').val();
        dataParam.lokasi = $('#lokasiCari').val();
        dataParam.MataKuliahID = $('#matakuliahCari').val();
        *//*
    
        $("#matakuliahNamaCari").removeAttr('value');
        var matakuliah = $(this).find(":selected").text();
        $("#matakuliahNamaCari").val(matakuliah);
    
    
    
        $("#matakuliahKodeCari").val($(this).find(":selected").prop("title"));
        $("#matakuliahIdCari").val($(this).val());
    
        buttonHandler("open");*/

    });

    datatable = $('#table-data-jadwal-kuliah').DataTable();
    reloadDatatable();

});


function reloadDatatable() {
    var variable =
        /*'idProdi=' + $('#prodiIdCari').val() +
        '&lokasi=' + $('#kampusCari').val() +
        '&idFakultas=' + $('#fakultasCari').val() +
        '&jenjangStudi=' + $('#jenjangCari').val() +*/
        'strm=' + $('#tahunAjaranCari').val();

    datatable.destroy();
    datatable = $('#table-data-jadwal-kuliah').DataTable({
        paging: false,
        ordering: false,
        info: false,
        /*"columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0,
            //"visible": false, 'targets': [4, 6]
        }],*/
        //"order": [[1, 'asc']],
        //"proccessing": true,
        //"serverSide": true,
        //"order": [[1, 'asc']],
        //"aaSorting": [[0, "asc"]],
        "ajax": {
            url: '/Portal/LinkFasilitasMahasiswa/GetJadwalKuliah?' + variable,
            dataSrc: '',
            type: 'POST'
        },
        /*"language": {
            "emptyTable": "No record found.",
            "processing":
                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
            "search": "Search:",
            "searchPlaceholder": ""
        },*/
        "columns": [
            {
                "title": "Action",
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">

                                 <a href="javascript:void(0)" style="color:black" onclick="IndexViewLink('${data}')"> <i class="fas fa-file-search coral" ></i></a>
                                



                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                    //<a href="javascript:void(0)" style="color:black" onclick="DetailMasterCPL('${data}')"> <i class="fas fa-file-search coral"></i></a>
                }
            },
          
            {
                //"title": "No",
                "data": null,
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "STRM",
                "name": "STRM",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + $('#tahunAjaranCari').select2('data')[0].text + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "NamaProdi",
                "name": "NamaProdi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "NamaFakultas",
                "name": "NamaFakultas",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "KodeMataKuliah",
                "name": "KodeMataKuliah",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nama",
                "data": "NamaMataKuliah",
                "name": "NamaMataKuliah",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "SKS",
                "name": "SKS",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "Hari",
                "name": "Hari",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "JamMasuk",
                "name": "JamMasuk",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data +' - ' + row.JamSelesai+ '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "ClassSection",
                "name": "ClassSection",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "RuangKelas",
                "name": "RuangKelas",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "Lokasi",
                "name": "Lokasi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "NamaDosen",
                "name": "NamaDosen",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

        ],
        /*"createdRow": function (row, data, index) {
            $('td', row).css({
                'border': '1px solid coral',
                'border-collapse': 'collapse',
                'vertical-align': 'center',
            });

        }*/
    });

}


/* --responsive */
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
