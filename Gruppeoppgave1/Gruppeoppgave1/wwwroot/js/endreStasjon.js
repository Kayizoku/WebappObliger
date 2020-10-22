$(function () {

    const id = window.location.search.substring(1);
    const url = "stasjoner/hentEnStasjon?" + id;
    $.get(url, function (stasjon) {
        $("#id").val(stasjon.id); // må ha med id inn skjemaet, hidden i html
        $("#nummerPaaStopp").val(stasjon.nummerPaaStopp);
        $("#stasjonsnavn").val(stasjon.stasjonsNavn);
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
    });
}