
function AddUserTemplate() {
    if ($('#created-user-template').length) {
        $('#TambahUser').modal('show');
    } else {
        $.ajax({
            url: '/Admin/UserManage/ModaladdUser',
            type: 'get',
            datatype: 'html',
            success: function (e) {
                //console.log(e);
                if ($('.data-content-modal').length) {
                    $('.data-content-modal').remove();
                }
                $('#modal-inner').append(e);
                $('.modal').modal('show');

                $("#fakultasCari").select2({
                    placeholder: "-- Pilih Fakultas --",
                    width: "100%",
                    ajax: {
                        url: "/Admin/UserManage/GetFakultas",
                        dataType: 'json',
                        method: "POST",
                        delay: 250,
                        cache: false,
                        data: function (params) {
                            return {
                                Search: params.term || "",
                                JenjangStudi: "S1",
                            };
                        },
                        processResults: function (data, params) {
                            return {
                                results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                            };
                        },
                    }
                });

                $('#idRole').change(function () {
                    var namaRole = document.getElementById("idRole");
                    var selectedRole = namaRole.options[namaRole.selectedIndex].text;
                    if (selectedRole != "Admin Fakultas") {//hide fakultas selectedRole != "Kepala Program Studi" && selectedRole != "Dosen" && 
                        $("#divFakultas").prop('hidden', true);
                        //if (selectedRole != "Kepala Program Studi" && selectedRole != "Dosen") {//close prodi
                            //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
                            $("#divProdi").prop('hidden', true);
                            //$("#specialRole").text("asd-BAA");
                            $("#specialRole").val(selectedRole);
                            console.log($("#specialRole").val());
                            console.log($("#txtKPTSDIN").val());
                            console.log($("#fakultasCari").val());
                            console.log($("#fakultasCari").text());
                        //}
                       
                        //else {//open prodi
                        //    $("#divProdi").prop('hidden', false);
                        //    console.log($("#txtKPTSDIN").val());
                        //    console.log($("#fakultasCari").val());
                        //    console.log($("#fakultasCari").text());

                        //}
                    }
                    else {
                        $("#divFakultas").prop('hidden', false); //open fakultas
                        //if (selectedRole != "Kepala Program Studi" && selectedRole != "Dosen") {//close prodi
                            //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
                            $("#divProdi").prop('hidden', true);
                            //$("#specialRole").text("asd-BAA");
                            $("#specialRole").val(selectedRole);
                            console.log($("#specialRole").val());
                            console.log($("#txtKPTSDIN").val());
                            console.log($("#fakultasCari").val());
                            console.log($("#fakultasCari").text());
                      // }
                        //else if (selectedRole != "Dosen") {
                        //    //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
                        //    $("#divProdi").prop('hidden', true);
                        //    //$("#specialRole").text("asd-BAA");
                        //    $("#specialRole").val(selectedRole);}
                        //else {//open prodi
                        //    $("#divProdi").prop('hidden', false);
                        //    console.log($("#txtKPTSDIN").val());
                        //    console.log($("#fakultasCari").val());
                        //    console.log($("#fakultasCari").text());

                        //}
                    }
                    
                    //console.log(selectedRole);
                })
                
                //var namaRole = selectedRole.substring(selectedRole.indexOf('-') + 1);
            }
        })
    }
}


