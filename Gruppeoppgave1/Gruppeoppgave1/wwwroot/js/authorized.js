$(function () {
    $.get("bruker/hentAlleStasjonerAdmin", function () {
        
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html'; 
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
});