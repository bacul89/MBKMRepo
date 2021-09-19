
$(document).ready(function () {
    $('.js-example-basic-single').select2({
        placeholder: "Select a state",
        "proccessing": true,
        "serverSide": true,
        ajax: {
            url: 'http://localhost:10776/Admin/VerifikasiMahasiswa/GetAllNoKerjasama',
            type: 'post',
            dataType: 'json',
            data: function (params) {
                return {
                    search: params.term,
                    instansi : "Universitas Diponegoro",
                    length: 5,
                    skip: 1
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
    $('#updateVerifikasiButton').removeClass("hidden");
}

