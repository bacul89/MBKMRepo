$(document).ready(function () {
    var t = $('#TableList').DataTable({

        //"scrollY": 200,
        "scrollX": true,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0
        }],
        "order": [[1, 'asc']],
        "createdRow": function (row, data, index) {
            $('td', row).css({
                'border': '1px solid coral',
            });
        }
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    //$("#table-data-master-lookup_wrapper").css({ overflow: "auto" });
});

function validationCustom() {
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

function convertMilisecondToDate(value) {
    var num = parseInt(value.match(/\d+/), 10)
    // console.log(value);

   // var time = value; // get your number
    var date = new Date(num); // create Date object
    // var date = new Date(1324339200000);
    // console.log(date.toString());
   var result = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return result;

}

/*
function loadDatatable() {

}
*/

var dataTable = $('#table-data-master-lookup').DataTable({
    ajax: {
        url: '/Admin/MasterLookup/GetDataMasterLookup',
        dataSrc: ''
    },

    "columns": [
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        {
            "data": "Tipe",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "Nama",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "Nilai",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "CreatedDate",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + convertMilisecondToDate(data)   + '</div>';
            }
        },
        {
            "data": "CreatedBy",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "UpdatedDate",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + convertMilisecondToDate(data) + '</div>';
            }
        },
        {
            "data": "UpdatedBy",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "IsActive",
            "render": function (data, type, row, meta) {
                if (data) {
                    return '<div class="center">Aktif</div>';
                } else {
                    return '<div class="center">Tidak Aktif</div>';
                }

            }
        },
        {
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="IndexUpdateMasterLookup('${data}')"> <i class="fas fa-edit coral" ></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="IndexViewMasterLookup('${data}')"> <i class="fas fa-file-search coral"></i></a>
                                <a href="javascript:void(0)" style="color:black" onclick="DeletedMasterLookup('${data}')">  <i class="fas fa-trash-alt coral"></i></a>
                            </div>
                        </div>`;
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


/*setTimeout(function () {

    var table = $("#table-data-master-lookup")[0].outerHTML;
    console.log(table);
    $("<div id='table-data-master-lookup_overflow' style='overflow-x: scroll; width:100%'></div>").insertBefore("#table-data-master-lookup");
    $('#table-data-master-lookup').remove();

    setTimeout(function () {
        $("#table-data-master-lookup_overflow").append(table);
    }, 2000);

}, 3000);
*/
