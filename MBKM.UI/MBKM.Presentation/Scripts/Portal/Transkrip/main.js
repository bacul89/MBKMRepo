Date.prototype.toShortFormat = function () {

    let monthNames = ["Januari", "Februari", "Maret", "April",
        "Mai", "Juni", "Juli", "Augustus",
        "September", "Oktober", "November", "Desember"];

    let day = this.getDate();

    let monthIndex = this.getMonth();
    let monthName = monthNames[monthIndex];

    let year = this.getFullYear();

    return `${day} ${monthName} ${year}`;
}


/* --responsive */
$(document).ready(function () {

    var date = new Date();
    console.log(date.toShortFormat());


    isZooming();
});

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