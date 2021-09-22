$(document).ready(function () {
    $('.js-example-basic-single').select2({
        placeholder: "Masukkan No. Kerjasama",
        "proccessing": true,
        "serverSide": true,
        ajax: {
            url: '/Admin/VerifikasiMahasiswa/GetAllNoKerjasama',
            type: 'post',
            dataType: 'json',
            data: function (params) {
                return {
                    search: params.term,
                    instansi: $('#namaUniversitas').val(),
                    length: params.length || 10,
                    skip: params.skip || 0
                };
            },
            processResults: function (data, page) {
                return {
                    results: data
                }
            }
        }
    });
    $('#inp_biaya').autoNumeric('init');
    var url = window.location.href;
    dId = url.substring(url.lastIndexOf('/') + 1);
    console.log(dId)
    $('#table-data-attachment').DataTable({
        ajax: {
            url: `/Admin/VerifikasiMahasiswa/GetAllAttachment/${dId}`  ,
            dataSrc: '',
            type : 'post',
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, full, meta) {
                    return '<div class="center">' + (meta.row + 1) + '</div>';
                }
            },
            {
                "data": "FileType",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                "data": "FileName",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="/Admin/VerifikasiMahasiswa/DownloadFile/${data}" style="color:coral"> <i class="fas fa-file-download"></i></a>
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
        },
        searching: false,
        paging: false,
        info : false
    });
});

function IndexEditVerifikasi() {
    $('#inp_noKerjaSama').prop('disabled', false);
    $('#inp_biaya').prop('disabled', false);
    $('#editVerifikasiButton').addClass("hidden");
}

