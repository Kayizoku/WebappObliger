$(function () {

    const id = window.location.search.substring(1);
    const url = "stasjoner/hentEnStasjon?" + id;
    $.get(url, function (stasjon) {
        $("#id").val(stasjon.id); // må ha med id inn skjemaet, hidden i html
        $("#nummerPaaStopp").val(stasjon.nummerPaaStopp);
        $("#stasjonsnavn").val(stasjon.stasjonsNavn);
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
});

function endreStasjon() {
    const stasjon = {
        id: $("#id").val(), 
        nummerPaaStopp: $("#nummerPaaStopp").val(),
        stasjonsnavn: $("#stasjonsnavn").val()
    };
    $.post("stasjoner/endreStasjon", stasjon, function (OK) {
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