console.log("hallo");

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

var getBirtDay = $('#birthday').text();
var date = getBirtDay.split(" ")[0];
var parts = date.split('/');
var mydate = new Date(parts[2], parts[1] - 1, parts[0]);

var result = mydate.toShortFormat();


function convertBirthday() {
    var getBirtDay = $('#birthday').text();
    var date = getBirtDay.split(" ")[0];
    var parts = date.split('/');
    var mydate = new Date(parts[2], parts[1] - 1, parts[0]);

    var result = mydate.toShortFormat();

    $('#birthday').text(result);
    $('#birthdayView').text(result);
}



/* --responsive */
$(document).ready(function () {

    $("#print").hide();

    var date = new Date();
    $("#currentDate").text(date.toShortFormat());
    $("#currentDatePrint").text(date.toShortFormat());
    convertBirthday();
    getNilai();
    isZooming();
    
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

function getNilai() {
    //var base_url = window.location.origin;
    $.ajax({
        url: '/Portal/TranskripMahasiswa/getTranskrip/',
        type: 'POST',
        datatype: 'json',
        success: function (result) {

            Nilais = result;
            showValue(result);

        }
    })
}


function showValue(result) {
    $("#data").empty();
    var row = "";
    var html = "";

    var gA = "A";
    var gAmin = "A-";
    var gBplus = "B+";
    var gB = "B";
    var gBmin = "B-";
    var gCplus = "C+";
    var gC = "C";
    var gD = "D";
    var gE = "E";


    var bA = '4.00';
    var bAmin = '3.70';
    var bBplus = '3.30';
    var bB = '3.00';
    var bBmin = '2.70';
    var bCplus = '2.30';
    var bC = '2.00';
    var bD = '1.00';
    var bE = '0';


    var A = parseInt(bA);
    var Amin = parseInt(bAmin);
    var Bplus = parseInt(bBplus);
    var B = parseInt(bB);
    var Bmin = parseInt(bBmin);
    var Cplus = parseInt(bCplus);
    var C = parseInt(bC);
    var D = parseInt(bD);
    var E = parseInt(bE);

    var nilaiTotal = 0;
    var rowNilaiSks = 0;


    for (var i = 0; i < result.length; i++) {
        var kodematakuliah = "<td>" + result[i].KodeMataKuliah + "</td>";
        var matakuliah = "<td>" + result[i].NamaMataKuliah + "</td>";
        var grade = result[i].Grade;
        var gradeHtml = "<td style='text-align:right;'>" + grade + "</td>";
        var sksInt = parseInt(result[i].SKS);
        var sks = "<td style='text-align:right;'>" + sksInt + "</td>";
        //var nilai = result[i].Nilai;



        sksTotal = sksTotal + sksInt;

        row = "<tr>" + kodematakuliah + matakuliah + sks + gradeHtml + "</tr>";


        console.log("sks : " + sksInt);
        if (grade == gA) {
            rowNilaiSks = sksInt * A;

            console.log("grade : " + A);
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
        }

        nilaiTotal = nilaiTotal + rowNilaiSks;
        console.log(rowNilaiSks);
        console.log(nilaiTotal);

        html = html + row;
    }
    console.log(html);

    gradeTotal = nilaiTotal / sksTotal;



    var A = 4.00;
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
    }

        $("#data").html(html);
        $("#totalSks").html(sksTotal);
        $("#totalGrade").html(gradeTotal);

        $("#dataPrint").html(html);
        $("#totalSksPrint").html(sksTotal);
        $("#totalGradePrint").html(gradeTotal);

}


//---print
function print(id, nim) {

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
                    idMahasiswa: idMahasiswa
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
                        $("#view").hide();
                        $("#print").show();
                        print();
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
                        var mywindow = window.open('', 'height=auto,width=auto,_blank');
                        mywindow.document.write('<html><head><title>Transkrip</title>');
                        /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
                        mywindow.document.write('</head><body >');
                        mywindow.document.write(data);
                        mywindow.document.write('</body></html>');

                       
                        mywindow.print();
                        mywindow.close();

                        $("#view").show();
                        $("#print").hide();
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