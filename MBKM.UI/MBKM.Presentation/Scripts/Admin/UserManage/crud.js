
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
                
                $('#idRole').change(function () {
                    var namaRole = document.getElementById("idRole");
                    var selectedRole = namaRole.options[namaRole.selectedIndex].text;
                    if (selectedRole != "Kepala Program Studi") {
                        //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
                        $("#divProdi").prop('hidden', true);
                        //$("#specialRole").text("asd-BAA");
                        $("#specialRole").val(selectedRole);
                        console.log($("#specialRole").val());
                    }
                    else {
                        $("#divProdi").prop('hidden', false);

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
            var namaRole = document.getElementById("idRole");
            var selectedRole = namaRole.options[namaRole.selectedIndex].text;
            if (selectedRole != "Kepala Program Studi") {
                //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
                $("#divProdi").prop('hidden', true);
                //$("#specialRole").text("asd-BAA");
                $("#specialRole").val(selectedRole);
                console.log($("#specialRole").val());
            }
            else {
                $("#divProdi").prop('hidden', false);

            }
            $('#idRole').change(function () {
                var namaRole = document.getElementById("idRole");
                var selectedRole = namaRole.options[namaRole.selectedIndex].text;
                if (selectedRole != "Kepala Program Studi") {
                    //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
                    $("#divProdi").prop('hidden', true);
                    //$("#specialRole").text("asd-BAA");
                    $("#specialRole").val(selectedRole);
                    console.log($("#specialRole").val());
                }
                else {
                    $("#divProdi").prop('hidden', false);

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
            if (selectedRole != "Kepala Program Studi") {
                //$('#specialRole').html('<input type="text" value="BAA" class="form-control input-pendaftaran input-data" disabled="disabled" id="specialRole" name="specialRole">')
                $("#divProdi").prop('hidden', true);
                //$("#specialRole").text("asd-BAA");
                $("#specialRole").val(selectedRole);
                //console.log($("#specialRole").val());
               
            }
            else {
                $("#divProdi").prop('hidden', false);

            }
        }
    })

}
function PostCreate2() {
    //getValueOnForm();
    var formInputUser = new Object();
    var namaRole = document.getElementById("idRole");
    var selectedRole = namaRole.options[namaRole.selectedIndex].text;
    //if bukan role spesial (Kaprodi)
    if (selectedRole == "Kepala Program Studi") {
        var namaProdi = document.getElementById("idProdi");
        var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
        var namaProdi = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
        formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
        formInputUser.UserName = $('input[id=txtnama]').val();
        formInputUser.Email = $('input[id=txtemail]').val();
        formInputUser.Password = $('input[id=txtpassword]').val();
        formInputUser.RoleID = $('#idRole').val();
        formInputUser.KodeProdi = $('#idProdi').val();
        //formInputUser.NamaProdi = $('#idProdi').val();
        formInputUser.NamaProdi = namaProdi;
        var cekAktif = $('input[id=inp_status]:checked').val();
        if (cekAktif == 1) {
            formInputUser.IsActive = "true";
        }
        else { formInputUser.IsActive = "false"; }
    }
    else { //else role special
        var namaProdi = document.getElementById("idProdi");
        var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
        var namaProdi = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
        formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
        formInputUser.UserName = $('input[id=txtnama]').val();
        formInputUser.Email = $('input[id=txtemail]').val();
        formInputUser.Password = $('input[id=txtpassword]').val();
        formInputUser.RoleID = $('#idRole').val();
        formInputUser.KodeProdi = $('#specialRole').val();
        //formInputUser.NamaProdi = $('#idProdi').val();
        formInputUser.NamaProdi = $('#specialRole').val();
        var cekAktif = $('input[id=inp_status]:checked').val();
        if (cekAktif == 1) {
            formInputUser.IsActive = "true";
        }
        else { formInputUser.IsActive = "false"; }
    }
    
    //formInputUser.IsActive = $('input[id=inp_status]:checked').val();

    if (validationCustom2()) {
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
    if (selectedRole == "Kepala Program Studi") {
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
    }
    else {
        var namaProdi = document.getElementById("idProdi");
        var selectedProdi = namaProdi.options[namaProdi.selectedIndex].text;
        var namaProdis = selectedProdi.substring(selectedProdi.indexOf('-') + 1);
        formInputUser.NoPegawai = $('input[id=txtnomorindukpegawai]').val();
        formInputUser.UserName = $('input[id=txtnama]').val();
        formInputUser.Email = $('input[id=txtemail]').val();
        formInputUser.Password = $('input[id=txtpassword]').val();
        formInputUser.RoleID = $('#idRole').val();
        formInputUser.KodeProdi = $('#specialRole').val();
        //formInputUser.NamaProdi = $('#idProdi').val();
        formInputUser.NamaProdi = $('#specialRole').val();
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
