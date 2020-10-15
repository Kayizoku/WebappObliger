$(function () {
    hentAlleBestillinger();
});

function hentAlleBestillinger() {
    $.get("bestillinger/hentAlleBestillinger", function (data) {
        formaterBestillinger(data);
    });
}

function formaterBestillinger(bestillinger) {
    let ut = "<table class='table table-striped'><tr><th>Fra</th><th>Til</th><th>Dato</th><th>Tid</th><th>Pris</th></tr>";

    bestillinger.forEach(bestilling => {
        ut += "<tr><td>" + bestilling.fra + "</td><td>" + bestilling.til + "</td><td>" +
            bestilling.dato + "</td><td>" + bestilling.tid + "</td><td>" + bestilling.pris + "</td></tr>";

    });

    ut += "</table>";
    $("#visAlleBestillinger").html(ut);
}