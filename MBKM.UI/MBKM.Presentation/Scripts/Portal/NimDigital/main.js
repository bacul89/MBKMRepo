﻿$(document).ready(function () {

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

                if (result.nim == null) {
                    $("#NIM").text(result.nim);
                } else {
                    $("#NIM").text("Empty..");
                }
                
                $("#nama").text(result.Nama);
                $("#universitas").text(result.NamaUniversitas);
                $("#prodi").text(result.Prodi);

                $("#photoprofile").attr("src", photoprofile);
            }
        })
    }

    LoadNim();

});