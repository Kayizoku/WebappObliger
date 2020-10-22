$(function () {

    const id = window.location.search.substring(1);
    const url = "bestillinger/hentEnBestilling?" + id;
    $.get(url, function (bestilling) {
        $("#id").val(bestilling.id); // må ha med id inn skjemaet, hidden i html
        $("#pris").val(bestilling.pris);
        $("#fra").val(bestilling.fra);
        $("#til").val(bestilling.til);
        $("#dato").val(bestilling.dato);
        $("#tid").val(bestilling.tid);
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
});

function endreBestilling() {
    const bestilling = {
        id: $("#id").val(),
        pris: $("#pris").val(),
        fra: $("#fra").val(),
        til: $("#til").val(),
        dato: $("#dato").val(),
        tid: $("#tid").val()
    };
    $.post("bestillinger/endreEnBestilling", bestilling, function (OK) {
        if (OK) {
            window.location.href = 'admin.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}