function EditUserTemplate(id) {
    $.ajax({
        url: '/Admin/UserManage/ModalEditUser/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            //console.log(e);
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            var f = document.getElementById("inp_fakultas");
            var fs = f.options[f.selectedIndex].text;
            //console.log($("option:selected:first", "#inp_fakultas").val());
           // console.log($("#inp_fakultas").val());
            

            var getKelompok = $('#get_kelompok').val();
            //console.log(getKelompok);
            //$('#inp_fakultas').select2('data', { id: getKelompok, value: getKelompok, text: getKelompok });
            $("#inp_fakultas").select2({
                placeholder: "-- Pilih Fakultas --",
                width: "100%",
                ajax: {
                    url: "/Admin/UserManage/GetFakultas",
                    dataType: 'json',
                    method: "POST",
                    delay: 250,
                    cache: false,
                    data: function (params) {
                        return {
                            Search: params.term || "",
                            JenjangStudi: "S1",
                        };
                    },
                    processResults: function (data, params) {
                        return {
                            results: $.map(data, function (item) { return { id: item.ID, value: item.ID, text: item.Nama } })
                        };
                    },
                }
            });
            
            var namaRole = document.getElementById("idRole");
            var selectedRole = namaRole.options[namaRole.selectedIndex].text;
            //$("#divProdi").prop('hidden', false);
            //$("#divFakultas").prop('hidden', false);
           
            //if (selectedRole != "Admin Fakultas") {//hide fakultas selectedRole != "Kepala Program Studi" && selectedRole != "Dosen" &&
            //    //$("#divFakultas").prop('hidden', true);
            //    //$("#divProdi").prop('hidden', false);//tes
            //    if (selectedRole != "Kepala Program Studi" && selectedRole != "Dosen") {//hide prodi
            //        $("#divProdi").prop('hidden', true);
            //        //$("#specialRole").text("asd-BAA");
            //        $("#specialRole").val(selectedRole);
            //        //console.log($("#specialRole").val());
            //    }
            //    else {
            //        $("#divProdi").prop('hidden', false);

            //    }

            //}
            if (selectedRole == "Admin Fakultas") {//admin fak
                $("#divProdi").prop('hidden', true);
                $("#divFakultas").prop('hidden', false);

            }
            //else { kaprodi dosen
            //    $("#divProdi").prop('hidden', false);
            //    $("#divFakultas").prop('hidden', false);
            //}
           
            $('#idRole').change(function () {
                //console.log($("option:selected:first", "#inp_fakultas").val());
                //console.log($("#inp_fakultas").val());
                //console.log($('#inp_fakultas').select2('data')[0].value);
                $("#inp_fakultas").empty();
                var namaRole = document.getElementById("idRole");
                var selectedRole = namaRole.options[namaRole.selectedIndex].text;
                //if (selectedRole != "Admin Fakultas") {//hide fakultas selectedRole != "Kepala Program Studi" && selectedRole != "Dosen" && 
                //    $("#divFakultas").prop('hidden', true);
                //    $("#divProdi").prop('hidden', false);//tes
                //    if (selectedRole != "Kepala Program Studi" && selectedRole != "Dosen") {//hide prodi
                //        $("#divProdi").prop('hidden', true);
                //        //$("#specialRole").text("asd-BAA");
                //        $("#specialRole").val(selectedRole);
                //        //console.log($("#specialRole").val());
                //    }
                //    else {
                //        $("#divProdi").prop('hidden', false);

                //    }

                //}
                if (selectedRole == "Admin Fakultas") {//admin fak
                    $("#divProdi").prop('hidden', true);
                    $("#divFakultas").prop('hidden', false);

                }
                else {
                    $("#divProdi").prop('hidden', true);
                    $("#divFakultas").prop('hidden', true);
                }
               
                //console.log(selectedRole);
            })
        }
    })
}

