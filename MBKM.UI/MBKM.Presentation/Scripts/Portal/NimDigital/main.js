$(document).ready(function () {

    isZooming();

    function LoadNim() {
        var base_url = window.location.origin;
        $.ajax({
            url: '/Portal/NimDigital/getNim/',
            type: 'get',
            datatype: 'json',
            success: function (result) {

                console.log(result);
                /*console.log();*/
                /*NIM
                nama           
                photoprofile*/

                var photoprofile = base_url+"/"+result.PhotoProfile;
                //console.log(result.NIM);

                if (typeof (result.NIM) != "undefined") {
                    $("#NIM").text(result.NIM);
                } else {
                    $("#NIM").text("NIM tidak tersedia..");
                }
                console.log(result.pertukaran);
                if (result.pertukaran == true) {
                    $("#prodi").text(result.Prodi);
                } else {
                    $("#prodi").hide();                    
                }
                
                $("#nama").text(result.Nama);
                $("#universitas").text(result.NamaUniversitas);
                $("#photoprofile").attr("src", photoprofile);
            }
        })
    }

    LoadNim();

});


$(document).ready(function () {

    
});


$(window).resize(function () {
    isZooming();
});


function isZooming() {
    var defaultH = 600;
    var square = $('.responsive-content');
    var screenH = $(document).height();
    //console.log("dfH :"+defaultH);
    //console.log("wnH :"+screenH);


    if (defaultH < screenH) {

        var footer = 60;
        var header = 105;
        var contentHeight = screenH - header - footer;

        if (defaultH < contentHeight) {
            square.css('height', contentHeight);
        } else {
            square.css('height', defaultH);
        }


    } else {
        square.css('height', defaultH);
    }
}