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
    });
}