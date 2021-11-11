
function convertDay(date) {
    var result = "";
    const weekday = new Array(7);
    weekday[0] = "Minggu";
    weekday[1] = "Senin";
    weekday[2] = "Selasa";
    weekday[3] = "Rabu";
    weekday[4] = "Kamis";
    weekday[5] = "Juma't";
    weekday[6] = "Sabtu";


    if (date == 'Sunday') {
        result = weekday[0];
    } else if (date == 'Monday') {
        result = weekday[1];
    } else if (date == 'Tuesday') {
        result = weekday[2];
    } else if (date == 'Wednesday') {
        result = weekday[3];
    } else if (date == 'Thursday') {
        result = weekday[4];
    } else if (date == 'Friday') {
        result = weekday[5];
    } else if (date == 'Saturday') {
        result = weekday[6];
    }

    return result;
}


function convertDate(getDate) {
    //console.log(getBirtDay);


/*    const month = new Array();
    month[0] = "Januari";
    month[1] = "Februari";
    month[2] = "Maret";
    month[3] = "April";
    month[4] = "Mei";
    month[5] = "Juni";
    month[6] = "Juli";
    month[7] = "Augustus";
    month[8] = "September";
    month[9] = "Oktober";
    month[10] = "November";
    month[11] = "Desember";
*/



    //let day = weekday[d.getDay()];

    var date = getDate.split(" ")[0];
    //var createdate = new Date(date);
    //var newdate = new Date();
    //console.log(getDate);
   // var parts = date.split('');
    //var createdate = new Date(parts[0], parts[1] - 1, parts[2]);

    //var result = mydate.toShortFormat();

    //var result = createdate.getDate() + '/' + createdate.getMonth()+ '/' + createdate.getFullYear();
    //$('#tgl').text(result);
    return date;
}


var datatable = null;

$(document).ready(function () {

    $('#tahunAjaranCari').select2({

        placeholder: "-- Pilih Tahun Ajaran --",
        "proccessing": true,
        "serverSide": true,
        width: "100%",
        ajax: {
            url: "/Admin/ReportMBKMEkternalPertukaran/GetSemesterAll",
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
        createDownload();




    });

    datatable = $('#table-data-external').DataTable();
    reloadDatatable();
    createDownload();
});


function createDownload() {
    var base_url = window.location.origin;
    var strm = $('#tahunAjaranCari').val();
    var urlPDF = base_url + "/Admin/ReportMBKMEkternalPertukaran/ExportPdf?strm=" + strm;
    var urlXLS = base_url + "/Admin/ReportMBKMEkternalPertukaran/ExportExcel?strm=" + strm;

    $("#pdf").attr("href", urlPDF);
    $("#xls").attr("href", urlXLS);
}

function reloadDatatable() {

    console.log($('#tahunAjaranCari').select2('data')[0].text);
    var variable =
        /*'idProdi=' + $('#prodiIdCari').val() +
        '&lokasi=' + $('#kampusCari').val() +
        '&idFakultas=' + $('#fakultasCari').val() +
        '&jenjangStudi=' + $('#jenjangCari').val() +*/
        'strm=' + $('#tahunAjaranCari').val();

    datatable.destroy();
    datatable = $('#table-data-external').DataTable({
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
            url: '/Admin/ReportMBKMEkternalPertukaran/DataTable?' + variable,
            dataSrc: '',
            //data: variable,
            type: 'post',
        },
        /*"language": {
            "emptyTable": "No record found.",
            "processing":
                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
            "search": "Search:",
            "searchPlaceholder": ""
        },*/
        "columns": [
            /*{
                "title": "Action",
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">

                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateJadwalKuliah('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewJadwalKuliah('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedJadwalKuliah('${data}')">  <i class="fas fa-trash-alt coral"></i></a>



                            </div>
                        </div>`;//<a href="javascript:void(0)" style="color:black" onclick="DeleteUserGetID('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                    //<a href="javascript:void(0)" style="color:black" onclick="DetailMasterCPL('${data}')"> <i class="fas fa-file-search coral"></i></a>
                }
            },*/
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
                "data": "1",
                "name": "1",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "2",
                "name": "2",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "3",
                "name": "3",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "4",
                "name": "4",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "5",
                "name": "5",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nomor Induk Pegawai",
                "data": "6",
                "name": "6",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Nama",
                "data": "7",
                "name": "7",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },

            {
                //"title": "Email",
                "data": "8",
                "name": "8",
                "render": function (data, type, row, meta) {
                    return '<div id="sks" class="center">' + data + '</div>';
                }
            },


            {
                //"title": "Email",
                "data": "9",
                "name": "9",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "10",
                "name": "10",
                "render": function (data, type, row, meta) {
                    //console.log(row.JamMasuk);
                    //console.log(data);
                    //console.log(type);

                    return '<div class="center">' + convertDay(data) + '</div>';
                }

            },

            {
                //"title": "Nomor Induk Pegawai",
                "data": "11",
                "name": "11",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "12",
                "name": "12",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "13",
                "name": "13",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + convertDate(data) + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "14",
                "name": "14",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + convertDate(data) + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "15",
                "name": "15",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + row[17] +' - '+ row[15] + '</div>';
                }
            },
            {
                //"title": "Email",
                "data": "16",
                "name": "16",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
        ],

        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };


            // Total over all pages
            total = api
                .column(6)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);


            // Update footer
            $(api.column(6).footer()).html(
                total + '.00'
            );


            //console.log(total);
            //sks();
            //var sksTotal = 0;
            //var sksBody = "<tr><td colspan='6'><b>Total SKS</b></td><td colspan='7'>" + sksTotal + ".00</td></tr>";
            //console.log(sksBody);

            //$('#table-data-jadwal-kuliah').append(sksBody);
        },
        /*var api = this.api(), data;

        // Remove the formatting to get integer data for summation
        var intVal = function (i) {
            return typeof i === 'string' ?
                i.replace(/[\$,]/g, '') * 1 :
                typeof i === 'number' ?
                    i : 0;
        };*/

        /*"createdRow": function (row, data, index) {
            $('td', row).css({
                'border': '1px solid coral',
                'border-collapse': 'collapse',
                'vertical-align': 'center',
            });

        }*/
    });

    //setTimeout(, 5000)
    /*    setTimeout(function () {
            sks();
        }, 800);*/
    //sks(true);
}


/*function sks() {

    //if (access == true) {
        var sksTotal = 0;

        var n = $('div[id^="sks"]').length;
        //console.log(n);

        $('div[id^="sks"]').each(function () {
            //var num = $(this).attr("id");

            var value = parseInt($(this).text());
            //console.log("herro : " + value);
            //list.push(value);
            sksTotal = sksTotal + value;
            //console.log(num);

        });

        var sksBody = "<tr><td colspan='6'><b>Total SKS</b></td><td colspan='7'>" + sksTotal + ".00</td></tr>";
        //console.log(sksBody);

        $('#table-data-jadwal-kuliah').append(sksBody);
    //}

}*/


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
