var url = window.location.href;
var jsData = {};
tempId = url.substring(url.lastIndexOf('/') + 1);



var table = $('#table-data-list-mahasiswa').DataTable({
    "ajax": {
        url: '/Admin/ManageJadwalUjian/GetDataTableMahasiswa/',
        data: {
            dataID: tempId
        },
        dataSrc : "",
        type: 'POST'
    },
    'columnDefs': [
        {
            'targets': 0,
            'checkboxes': {
                'selectRow': true
            }
        },
        {
            'targets': 1,
            "data": 1,
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center vertical-center" style="font-size: 0.8vw"> - </div>'
                } else {
                    return '<div class="center vertical-center" style="font-size: 0.8vw">' + data +'</div>';
                }
            }
        },
        {
            'targets': 2,
            "data": 2,
            "render": function (data, type, row, meta) {
                return '<div class="center vertical-center" style="font-size: 0.8vw">' + data + '</div>';
            }
        }

    ],
    'select': {
        'style': 'multi'
    },
    'order': [[1, 'asc']],
    "createdRow": function (row, data, index) {
        $('td', row).css({
            'border': '1px solid coral',
            'border-collapse': 'collapse',
            'vertical-align': 'center',
        });
    }
});

$('#frm-example').on('submit', function (e) {
    e.preventDefault();
    var form = this;

    var rows_selected = table.column(0).checkboxes.selected();

    $.each(rows_selected, function (index, rowId) {
        $(form).append(
            $('<input>')
                .attr('type', 'hidden')
                .attr('name', 'id[]')
                .val(rowId)
        );
    });
    console.log(rows_selected.join(","));
    $('input[name="id\[\]"]', form).remove();

    jsData.IdData = tempId;
    jsData.dataMahasiswa = rows_selected.join(",");

    $.ajax({
        url: '/Admin/ManageJadwalUjian/CheckData/',
        type: 'post',
        datatype: 'json',
        data: JSON.stringify(jsData),
        contentType: 'application/json',
        success: function (e) {
            if (e.status == 300) {
                Swal.fire({
                    title: 'Oppss',
                    icon: 'error',
                    html: e.message,
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
            } else if (e.status == 400) {
                swal.fire({
                    title: "Apakah anda yakin?",
                    type: "warning",
                    icon: "warning",
                    html: e.message,
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes!",
                    closeOnConfirm: false
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.LoadingOverlay("show");
                        $.ajax({
                            url: '/Admin/ManageJadwalUjian/PostDataMahasiswa/',
                            type: 'post',
                            datatype: 'json',
                            data: JSON.stringify(jsData),
                            contentType: 'application/json',
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
                                    Swal.fire({
                                        title: 'Berhasil',
                                        icon: 'success',
                                        html: 'Data Ujian Sudah Terbaharui',
                                        showCloseButton: true,
                                        showCancelButton: false,
                                        focusConfirm: false,
                                        confirmButtonText: 'OK'
                                    })
                                    window.location.href ="/Admin/ManageJadwalUjian"
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
            } else {
                $.ajax({
                    url: '/Admin/ManageJadwalUjian/PostDataMahasiswa/',
                    type: 'post',
                    datatype: 'json',
                    data: JSON.stringify(jsData),
                    contentType: 'application/json',
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
                            Swal.fire({
                                title: 'Berhasil',
                                icon: 'success',
                                html: 'Data Ujian Sudah Terbaharui',
                                showCloseButton: true,
                                showCancelButton: false,
                                focusConfirm: false,
                                confirmButtonText: 'OK'
                            })
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
        }
    })
});