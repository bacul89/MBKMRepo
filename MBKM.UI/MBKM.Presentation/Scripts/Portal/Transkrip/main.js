//---lookup
function getLookupBAA(tipe) {

    $.ajax({
        url: "/MasterMapingCapaianPembelajaran/getLookupByTipe",
        type: 'get',
        datatype: 'html',
        data: { Tipe: tipe },
        success: function (e) {

            if (tipe == 'KepalaBiroAdministrasiAkademik') {
                $("#baaNamePrint").text(e[0].Nilai);
                $("#baaName").text(e[0].Nilai);
            } else {
                
                return e;
            }
        }
    })

}

// date converter
Date.prototype.toShortFormat = function () {

    let monthNames = ["Januari", "Februari", "Maret", "April",
        "Mei", "Juni", "Juli", "Augustus",
        "September", "Oktober", "November", "Desember"];

    let day = this.getDate();

    let monthIndex = this.getMonth();
    let monthName = monthNames[monthIndex];

    let year = this.getFullYear();

    return `${day} ${monthName} ${year}`;
}

/*var getBirtDay = $('#birthday').text();
var date = getBirtDay.split(" ")[0];
var parts = date.split('/');
var mydate = new Date(parts[2], parts[1] - 1, parts[0]);

var result = mydate.toShortFormat();*/


function convertBirthday(value) {
/*    var getBirtDay = $('#birthday').text();
    var date = getBirtDay.split(" ")[0];
    var parts = date.split('/');
    var mydate = new Date(parts[2], parts[1] - 1, parts[0]);*/
    //console.log(value);
    var date = value.split("T")[0];
    var parts = date.split('-');
    var mydate = new Date(parts[0], parts[1] - 1, parts[2]);
    var result = mydate.toShortFormat();

    //var result = mydate.toShortFormat();

    $('#birthday').text(result);
    $('#birthdayView').text(result);
}



$(document).ready(function () {
    $("#print").hide();
    var date = new Date();
    $("#currentDate").text(date.toShortFormat());
    $("#currentDatePrint").text(date.toShortFormat());
    //convertBirthday();
    getNilai();
    isZooming();
    getLookupBAA('KepalaBiroAdministrasiAkademik');
    
});

//---responsive
$(window).resize(function () {
    isZooming();
});

