
var table = $('#TableList').DataTable({
    "proccessing": true,
    "serverSide": true,
    "ajax": {
        url: '/Admin/PerjanjianKerjasama/GetListPKGrid',
        type: 'POST'
    },
    "language": {
        "emptyTable": "No record found.",
        "processing":
            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
        "search": "",
        "searchPlaceholder": "Search..."
    },
    "columns": [
        {
            "title": "Action",
            "data": "ID",
            "orderable": false,
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="UpdateKerjasama('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewKerjasama('${data}')"> <i class="fas fa-file-search coral"></i></a>
                            </div>
                        </div>`;
            }
        },
        {
            //"title":"No",
            "orderable": false,
            "data": null,
            "render": function (data, type, full, meta) {
                //return '<div class="center vertical-center" style="font-size: 0.8vw">' + (meta.row + 1) + '</div>';
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        {
            //"title": "No Kerjasama",
            "data": "NoPerjanjian",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            //"title": "Tanggal Mulai",
            "data": "TanggalMulai",
            "render": function (data, type, row, meta) {
                if (data === null) return "";
                return moment(data).format('DD/MM/YYYY');
            }
        },
        {
            //"title": "Tanggal Akhir",
            "data": "TanggalAkhir",
            "render": function (data, type, row, meta) {
                if (data === null) return "";
                return moment(data).format('DD/MM/YYYY');
            }
        },

        {
            //"title": "Jenis Pertukaran",
            "data": "JenisPertukaran",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            //"title": "Jenis Kerjasama",
            "data": "JenisKerjasama",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            //"title": "Instansi",
            "data": "Instansi",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            //"title": "Nama Unit",
            "data": "NamaUnit",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            //"title": "Nama Instansi",
            "data": "NamaInstansi",
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        },
        {
            //"title": "Inputer",
            "data": "CreatedBy",
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
        var d = new Date();
        var d2 = new Date();
        
        d.setDate(d.getDate() + 30);
        
        var tglakhir = moment(data.TanggalAkhir).format('YYYY-MM-DD');
        var tglakhir2 = moment(data.TanggalAkhir).format('YYYY-MM-DD');
        //var today = Date.now();
        tglakhir = new Date(tglakhir).toISOString().slice(0, 10);
        tglakhir2 = new Date(tglakhir2);
        var today = new Date().toISOString().slice(0, 10);
        //console.log(today) // 2021-01-16
        
        if (tglakhir == today || (tglakhir2 >= d2 && tglakhir2 <= d)) { //tglakhir >= d2 && tglakhir <= d ||
            $(row).css("background-color", "#ee2400");
        }
         else if (tglakhir2 < d2) {
            $(row).css("background-color", "grey");
        }
    },
});
var info = table.page.info();
function validationCustom2() {
    var isValid;
    $(".input-data").each(function () {
        var element = $(this);
        if (element.val() == "") {
            return isValid = false;
        } else {
            return isValid = true;
        }
    });
    return isValid;
}