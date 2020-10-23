function lagreAvgang() {
    const avgang = {
        fra: $("#FraFelt").val(),
        til: $("#TilFelt").val(),
        tid: $("#TidFelt").val()
    }
    const url = "avganger/leggTilAvgang";
    $.post(url, avgang, function (OK) {
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
};