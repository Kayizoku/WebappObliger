function lagreStasjon() {
    const stasjon = {
        nummerPaaStopp: $("#nummerPaaStopp").val(),
        stasjonsNavn: parseInt($("#stasjonsNavn").val())
    }
    const url = "stasjoner/lagreStasjon";
    $.post(url, stasjon, function (OK) {
        if (OK) {
            window.location.href = 'admin.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
};