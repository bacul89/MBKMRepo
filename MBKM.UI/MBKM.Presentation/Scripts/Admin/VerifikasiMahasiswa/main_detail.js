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
});

function IndexEditVerifikasi() {
    $('#inp_noKerjaSama').prop('disabled', false);
    $('#inp_biaya').prop('disabled', false);
    $('#editVerifikasiButton').addClass("hidden");
}

