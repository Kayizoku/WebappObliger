﻿/*function loggInn() {
    const bruker = {
        brukernavn: $("#brukernavn").val(),
        passord: $("#passord").val()
    }
    $.post("Bestilling/LoggInn", bruker, function (OK) {
        if (OK) {
            window.location.href = 'index.html'; //Må endres til admin sin html

        }
        else {
            $("#feil").html("Feil brukernavn eller passord");
        }
    })
        .fail(function (feil) {
            $("#feil").html("Feil på server. Prøv igjen senere." + feil.responseText);
        });
}*/



function loggInn() {

    const brukernavnOK = validerBrukernavn($("#brukernavn").val());
    const passordOK = validerPassord($("#passord").val());

    if (brukernavnOK && passordOK) {
        const bruker = {
            brukernavn: $("#brukernavn").val(),
            passord: $("#passord").val()
        }
        $.post("Bestilling/LoggInn", bruker, function (OK) {
            if (OK) {
                window.location.href = 'index.html';
            }
            else {
                $("#feil").html("Feil brukernavn eller passord");
            }
        })
            .fail(function (feil) {
                $("#feil").html("Feil på server - prøv igjen senere: " + feil.responseText);
            });
    }
}


