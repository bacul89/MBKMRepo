$(document).ready(function (e) {
    $('#table-data-approval-pendaftaran-makul').DataTable({
        "createdRow": function (row, data, index) {
            $('td', row).css({
                'border': '1px solid coral',
                'border-collapse': 'collapse',
                'vertical-align': 'center',
            });
        },
    });
})

function OpenModal() {
    $('#DetailTrackingStatus').modal('show');
}