function DetailUserTemplate(id) {
    $.ajax({
        url: '/Admin/UserManage/ModalDetailUser/' + id,
        type: 'get',
        datatype: 'html',
        success: function (e) {
            //console.log(e);
            if ($('.data-content-modal').length) {
                $('.data-content-modal').remove();
            }
            $('#modal-inner').append(e);
            $('.modal').modal('show');
            var namaRole = document.getElementById("idRole");
            var selectedRole = $('#idRole').val();
            console.log(selectedRole);
            if (selectedRole == "Admin Fakultas") {
                //$("#divProdi").prop('hidden', true);
                $("#divFakultas").prop('hidden', false);
            }
            //else if (selectedRole != "Kepala Program Studi" && selectedRole != "Dosen") {
                
            //    //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
            //    $("#divProdi").prop('hidden', true);
            //    $("#divFakultas").prop('hidden', true);
            //    //$("#specialRole").text("asd-BAA");
            //    //$("#specialRole").val(selectedRole);
            //    //console.log($("#specialRole").val());
               
            //}
            //else {
            //    $("#divProdi").prop('hidden', false);
                

            //}
        }
    })

}
function PostCreate2() {
    //getValueOnForm();
    console.log($('#fakultasCari').val());
    var formInputUser = new Object();
    var namaRole = document.getElementById("idRole");
    var selectedRole = namaRole.options[namaRole.selectedIndex].text;
    //if bukan role spesial (Kaprodi dan dosen)
    if (selectedRole == "Admin Fakultas") {
        formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
        formInputUser.UserName = $('input[id=txtnama]').val();
        formInputUser.Email = $('input[id=txtemail]').val();
        formInputUser.Password = $('input[id=txtpassword]').val();
        formInputUser.RoleID = $('#idRole').val();
        formInputUser.KodeFakultas = $('#fakultasCari').val();
        formInputUser.NamaFakultas = $("option:selected:first", "#fakultasCari").text();
        formInputUser.KPTSDIN = $('#txtKPTSDIN').val();
        formInputUser.KodeProdi = $("option:selected:first", "#fakultasCari").text();
        //formInputUser.NamaProdi = $('#idProdi').val();
        formInputUser.NamaProdi = "Admin Fakultas";//$('#specialRole').val();
        var cekAktif = $('input[id=inp_status]:checked').val();
        if (cekAktif == 1) {
            formInputUser.IsActive = "true";
        }
        else { formInputUser.IsActive = "false"; }
    }
    //else if (selectedRole == "Kepala Program Studi" || selectedRole == "Dosen") {
    //    var namaProdi = document.getElementById("idProdi");
    //    var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
    //    var namaProdi = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
    //    formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
    //    formInputUser.UserName = $('input[id=txtnama]').val();
    //    formInputUser.Email = $('input[id=txtemail]').val();
    //    formInputUser.Password = $('input[id=txtpassword]').val();
    //    formInputUser.RoleID = $('#idRole').val();
    //    formInputUser.KodeFakultas = $('#fakultasCari').val();
    //    formInputUser.NamaFakultas = $("option:selected:first", "#fakultasCari").text();
    //    formInputUser.KPTSDIN = $('#txtKPTSDIN').val();
    //    formInputUser.KodeProdi = $('#idProdi').val();
    //    //formInputUser.NamaProdi = $('#idProdi').val();
    //    formInputUser.NamaProdi = namaProdi;
    //    var cekAktif = $('input[id=inp_status]:checked').val();
    //    if (cekAktif == 1) {
    //        formInputUser.IsActive = "true";
    //    }
    //    else { formInputUser.IsActive = "false"; }
    //}
    else { //else role special
        var namaProdi = document.getElementById("idProdi");
        var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
        var namaProdi = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
        formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
        formInputUser.UserName = $('input[id=txtnama]').val();
        formInputUser.Email = $('input[id=txtemail]').val();
        formInputUser.Password = $('input[id=txtpassword]').val();
        formInputUser.RoleID = $('#idRole').val();
        //formInputUser.KodeProdi = $('#specialRole').val();
        formInputUser.KodeProdi = "";
        formInputUser.KodeFakultas = "";
        formInputUser.NamaFakultas = "";
        //formInputUser.NamaProdi = $('#idProdi').val();
        //formInputUser.NamaProdi = $('#specialRole').val();
        formInputUser.NamaProdi = "";
        formInputUser.KPTSDIN = $('#txtKPTSDIN').val();
        var cekAktif = $('input[id=inp_status]:checked').val();
        if (cekAktif == 1) {
            formInputUser.IsActive = "true";
        }
        else { formInputUser.IsActive = "false"; }
    }
    
    //formInputUser.IsActive = $('input[id=inp_status]:checked').val();

    if (validationCustom2()) {//&& formInputUser.KodeFakultas != null
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + '/Admin/UserManage/PostDataUser',
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(formInputUser),
            //contentType: 'application/json'
            contentType: 'application/json',
        }).then(function (response) {
            if (response.status == 400) {
                Swal.fire({
                    title: 'Gagal!',
                    icon: 'error',
                    html: 'EMAIL atau NIP telah terdaftar!',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                tableUser.ajax.reload(null, false);
                $('.modal').modal('hide');
                $('#txtnomorindukpegawai').val('');
                $('#txtemail').val('');
                //$('#TambahUser')[0].reset();
            }
            else
                if (response.status == 200) {
                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: 'User Baru Berhasil Ditambahkan',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    })
                    $("#divFakultas").prop('hidden', true);
                    tableUser.ajax.reload(null, false);
                    $('.modal').modal('hide');
                    $("#frmSave").trigger("reset");
                    //$('#TambahUser')[0].reset();

                }

            //response=0;
            //alert(response.message);
        });
        //        success: function (e) {
        //            Swal.fire({
        //                title: 'Berhasil',
        //                icon: 'success',
        //                html: 'User Baru Berhasil Ditambahkan',
        //                showCloseButton: true,
        //                showCancelButton: false,
        //                focusConfirm: false,
        //                confirmButtonText: 'OK'
        //            })
        //            tableUser.ajax.reload(null, false);
        //            $('.modal').modal('hide');
        //        },
        //        error: function (e) {
        //            Swal.fire({
        //                title: 'Oppss',
        //                icon: 'error',
        //                html: 'Coba Reload Page',
        //                showCloseButton: true,
        //                showCancelButton: false,
        //                focusConfirm: false,
        //                confirmButtonText: 'OK'
        //            })
        //            $('.modal').modal('hide');
        //        }
        //    })
    }
         else {
            Swal.fire({
                title: 'Oppss',
                icon: 'warning',
                html: 'Ada beberapa field yang belum kamu isikan',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
        }
}

function PostUpdateUser() {
    var formInputUser = new Object();
    var namaRole = document.getElementById("idRole");
    var selectedRole = namaRole.options[namaRole.selectedIndex].text;
    //if bukan role spesial (Kaprodi)
    if (selectedRole == "Admin Fakultas") {
        var namaProdi = document.getElementById("idProdi");
        var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
        var namaProdis = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
        formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
        formInputUser.UserName = $('input[id=txtnama]').val();
        formInputUser.Email = $('input[id=txtemail]').val();
        formInputUser.Password = $('input[id=txtpassword]').val();
        formInputUser.RoleID = $('#idRole').val();
        formInputUser.KodeProdi = $("option:selected:first", "#inp_fakultas").text();
        //formInputUser.NamaProdi = $('#idProdi').val();
        formInputUser.NamaProdi = "Admin Fakultas";

        //formInputUser.KodeFakultas = $('#inp_fakultas').select2('data')[0].value;
        formInputUser.KodeFakultas = $("option:selected:first", "#inp_fakultas").val();
        //formInputUser.KodeFakultas = $('#inp_fakultas').val();
        //$('#inp_fakultas').select2('data')[0].value
        formInputUser.NamaFakultas = $("option:selected:first", "#inp_fakultas").text();
        formInputUser.KPTSDIN = $('#txtKPTSDIN').val();

        var cekAktif = $('input[id=inp_status]:checked').val();
        if (cekAktif == 1) {
            formInputUser.IsActive = "true";
        }
        else { formInputUser.IsActive = "false"; }
        formInputUser.ID = $('#id_userTemplate').val();
    }
    
    //else if (selectedRole == "Kepala Program Studi" || selectedRole == "Dosen") {
    //    var namaProdi = document.getElementById("idProdi");
    //    //var kodeFakultas = document.getElementById("inp_fakultas");
    //    var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
    //    //var selecetedKodeFakultas = $("option:selected:first", "#inp_fakultas").val();
    //    var namaProdis = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
    //    formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
    //    formInputUser.UserName = $('input[id=txtnama]').val();
    //    formInputUser.Email = $('input[id=txtemail]').val();
    //    formInputUser.Password = $('input[id=txtpassword]').val();
    //    formInputUser.RoleID = $('#idRole').val();
    //    formInputUser.KodeProdi = $('#idProdi').val();
    //    //formInputUser.NamaProdi = $('#idProdi').val();
    //    formInputUser.NamaProdi = namaProdis;

    //    //formInputUser.KodeFakultas = $('#inp_fakultas').select2('data')[0].value;
    //    formInputUser.KodeFakultas = $("option:selected:first", "#inp_fakultas").val();
    //    //formInputUser.KodeFakultas = $('#inp_fakultas').select2('data')[0].value;
    //    //$('#inp_fakultas').select2('data')[0].value
    //    formInputUser.NamaFakultas = $("option:selected:first", "#inp_fakultas").text();
    //    formInputUser.KPTSDIN = $('#txtKPTSDIN').val();

    //    var cekAktif = $('input[id=inp_status]:checked').val();
    //    if (cekAktif == 1) {
    //        formInputUser.IsActive = "true";
    //    }
    //    else { formInputUser.IsActive = "false"; }
    //    formInputUser.ID = $('#id_userTemplate').val();
    //}
    else {//role tanpa fakultas dan prodi
        var namaProdi = document.getElementById("idProdi");
        var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
        var namaProdis = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
        formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
        formInputUser.UserName = $('input[id=txtnama]').val();
        formInputUser.Email = $('input[id=txtemail]').val();
        formInputUser.Password = $('input[id=txtpassword]').val();
        formInputUser.RoleID = $('#idRole').val();
        //formInputUser.KodeProdi = $('#specialRole').val();
        formInputUser.KodeProdi = "";
        //formInputUser.NamaProdi = $('#idProdi').val();
        //formInputUser.NamaProdi = $('#specialRole').val();
        formInputUser.NamaProdi = "";

        formInputUser.KodeFakultas = "";
        formInputUser.NamaFakultas = "";
        formInputUser.KPTSDIN = $('#txtKPTSDIN').val();

        var cekAktif = $('input[id=inp_status]:checked').val();
        if (cekAktif == 1) {
            formInputUser.IsActive = "true";
        }
        else { formInputUser.IsActive = "false"; }
        formInputUser.ID = $('#id_userTemplate').val();
    }
    
    //console.log(formInputUser);
    if (validationCustomEditUser()) {
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/UserManage/PostUpdateDataUser',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(formInputUser),
        contentType: 'application/json',
    }).then(function (response) {
        if (response.status == 400) {
            Swal.fire({
                title: 'Gagal!',
                icon: 'error',
                html: 'EMAIL atau NIP telah terdaftar!',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            tableUser.ajax.reload(null, false);
            $('.modal').modal('hide');
        }
        else
            if (response.status == 200) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: 'User Baru Berhasil Diedit',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                tableUser.ajax.reload(null, false);
                $('.modal').modal('hide');
                

            }
    });
    
}
         else {
    Swal.fire({
        title: 'Oppss',
        icon: 'warning',
        html: 'Ada beberapa field yang belum kamu isikan',
        showCloseButton: true,
        showCancelButton: false,
        focusConfirm: false,
        confirmButtonText: 'OK'
    })
}
}
/*function PostUpdateUser() {
    var formInputUser = new Object();
    var namaProdi = document.getElementById("idProdi");
    var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
    var namaProdis = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
    formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
    formInputUser.UserName = $('input[id=txtnama]').val();
    formInputUser.Email = $('input[id=txtemail]').val();
    formInputUser.Password = $('input[id=txtpassword]').val();
    formInputUser.RoleID = $('#idRole').val();
    formInputUser.KodeProdi = $('#idProdi').val();
    //formInputUser.NamaProdi = $('#idProdi').val();
    formInputUser.NamaProdi = namaProdis;
    var cekAktif = $('input[id=inp_status]:checked').val();
    if (cekAktif == 1) {
        formInputUser.IsActive = "true";
    }
    else { formInputUser.IsActive = "false"; }
    formInputUser.ID = $('#id_userTemplate').val();
    console.log(formInputUser);
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + '/Admin/UserManage/PostUpdateDataUser',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(formInputUser),
        contentType: 'application/json',
        success: function (e) {
            Swal.fire({
                title: 'Berhasil',
                icon: 'success',
                html: 'Data User Berhasil Diubah',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            tableUser.ajax.reload(null, false);
            $('.modal').modal('hide');
        },
        error: function (e) {
            Swal.fire({
                title: 'Oppss',
                icon: 'error',
                html: 'Coba Reload Page',
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
                confirmButtonText: 'OK'
            })
            $('.modal').modal('hide');
        }
    })
}*/
