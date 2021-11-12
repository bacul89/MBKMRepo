var semester;

function printDHU(Id) {

    var base_url = window.location.origin;
    var url = base_url + "/Admin/DaftarHadirUjian/GetDHU/";

    $.ajax({
        url: url,
        type: 'POST',
        datatype: 'json',
        data: { ID: Id },
        success: function (result) {
            $.LoadingOverlay("show");
            var HTML = "";
            var ujian = result[0].Value;
            var semester = result[1].Value;
            var mahasiswa = result[2].Value;

            console.log(result[0].Value);
            console.log(result[1].Value);
            console.log(result[2].Value);


            // component
            var header, sidebar, content, footer;

            // config
            var defaultRow = 20;
            var page = 0;
            var pageTotal = getTotalPage(defaultRow, mahasiswa.length);

            for (var i = 0; i < mahasiswa.length; i++) {
                if (i % 20 == 0) {
                    page++;
                    header = generateHeader(ujian, semester, page, pageTotal);
                    sidebar = generateSidebar(mahasiswa.length);
                    content = generateContent(mahasiswa, page, pageTotal, defaultRow);
                    footer = generateFooter();
                    HTML = HTML + generatePage(header, content, footer, sidebar);
                }
            }






            //var assets = generateStyle();
            var mywindow = window.open('', '_blank');
            mywindow.document.write('<html><head>');
            //mywindow.document.write(assets);
            mywindow.document.write('<link href="'+base_url+'/Content/select2.min.css" rel="stylesheet">');
            //mywindow.document.write('<script src="../../Scripts/modernizr-2.8.3.js"></script>');
            //mywindow.document.write('<script src="../../Scripts/jquery-3.4.1.js"></script>');
            mywindow.document.write('<link href="'+base_url+'/Content/bootstrap.css" rel="stylesheet">');
            mywindow.document.write('<link href="'+base_url+'/Content/site.css" rel="stylesheet">');
            //mywindow.document.write('<script src="../../Scripts/bootstrap.js"></script>');
            mywindow.document.write('<link href="' + base_url+'/Content/Admin/PrintDHU.css" rel="stylesheet" media="print">');
            mywindow.document.write('<link href="' + base_url+'/Content/Admin/PrintDHUStyle.css" rel="stylesheet">');

            mywindow.document.write('</head></html>');
            mywindow.document.write('<body id="body" style="padding: 0 0 0 0px; float: left; font-size:12px">');
            mywindow.document.write(HTML);
            mywindow.document.write('</body></html>');
            /*mywindow.print();
            mywindow.close();*/

            setTimeout(function () {
                mywindow.print();
                mywindow.close();
            }, 500);
            
            /*Swal.fire({
                title: 'success',
                icon: 'success',
                html: 'Cetak Berhasil',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })*/
            $.LoadingOverlay("hide");



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



//---<> Component
function generateContent(mahasiswa, page, pageTotal, limit) {

    var tablemhs = '';

    var limitPage = page * limit;
    var numEnd = limitPage;
    var numStart = limitPage - limit;
    var data = mahasiswa;//filterData(mahasiswa, numStart, numEnd);

    //console.log(data);



    if (data.length < numEnd) {
        for (var i = numStart; i < data.length; i++) {
            var num = i + 1;
            var nama = data[i].Nama;
            var nim = data[i].StudentID;
            //var idCampus = mahasiswa[i].NoKerjasama;
            tablemhs = tablemhs + '<tr><td class="mhsList start padding0"><center>' + num + '<center></td><td class="mhsList">' + nim + '</td><td class="mhsList">' + nama + '</td><td class="mhsList"></td></tr>'
        }

        for (var i = 0; i < numEnd - data.length; i++) {
            //var idCampus = mahasiswa[i].NoKerjasama;
            tablemhs = tablemhs + '<tr><td class="mhsList start heightDefault"><center><center></td><td class="mhsList heightDefault"></td><td class="mhsList heightDefault"></td><td class="mhsList heightDefault"></td></tr>'
        }

    } else {

        for (var i = numStart; i < numEnd; i++) {
            var num = i + 1;
            var nama = data[i].Nama;
            var nim = data[i].StudentID;
            //var idCampus = mahasiswa[i].NoKerjasama;
            tablemhs = tablemhs + '<tr><td class="mhsList start"><center>' + num + '<center></td><td class="mhsList">' + nim + '</td><td class="mhsList">' + nama + '</td><td class="mhsList"></td></tr>'
        }
    }


    var content = `<div id="content" class="col-md-8 col-lg-8 col-sm-8 col-xs-8 p-l15" style="padding-left:0px">
                                <table width="100%">
                                    <thead>
                                        <tr>
                                            <th class="mhsList start"><center>No.</center></th>
                                            <th class="mhsList"><center>STUDENT ID</center></th>
                                            <th class="mhsList" style="width:56%"><center>NAMA MAHASISWA</center></th>
                                            <th class="mhsList"><center>TANDA TANGAN</center></th>
                                        </tr>
                                    </thead>
                                    <tbody id="mhs">
                                        ${tablemhs}
                                    </tbody>
                                </table>
                            </div>`;
    return content;
}

function generateHeader(ujian, semester, page, pageTotal) {
    var base_url = window.location.origin;
    var komponen = ujian.Komponen;

    console.log(semester);

    var header = `<div class="border col-md-12 col-lg-12 col-sm-12 col-xs-12  p0">
                            <div class="col-md-12 Header p0">
                                <div class="col-md-1 col-sm-1 col-lg-1 col-xs-1 p0">
                                    <img class="logo" src="${base_url}/Asset/Lambang_Atma_Jaya.png" />
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6 col-xs-6 Title p0" style="padding-left: 25px;">
                                    <b>
                                        <span> UNIVERSITAS KATOLIK INDONESIA</span>
                                        <h1>ATMA JAYA</h1>
                                    </b>
                                </div>
                                <div class="col-md-5 col-sm-5 col-lg-5 col-xs-5 Title p0">
                                    <div class="title-right">
                                        <i>
                                            <h1 style="font-size: 29px;"><b>DHU</b></h1>
                                            <h2>DAFTAR HADIR UJIAN</h2>
                                        </i>
                                    </div>
                                </div>
                            </div>

                            <table class="info col-md-12 col-lg-12 col-sm-12 col-xs-12  p0">
                                <tr>
                                    <td class="p-tb10"><b>SEMESTER</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" style="width: 42%;">${semester}</td>
                                    <td class="space"></td>
                                    <td class="p-tb8"><b>SEKSI</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="seksi" style="width:21%">${ujian.ClassSection}</td>
                                </tr>
                                <tr>
                                    <td class="p-tb10"><b>PROGRAM STUDI</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="prodi">${ujian.NamaProdi}</td>
                                    <td class="space"></td>
                                    <td class="p-tb8"><b>KOMPONEN</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="komponen">${komponen}</td>
                                </tr>
                                <tr>
                                    <td class="p-tb10"><b>KODE MATAKULIAH</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="kodeMK">${ujian.KodeMatkul}</td>
                                    <td class="space"></td>
                                    <td class="p-tb8"><b>HARI / TGL</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="tgl">${convertMilisecondToDate(ujian.TanggalUjian)}</td>
                                </tr>
                                <tr>
                                    <td class="p-tb10"><b>NAMA MATAKULIAH</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="namaMK">${ujian.NamaMatkul}</td>
                                    <td class="space"></td>
                                    <td class="p-tb8"><b>JAM</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="jam">${ujian.JamMulai} - ${ujian.JamAkhir}</td>
                                </tr>
                                <tr>
                                    <td class="p-tb10"><b>SKS</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="sks">${ujian.SKS}</td>
                                    <td class="space"></td>
                                    <td class="p-tb8"><b>RUANG</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="ruang">${ujian.RuangUjian}</td>
                                </tr>
                                <tr>
                                    <td class="p-tb10"><b>DOSEN</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10" id="dosen">${ujian.Dosen} - ${ujian.NamaDosen}</td>
                                    <td class="space"></td>
                                    <td class="p-tb8"><b>HALAMAN</b></td>
                                    <td class="p-tb10lr5">:</td>
                                    <td class="p-tb10">${page} DARI ${pageTotal}</td>
                                </tr>
                            </table>
                        </div>`;

    return header
}

function generateSidebar(totalMhs) {
    var sidebar = `<div id="sidebar" class="col-md-4 col-lg-4 col-sm-4 col-xs-4 p0">
                                <table class="berita" width="100%">
                                    <thead>
                                        <tr>
                                            <th colspan="2" style="padding: 10px;"><center>BERITA ACARA UJIAN</center></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="2" style=" height: 333px; padding: 15px; vertical-align: 10px;">
                                                Waktu : .....Jam
                                            </td>
                                        </tr>
                                        <tr rowspan="3">
                                            <td colspan="2">
                                                <center>
                                                    <table style=" margin: 20px 48px;">
                                                        <tr>
                                                            <td class="borderNone pading0">Tercetak</td>
                                                            <td class="borderNone pading5">:</td>
                                                            <td class="borderNone pading0" style="text-align:right"><span id="tercetak">${totalMhs}</span> Orang</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="borderNone pading0">Hadir</td>
                                                            <td class="borderNone pading5">:</td>
                                                            <td class="borderNone pading0" style="border-bottom:solid 1px!important">.....Orang</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="borderNone pading0">Tidak Hadir </td>
                                                            <td class="borderNone pading5">:</td>
                                                            <td class="borderNone pading0">.....Orang</td>
                                                        </tr>
                                                    </table>
                                                </center>
                                            </td>
                                        </tr>
                                    </tbody>

                                    <tbody>
                                        <tr>
                                            <td class="mhsList"><b>NAMA PENGAWAS UJIAN</b></td>
                                            <td class="mhsList"><b>PARAF</b></td>
                                        </tr>
                                        <tr>
                                            <td class="heightDefault"></td>
                                            <td class="heightDefault"></td>
                                        </tr>
                                        <tr>
                                            <td class="heightDefault"></td>
                                            <td class="heightDefault"></td>
                                        </tr>
                                        <tr>
                                            <td class="heightDefault"></td>
                                            <td class="heightDefault"></td>
                                        </tr>
                                        <tr>
                                            <td class="heightDefault"></td>
                                            <td class="heightDefault"></td>
                                        </tr>
                                        <tr>
                                            <td class="heightDefault"></td>
                                            <td class="heightDefault"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>`;

    return sidebar;
}

function generateFooter() {
    var footer = `<div style=" margin-top:10px; width:100%" class="left"><span>........ : CEKAL</span></div>
                            <br class="left" />
                            <br />
                            <div class="border col-md-8 col-sm-8 col-lg-8 col-xs-8 p0" style=" font-size: 11px; margin-top:10px; padding: 8px 15px!important;">
                                <h3 style="font-size: 13px;font-weight: bold; margin-bottom: 5px;">Perhatian !!!</h3>
                                <ol>
                                    <li>Mahasiswa yang berhak mengikuti ujian adalah mereka yang namanya tercantm dalam DHU ini.</li>
                                    <li>Tidak diperkenankan menambahkan peseta ujian dengan alasan apapun juga.</li>
                                </ol>
                            </div>`;

    return footer;
}

function generatePage(header, content, footer, sidebar) {
    var page = `<div id="page" class="page col-md-12 col-lg-12 col-sm-12 col-xs-12">
            ${header} ${content} ${sidebar} ${footer}
            </div>`;
    return page;
    //$("#body").append(page);
}

function generateStyle() {
    var assets = `
            <meta charset="utf-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Print Daftar Hadir Ujian (DHU) - SIMBKM</title>
        `;

    return assets;
}

//--<> Helper
function convertDate(getDate) {



    var date = getDate.split("T")[0];

    console.log(date);
    var parts = date.split('-');
    var createdate = new Date(parts[0], parts[1] - 1, parts[2]);

    var result = weekday[createdate.getDay()] + ' / ' + createdate.getDate() + ' ' + month[createdate.getMonth()] + ' ' + createdate.getFullYear();
    
    return result;
}

function convertMilisecondToDate(value) {
    const month = new Array();
    month[0] = "Januari";
    month[1] = "Februari";
    month[2] = "Maret";
    month[3] = "April";
    month[4] = "Mei";
    month[5] = "Juni";
    month[6] = "Juli";
    month[7] = "Augustus";
    month[8] = "September";
    month[9] = "Oktober";
    month[10] = "November";
    month[11] = "Desember";


    const weekday = new Array(7);
    weekday[0] = "Minggu";
    weekday[1] = "Senin";
    weekday[2] = "Selasa";
    weekday[3] = "Rabu";
    weekday[4] = "Kamis";
    weekday[5] = "Juma't";
    weekday[6] = "Sabtu";

    var num = parseInt(value.match(/\d+/), 10)
    console.log(num);
    var createdate = new Date(num);
    console.log(createdate);

    var result = weekday[createdate.getDay()] + ' / ' + createdate.getDate() + ' ' + month[createdate.getMonth()] + ' ' + createdate.getFullYear();

    return result;

}



function getTotalPage(limit, mhsLenght) {
    var page = 0;
    //mhsLenght = 112;
    //console.log(mhsLenght);
    //console.log(limit);
    if (mhsLenght < limit) {
        page = 1;
        //console.log('hello');
    } else if (mhsLenght > limit) {
        page = parseInt(mhsLenght / limit);

        if (mhsLenght % limit > 0) {
            page++;
        }
    }

    // console.log(page);
    return page;
}