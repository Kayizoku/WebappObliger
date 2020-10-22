function lagreStasjon() {
    const stasjon = {
        nummerPaaStopp: $("#nummerPaaStopp").val(),
        stasjonsNavn: $("#stasjonsnavn").val()
    }
    const url = "stasjoner/lagreStasjon";
    $.post(url, stasjon, function (OK) {
        if (OK) {
            window.location.href = 'admin.html';
        }
        else {
            console.log("eroinberon");
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
};