//---lookup
function getLookupBAA(tipe) {

    $.ajax({
        url: "/Admin/MasterMapingCapaianPembelajaran/getLookupByTipe",
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
/*
var getBirtDay = $('#birthday').text();
var date = getBirtDay.split("T")[0];
var parts = date.split('/');
var mydate = new Date(parts[2], parts[1] - 1, parts[0]);

var result = mydate.toShortFormat();*/


function convertBirthday(value) {


    //console.log(value);

    var date = value.split("T")[0];
    var parts = date.split('-');
    var mydate = new Date(parts[0], parts[1] - 1, parts[2]);

    var result = mydate.toShortFormat();

    return result;
    //$('#birthday').text(result);
    //$('#birthdayView').text(result);
}



/* --responsive */
$(document).ready(function () {

    $("#print").hide();

    var date = new Date();
    //$("#currentDate").text(date.toShortFormat());
    $("#currentDatePrint").text(date.toShortFormat());
    //convertBirthday();
    //getNilai();
    //isZooming();
    getLookupBAA('KepalaBiroAdministrasiAkademik');

});


//---Grade
var gradeFinal = "";
var sksTotal = 0;
var Nilais;


//---GetLookup Grade
/*function getNilai() {
    //var base_url = window.location.origin;
    $.ajax({
        url: '/Admin/Transkrip/getTranskrip/',
        type: 'POST',
        datatype: 'json',
        success: function (resultTranskip) {

            Nilais = resultTranskip;

            $.ajax({
                url: "/MasterMapingCapaianPembelajaran/getLookupByTipe",
                type: 'get',
                datatype: 'html',
                data: { Tipe: 'NilaiGrade' },
                success: function (resultLookup) {
                    NilaiGrades = resultLookup;
                    showValue(resultTranskip, resultLookup);

                }
            })

        }
    })
}*/

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

    //console.log(Nilais);

    for (var i = 0; i < result.length; i++) {
        if (i == 0) {
            //console.log(Nilais[0].tanggalLahir);
            var tempatLahir = Nilais[0].TempatLahir;
            var tanggalLahir = convertBirthday(Nilais[0].TanggalLahir);
            var ttl = tempatLahir + ' / ' + tanggalLahir;
            var jenjangStudi = Nilais[0].JenjangStudi;
            $('#name').text(Nilais[0].Nama);
            $('#nim').text(Nilais[0].NIM);
            $('#ttl').text(ttl);
            $('#jenjangStudi').text(jenjangStudi);
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

    console.log(gradeTotal);

    $("#data").html(html);
    $("#totalSks").html(sksTotal);
    $("#totalGrade").html(intToFloat(gradeTotal, 2));



    $("#dataPrint").html(html);
    $("#totalSksPrint").html(sksTotal);
    $("#totalGradePrint").html(intToFloat(gradeTotal, 2));
}


//---print
function print(id, nim) {
    var idMahasiswa = parseInt(id);
    $.LoadingOverlay("show");
    var base_url = window.location.origin;

    $.ajax({
        url: '/Admin/Transkrip/getTranskripByIdMahasiswa/',
        type: 'POST',
        datatype: 'json',
        data: { id: idMahasiswa },
        success: function (resultTranskip) {

            Nilais = resultTranskip;



                    $.ajax({
                        url: "/Admin/Transkrip/getLookupByTipe",
                        type: 'get',
                        datatype: 'html',
                        data: { Tipe: 'NilaiGrade' },
                        success: function (resultLookup) {
                            

                            NilaiGrades = resultLookup;
                            showValue(resultTranskip, resultLookup);
                            
                            var data = $("#print").html();
                            var mywindow = window.open('', '_blank');
                            mywindow.document.write('<html><head><title>Transkrip</title>');
                            mywindow.document.write('<link rel="stylesheet" href="' + base_url+'/Content/Portal/Transkrip/print.css" type="text/css" media="print" />');
                            mywindow.document.write('</head><body>');
                            mywindow.document.write(data);
                            mywindow.document.write('</body></html>');

                            //mywindow.print();
                            //setTimeout(function () { window.print(); }, 500);
                            //mywindow.onfocus = function () { setTimeout(function () { window.close(); }, 500); }
                            //mywindow.close();
                            setTimeout(function () {
                                mywindow.print();
                                mywindow.close();
                            }, 500);
                    
                            Swal.fire({
                                title: 'success',
                                icon: 'success',
                                html: 'Cetak Berhasil',
                                showCloseButton: true,
                                showCancelButton: false,
                                focusConfirm: false,
                                confirmButtonText: 'OK'
                            })
                            $.LoadingOverlay("hide");
                            //



/*                            swal.fire({
                                title: "Transkrip Nilai Hanya Dapat Tercetak Satu Kali \n Hubungi Pihak BAA Jika Terjadi Gagal Cetak",
                                type: "warning",
                                icon: "warning",
                                showCancelButton: true,
                                confirmButtonColor: "#06a956",
                                confirmButtonText: "Ok",
                                //closeOnConfirm: false
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $.LoadingOverlay("hide");
                                }
                            })*/
                        },
                        error: function (e) {
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
                
 

        },
        error: function (e) {
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