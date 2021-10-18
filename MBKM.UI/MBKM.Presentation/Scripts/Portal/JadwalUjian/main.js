$(document).ready(function () {
    var table = $('#table-data-jadwal-ujian-mahasiswa').DataTable();
    $("#inp_semester").select2({
        placeholder: "-- Pilih Fakultas --",
        width: "100%",
    });
})