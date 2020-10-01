//variabler som brukes til å regne ut pris
var pris = 0;
var antallStasjoner = 0;

$(function () {
    hentAlleBestillinger();
});



function lagreBestilling(bestilling) {
    $.post("bestilling/lagre", bestilling, function () {
        alert("Bestillingen er lagret");
    });
}


//henter alle bestillinger i et array
function hentAlleBestillinger() {
    $.get("bestilling/hentAlle", function (data) {
        formaterBestillinger(data);
    });
}

function formaterBestillinger(bestillinger) {
    let ut = "<table><tr><th>Fra</th><th>Til</th><th>Dato><th>Avgang</th></tr>";

    for (const bestilling in bestillinger) {
        ut += "<tr><td>" + bestilling.Fra + "</td><td>" + bestilling.Til + "</td><td>" +
            bestilling.Dato + "</td><td>" + bestilling.Avgang + "</td></tr>";
    }

    ut += "</table>";
    $("#visAlleBestillinger").innerHTML = ut;
}

$("#FraFelt").click(function () {
    visStasjonerAuto();
});

$("#TilFelt").click(function () {
    visStasjonerAuto();
});

$("#lagreKnapp").click(function ( {
    if (validerFelt() != 0) {
    console.log("Feil i bestillingskjema");
}
    
    const bestilling = {
        Fra: $("#FraFelt").val(),
        Til: $("#TilFelt").val(),
        Dato: $("#dato").val(),
        Avgang: $("avgangValgt").val(),
        Pris: pris
};

lagreBestilling(bestilling);
hentAlleBestillinger();
resetInput();
});

function resetInput() {
    $("#FraFelt").val("");
    $("#TilFelt").val("");
    $("#dato").val("");
    $("avgangValgt").val("");

}

//generelt inputvalidering metode
function validerFelt() {
    let feil = 0;
    var fra = $("#FraFelt").val();
    var til = $("#TilFelt").val();
    var dato = $("#dato").val();

    if (fra === til) {
        feil++;
        $("#feilmelding").html("Du må velge ulike FRA- og TIL-stasjoner!");
        event.preventDefault();
    }
    else if (fra === "") {
        feil++;
        $("#feilmelding").html("Feil i FRA-boksen" + "\nSett inn gyldig verdi for FRA\n");
        event.preventDefault();
    }
    else if (til === "") {
        feil++;
        $("#feilmelding").html("Feil i TIL-boksen" + "\nSett inn gyldig verdi for TIL\n");
        event.preventDefault();
    }
    else if (dato === "") {
        feil++;
        $("#feilmelding").html("Dato er ikke valgt" + "\nVelg Dato\n");
        event.preventDefault();
    }
    else if (dato.split("-")[2] !== "2020") {
        feil++;
        $("#feilmelding").html("Vi kan kun tilby turer ut året foreløpig");
    }
    return feil;
}

function prisKalk()

function visStasjonerAuto() {
    //skal bruke autocomplete plugin hvis lov
}

function velgAvganger(fra, til, dato) {
    $.get("api/bestilling/velgAvganger", fra, til, dato, function (data) {
        formaterAvganger(data);
    });
}

function formaterAvganger(avgangsliste) {

}

