function validerBrukernavn(brukernavn) {
    const regexp = /^[0-9a-zA-ZæøåÆØÅ.\-]{2,20}$/;
    const ok = regexp.test(brukernavn);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet må bestå av 2 til 20 tegn");
        return false;
    }
    else {
        $("#feilBrukernavn").html("");
        return true;
    }
}

function validerPassord(passord) {
    const regexp = /([0-9a-zA-ZæøåÆØÅ.\-]{8,20})/;
    const ok = regexp.test(passord);
    if (!ok) {
        $("#feilPassord").html("Passordet må bestå av 8 til 20 tegn, minst en bokstav og et tall");
        return false;
    }
    else {
        $("#feilPassord").html("");
        return true;
    }
}