function isZooming() {
    var defaultH = 800;
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

//---Grade
var gradeFinal = "";
var sksTotal = 0;
var Nilais;
var mhsName;
var mhsNIM;
var mhsStrm;
var mhsStrmId;

//NilaiGrades
function getNilai() {
    //var base_url = window.location.origin;
    $.ajax({
        url: '/Portal/TranskripMahasiswa/getTranskrip/',
        type: 'POST',
        datatype: 'json',
        success: function (resultTranskip) {

            

            convertBirthday(resultTranskip.mahasiswaBirthday);
            mhsName = resultTranskip.mahasiswaName;
            mhsNIM = resultTranskip.mahasiswaNIM;
            mhsStrm = resultTranskip.semester;
            mhsStrmId = resultTranskip.strm;

            console.log(mhsStrmId);

            if (resultTranskip.pertukaran == true) {
                CheckStatusFeedback(true, 'internal', mhsStrmId);
            } else {
                Nilais = resultTranskip.transkrip;
                showValue(resultTranskip.transkrip);
            }



            
                /*$.ajax({
                    url: "/Portal/TranskripMahasiswa/getLookupByTipe",
                    type: 'get',
                    datatype: 'html',
                    data: { Tipe: 'NilaiGrade' },
                    success: function (resultLookup) {
                        NilaiGrades = resultLookup;
                       

                    }
                })*/

        }
    })
}

function intToFloat(num, decPlaces) { return num.toFixed(decPlaces); }

function showValue(result) {
    //let NilaiGrades = loadFromLookup('NilaiGrade');

    //console.log(NilaiGrades.A);

    $("#data").empty();
    var row = "";
    var html = "";

    /*var gA = "A";
    var gAmin = "A-";
    var gBplus = "B+";
    var gB = "B";
    var gBmin = "B-";
    var gCplus = "C+";
    var gC = "C";
    var gD = "D";
    var gE = "E";*/


    /*var bA = '4.00';
    var bAmin = '3.70';
    var bBplus = '3.30';
    var bB = '3.00';
    var bBmin = '2.70';
    var bCplus = '2.30';
    var bC = '2.00';
    var bD = '1.00';
    var bE = '0';*/


    /*var gA = NilaiGrades[0].Nama;
    var gAmin = NilaiGrades[1].Nama;
    var gBplus = NilaiGrades[2].Nama;
    var gB = NilaiGrades[3].Nama;
    var gBmin = NilaiGrades[4].Nama;
    var gCplus = NilaiGrades[5].Nama;
    var gC = NilaiGrades[6].Nama;
    var gD = NilaiGrades[7].Nama;
    var gE = NilaiGrades[8].Nama;


    var bA = NilaiGrades[0].Nilai;
    var bAmin = NilaiGrades[1].Nilai;
    var bBplus = NilaiGrades[2].Nilai;
    var bB = NilaiGrades[3].Nilai;
    var bBmin = NilaiGrades[4].Nilai;
    var bCplus = NilaiGrades[5].Nilai;
    var bC = NilaiGrades[6].Nilai;
    var bD = NilaiGrades[7].Nilai;
    var bE = NilaiGrades[8].Nilai;



    var A = parseFloat(bA);
    var Amin = parseFloat(bAmin);
    var Bplus = parseFloat(bBplus);
    var B = parseFloat(bB);
    var Bmin = parseFloat(bBmin);
    var Cplus = parseFloat(bCplus);
    var C = parseFloat(bC);
    var D = parseFloat(bD);
    var E = parseFloat(bE);*/

    var nilaiTotal = 0;
    var rowNilaiSks = 0;
    var sksTotal = 0;

    if (result.length > 0) {
        for (var i = 0; i < result.length; i++) {
            if (i == 0) {
                //console.log(result[i].FlagCetak);
                //$("#btnCetak").prop("disabled", false);
                CheckStatusFeedback(result[i].FlagCetak, result.length, mhsStrmId);
            }

            var kodematakuliah = "<td>" + result[i].KodeMataKuliah + "</td>";

            var mkEn = "<i style='text-align:left;' class='en'>" + result[i].NamaMataKuliahEN + "</i>";
            var matakuliah = "<td>" + result[i].NamaMataKuliah + mkEn + "</td>";

            var sksInt = parseInt(result[i].SKS);
            var sks = "<td style='text-align:right;'>" + sksInt + "</td>";

            var grade = result[i].Grade;
            var gradeHtml = "<td style='float:right;'><div style='width: 14px;text-align: left; position:relative;'>" + grade + "</div></td>";

            var bobot = parseFloat(result[i].Nilai);

            //console.log(result[i].Nilai);
            //console.log(bobot);

            //bobotTotal = bobotTotal + bobot;
            //var nilai = result[i].Nilai;



            sksTotal = sksTotal + sksInt;

            row = "<tr>" + kodematakuliah + matakuliah + sks + gradeHtml + "</tr>";
            rowNilaiSks = sksInt * bobot;

            //console.log("sks : " + sksInt);
            //console.log("grade : " + grade);
            /*if (grade == gA) {
                rowNilaiSks = sksInt * A;
            } else if (grade == gAmin) {
                rowNilaiSks = sksInt * Amin;
            } else if (grade == gBplus) {
                rowNilaiSks = sksInt * Bplus;
            } else if (grade == gB) {
                rowNilaiSks = sksInt * B;
            } else if (grade == gBmin) {
                rowNilaiSks = sksInt * Bmin;
            } else if (grade == gCplus) {
                rowNilaiSks = sksInt * Cplus;
            } else if (grade == gC) {
                rowNilaiSks = sksInt * C;
            } else if (grade == gD) {
                rowNilaiSks = sksInt * D;
            } else if (grade == gE) {
                rowNilaiSks = sksInt * E;
            }*/
            //console.log("sks k:"+rowNilaiSks);
            nilaiTotal = nilaiTotal + rowNilaiSks;
            //console.log(rowNilaiSks);
            //console.log(nilaiTotal);

            html = html + row;
        }
        //console.log(html);

        gradeTotal = nilaiTotal / sksTotal;

        if (isNaN(parseFloat(gradeTotal))) {
            gradeTotal = 0;
        }


        /*    var A = 4.00;
            var Amin = 3.70;
            var Bplus = 3.30;
            var B = 3.00;
            var Bmin = 2.70;
            var Cplus = 2.30;
            var C = 2.00;
            var D = 1.00;
            var E = 0;
        
            if (gradeTotal == A) {
                gradeFinal = gA;
            } else if (gradeTotal >= Amin && gradeTotal <= A) {
                gradeFinal = gAmin;
            } else if (gradeTotal >= Bplus && gradeTotal <= Amin) {
                gradeFinal = gBplus;
            } else if (gradeTotal >= B && gradeTotal <= Bplus) {
                gradeFinal = gB;
            } else if (gradeTotal >= Bmin && gradeTotal <= B) {
                gradeFinal = gBmin;
            } else if (gradeTotal >= Cplus && gradeTotal <= Bmin) {
                gradeFinal = gCplus;
            } else if (gradeTotal >= C && gradeTotal <= Cplus) {
                gradeFinal = gC;
            } else if (gradeTotal >= D && gradeTotal <= C) {
                gradeFinal = gD;
            } else if (gradeTotal >= E && gradeTotal <= D) {
                gradeFinal = gE;
            }*/

        //console.log(gradeTotal);
        //console.log(gradeTotal.toFixed(2));

        $("#data").html(html);
        $("#totalSks").html(sksTotal);
        $("#totalGrade").html(gradeTotal.toFixed(2));


        //intToFloat(gradeTotal, 2)
        $("#dataPrint").html(html);
        $("#totalSksPrint").html(sksTotal);
        $("#totalGradePrint").html(gradeTotal.toFixed(2));
    } else {
        CheckStatusFeedback(false, 'eksternal', mhsStrmId);
    }

}


//---print
function print(id, nim) {
    var base_url = window.location.origin;
    var idMahasiswa = parseInt(id);

    swal.fire({
        title: "Transkrip Nilai Hanya Dapat Tercetak Satu Kali \n Hubungi Pihak BAA Jika Terjadi Gagal Cetak",
        type: "warning",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#06a956",
        confirmButtonText: "Yes",
        closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {

            $.LoadingOverlay("show");
            $.ajax({
                url: '/Portal/TranskripMahasiswa/UpdateStatus',
                type: 'post',
                data: {
                    idMahasiswa : idMahasiswa,
                    strmStr : mhsStrmId
                },
                datatype: 'json',
                success: function (e) {
                    $.LoadingOverlay("hide");


                    if (e.status == 500) {
                        Swal.fire({
                            title: 'Oppss',
                            icon: 'error',
                            html: e.message,
                            showCloseButton: true,
                            showCancelButton: false,
                            focusConfirm: false,
                            confirmButtonText: 'OK'
                        })
                    } else {
                        //$("#view").hide();
                        //$("#print").show();
                        //print();
                        Swal.fire({
                            title: 'Berhasil',
                            icon: 'success',
                            html: 'Cetak Berhasil',
                            showCloseButton: true,
                            showCancelButton: false,
                            focusConfirm: false,
                            confirmButtonText: 'OK'
                        })
                        var data =  $("#print").html();
                        var mywindow = window.open('', '_blank');
                        mywindow.document.write('<html><head><title>' + mhsStrm + ' - Transkrip_' + mhsNIM + '_' + mhsName+'</title>');
                        /*optional stylesheet*/ mywindow.document.write('<link rel="stylesheet" href="' + base_url+'/Content/Portal/Transkrip/print.css" type="text/css" />');
                        mywindow.document.write('</head><body >');
                        mywindow.document.write(data);
                        mywindow.document.write('</body></html>');

                        setTimeout(function () {
                            mywindow.print();
                            mywindow.close();
                        }, 500);
                        $("#btnCetak").prop("disabled", true);
                        //$("#view").show();
                        //$("#print").hide();
                    }
                }, error: function (e) {
                    $.LoadingOverlay("hide");
                    Swal.fire({
                        title: 'Oppss',
                        icon: 'error',
                        html: 'Coba Reload Page',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                }
            })
        }
    })
}


function checkStatusSertifikat() {
    $.ajax({
        url: '/Portal/TranskripMahasiswa/CheckStatusSertifikat',
        type: 'POST',
        datatype: 'JSON',
        success: function (e) {


            if (e.data == false) {
                $("#sertifikatCetak").prop("disabled", false);
            } else {
                $("#sertifikatCetak").prop("disabled", true);
                var alert = "Cetak Sertifikat telah dilakukan, silahkan hubungi BAA untuk mencetak Sertifikat!";
                $('#notif-sertifikat').text(alert);
                $('#notif-sertifikat').show();
            }
        }, error: function (e) {
            $("#sertifikatCetak").prop("disabled", true);
        }
    })

}

function printSertifikat() {
    swal.fire({
        title: "Sertifikat Nilai Hanya Dapat Tercetak Satu Kali \n Hubungi Pihak BAA Jika Terjadi Gagal Cetak",
        type: "warning",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#06a956",
        confirmButtonText: "Ok",
        //closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {
            $.LoadingOverlay("show");
            $.ajax({
                url: '/Portal/SertifikatMbkm/GetFile',
                type: 'POST',
                datatype: 'JSON',
                success: function (e) {

                    //console.log("try " + e.data);
                    var base_url = window.location.origin;
                    if (e.data != true) {

                        var mywindow = window.open('', '_blank');
                        mywindow.location = base_url + '/Portal/SertifikatMbkm/GetFile';
                        setTimeout(function () {
                            $.ajax({
                                url: '/Portal/TranskripMahasiswa/UpdateStatusSertifikat',
                                type: 'POST',
                                datatype: 'JSON',
                                success: function (e) {
                                    //console.log(e);
                                    //console.log(e.data);
                                    if (e.status == 500) {
                                        $("#sertifikatCetak").prop("disabled", true);
                                    } else if (e.status == 200) {
                                        $("#sertifikatCetak").prop("disabled", true);
                                        Swal.fire({
                                            title: 'success',
                                            icon: 'success',
                                            html: 'Cetak Berhasil',
                                            showCloseButton: true,
                                            showCancelButton: false,
                                            focusConfirm: false,
                                            confirmButtonText: 'OK'
                                        })
                                        //$("#sertifikatCetak").prop("disabled", false);
                                    }
                                    $.LoadingOverlay("hide");
                                }, error: function (e) {
                                    $("#sertifikatCetak").prop("disabled", true);
                                    $.LoadingOverlay("hide");
                                }
                            })

                        }, 500);


                    } else {
                        //var location = 
                        //window.location = base_url + '/Portal/TranskripMahasiswa';
                        $.LoadingOverlay("hide");
                        $("#sertifikatCetak").prop("disabled", true);
                        swal.fire({
                            title: "Cetak Sertifikat Gagal, Silahkan Hubungi Pihak BAA!!!",
                            type: "warning",
                            icon: "warning",
                            showCancelButton: true,
                            confirmButtonColor: "#06a956",
                            confirmButtonText: "Ok",
                            //closeOnConfirm: false
                        }).then((result) => {
                            if (result.isConfirmed) {

                            }
                        })
                    }
                }, error: function (e) {
                    $("#sertifikatCetak").prop("disabled", true);
                }
            })
        }
    })



}


/*function printSertifikat() {
    var base_url = window.location.origin;
    


    $.ajax({
        url: '/Portal/TranskripMahasiswa/UpdateStatusSertifikat',
        type: 'POST',
        datatype: 'JSON',
        success: function (e) {

            if (e.status == 500) {
                $("#sertifikatCetak").prop("disabled", true);
            } else {
                $("#sertifikatCetak").prop("disabled", false);
            }
        }, error: function (e) {
            $("#sertifikatCetak").prop("disabled", true);
        }
    })
}*/

var feedbackStatus;
var alert;
function CheckStatusFeedback(FlagTranscript, row, mhsStrmId) {
    console.log(mhsStrmId);
    $.ajax({
        url: '/Portal/TranskripMahasiswa/CheckStatusFeedback',
        type: 'POST',
        datatype: 'json',
        data: {
           strmStr: mhsStrmId 
        },
        success: function (result) {
            //console.log(result);
            //var status = result[0][5];
            //var paymentStatus = result[0][6];

            feedbackStatus = result;
            /*console.log(status);
            console.log(paymentStatus);
            console.log(FlagTranscript);*/
            //console.log(row);

            /*if (checker == true && (row == 'internal' || row == result.length)) {
                
                //$("#sertifikatCetak").prop("disabled", false);
                if (FlagTranscript == false) {
                    
                }  if (FlagTranscript == true) {
                    
                }
            } else {
                

            }*/
            
            if (row == 'internal') {
                var html = "";
                for (var i = 0; i < result.length; i++) {
                    var mkEn = "<i style='text-align:left;' class='en'>" + result[i][7] + "</i>";
                    var matakuliah = result[i][1] + mkEn;
                    html = html + "<tr><td>" + result[i][2] + "</td><td>" + matakuliah + "</td><td style='text-align:right'>-</td><td style='text-align:right'>-</td></tr>";
                }



                $("#data").html(html);
                $("#totalSks").html("-");
                $("#totalGrade").html("-");
                $("#btnCetak").hide();
                $("#PageTitle").text("SERTIFIKAT MBKM");

            } else if (row == 'eksternal') {
                var html = "";
                for (var i = 0; i < result.length; i++) {
                    var mkEn = "<i style='text-align:left;' class='en'>" + result[i][7] + "</i>";
                    var matakuliah = result[i][1] + mkEn;
                    html = html + "<tr><td>" + result[i][2] + "</td><td>" + matakuliah + "</td><td style='text-align:right'>-</td><td style='text-align:right'>-</td></tr>";
                }



                $("#data").html(html);
                $("#totalSks").html("-");
                $("#totalGrade").html("-");
                //$("#btnCetak").hide();
                $("#PageTitle").text("TRANSKRIP NILAI & SERTIFIKAT MBKM");

            } else {
                $("#PageTitle").text("TRANSKRIP NILAI & SERTIFIKAT MBKM");

            }


            var checker = parseFeedback(result);
            //console.log(checker);
            //console.log(FLAGBAYAR);

                if (row == 'internal') {
                    checkStatusSertifikat();
                    $("#btnCetak").prop("disabled", true);

                } else {
                    if (FLAGBAYAR == 'True') {
                        if (checker == true) {
                            if (row != 'internal' && row == result.length) {

                                checkStatusSertifikat();
                                if (FlagTranscript == true) {
                                    alert = "Cetak Transkrip telah dilakukan, silahkan hubungi BAA untuk mengaktifkan kembali tombol cetak!";


                                    $("#btnCetak").prop("disabled", true);
                                    $('#notif').text(alert);
                                    $('#notif').show();
                                } else if (FlagTranscript == false) {
                                    $("#btnCetak").prop("disabled", false);

                                }


                            } else if (row == 'internal') {
                                checkStatusSertifikat();
                                $("#btnCetak").prop("disabled", true);
                            } else {
                                alert = "Nilai transkrip belum lengkap, menunggu penilaian dari dosen!";

                                $("#btnCetak").prop("disabled", true);
                                $("#sertifikatCetak").prop("disabled", true);
                                $('#notif').text(alert);
                                $('#notif').show();
                            }
                        } else {
                            alert = "Silahkan melengkapi feedback terlebih dahulu, untuk mengaktifkan tombol cetak!"

                            $("#btnCetak").prop("disabled", true);
                            $("#sertifikatCetak").prop("disabled", true);
                            $('#notif').text(alert);
                            $('#notif').show();
                        }

                    } else {
                        alert = "Harap menyelesaikan proses administrasi (pembayaran) terlebih dahulu!";
                        $('#notif').text(alert);
                        $('#notif').show();
                    }
                }


        }
    })

}

function parseFeedback(data) {
    var tempFeedback, tempPayment;

    for (var i = 0; i < data.length; i++) {
        if (data[i][4] == "Sudah Feedback" && data[i][5] == 'True') {
            tempFeedback = true;
            tempPayment = true;
        } else {
            return false;
        }
    }

    if (tempFeedback == true && tempPayment == true) {
        return true
    }
}