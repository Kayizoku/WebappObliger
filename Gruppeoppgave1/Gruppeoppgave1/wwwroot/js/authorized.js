$(function () {
    $.get("bruker/hentAlleStasjonerAdmin", function () {
        
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
});