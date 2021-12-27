$(document).ready(function () {
    $('#deletedMenu').removeClass("bg-silver-active");
    $('collapse').removeClass("in");

    if (window.location.href.indexOf("/DataDiri") > -1) {
        $('#submenu1').addClass("in");
        $('#DataDiriMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/PendaftaranMataKuliah") > -1) {
        $('#submenu1').addClass("in");
        $('#PendaftaranMataKuliahMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/TrackingStatusPendaftaran") > -1) {
        $('#submenu1').addClass("in");
        $('#TrackingStatusPendaftaranMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/NimDigital") > -1) {
        $('#submenu1').addClass("in");
        $('#NimDigitalMenu').addClass("bg-silver-active");
    }


    if (window.location.href.indexOf("/JadwalKuliahMahasiswa") > -1) {
        $('#submenu2').addClass("in");
        $('#JadwalKuliahMahasiswaMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/JadwalUjian") > -1) {
        $('#submenu2').addClass("in");
        $('#JadwalUjianMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/PresensiKelas") > -1) {
        $('#submenu2').addClass("in");
        $('#PresensiKelasMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/PresensiUjian") > -1) {
        $('#submenu2').addClass("in");
        $('#PresensiUjianMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/LinkFasilitasMahasiswa") > -1) {
        $('#submenu2').addClass("in");
        $('#LinkFasilitasMahasiswaMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/SummaryPresensi") > -1) {
        $('#submenu2').addClass("in");
        $('#SummaryPresensiMenu').addClass("bg-silver-active");
    }


    if (window.location.href.indexOf("/FeedBackMatakuliah") > -1) {
        $('#submenu3').addClass("in");
        $('#FeedBackMatakuliahMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/KHS") > -1) {
        $('#submenu3').addClass("in");
        $('#KHSMenu').addClass("bg-silver-active");
    } else if (window.location.href.indexOf("/TranskripMahasiswa") > -1) {
        $('#submenu3').addClass("in");
        $('#TranskripMahasiswaMenu').addClass("bg-silver-active");
    }
})