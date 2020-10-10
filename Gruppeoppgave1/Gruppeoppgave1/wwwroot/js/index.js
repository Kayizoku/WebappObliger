//variabler som brukes til å regne ut pris
var pris = 0;
var antallStasjoner = 0;
var fraStasjon;
var tilStasjon;

$(function () {
    hentAlleBestillinger();
    visStasjonerAuto();
});



function lagreBestilling(bestilling) {
    $.post("bestillinger/lagreBestilling", bestilling, function () {
        alert("Bestillingen er lagret");
    });
}


//henter alle bestillinger i et array
function hentAlleBestillinger() {
    $.get("bestillinger/hentAlleBestillinger", function (data) {
        formaterBestillinger(data);
    });
}

function formaterBestillinger(bestillinger) {
    let ut = "<table><tr><th>Fra</th><th>Til</th><th>Dato</th><th>Tid</th><th>Pris</th></tr>";

    bestillinger.forEach(bestilling => {
        ut += "<tr><td>" + bestilling.fra + "</td><td>" + bestilling.til + "</td><td>" +
            bestilling.dato + "</td><td>" + bestilling.tid + "</td><td>" + bestilling.pris + "</td></tr>";

    });

    ut += "</table>";
    $("#visAlleBestillinger").html(ut);
}

function prisKalk(frastasjon, tilstasjon) {

    var prisLokal = 0;

    if (frastasjon === "Oslo") {
        fraStasjon = 1;
    }

    else if (frastasjon === "Drammen") {
        fraStasjon = 2;
    }

    else if (frastasjon === "Horten") {
        fraStasjon = 3;
    }

    if (tilstasjon === "Oslo") {
        tilStasjon = 1;
    }

    else if (tilstasjon === "Drammen") {
        tilStasjon = 2;
    }

    else if (tilstasjon === "Horten") {
        tilStasjon = 3;
    }

    prisLokal = Math.abs((tilStasjon - fraStasjon) * 50);

    return prisLokal;

}


function lagre() {
    if (validerFelt() != 0) {
        alert("Feil i bestillingskjema");
        return;
    }

    pris = prisKalk($("#FraFelt").val(), $("#TilFelt").val());


    const bestilling = {
        Fra: $("#FraFelt").val(),
        Til: $("#TilFelt").val(),
        Dato: $("#dato").val(),
        Pris: pris,
        Tid: $("#TidFelt").val()
    };

    lagreBestilling(bestilling);
    hentAlleBestillinger();
    resetInput();
    location.reload();
}

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
        $("#feilmelding").innerHTML = "Du må velge ulike FRA- og TIL-stasjoner!";
        event.preventDefault();
    }
    else if (fra === "") {
        feil++;
        $("#feilmelding").innerHTML = "Feil i FRA-boksen" + "\nSett inn gyldig verdi for FRA\n";
        event.preventDefault();
    }
    else if (til === "") {
        feil++;
        $("#feilmelding").innerHTML = "Feil i TIL-boksen" + "\nSett inn gyldig verdi for TIL\n";
        event.preventDefault();
    }
    else if (dato === "") {
        feil++;
        $("#feilmelding").innerHTML = "Dato er ikke valgt \nVelg Dato\n";
        event.preventDefault();
    }
    /*else if (dato.split(".")[2] !== "2020") {
        feil++;
        $("#feilmelding").innerHTML = "Vi kan kun tilby turer ut året foreløpig";
    }*/
    return feil;
}

function visStasjonerAuto() {
    $.get("stasjoner/hentAlleStasjoner", function (data) {
        visDropDownFra(data);
        visDropDownTil(data);
    });
}


function velgAvganger(fra, til, dato) {
    $.get("avganger/hentAlleAvganger", fra, til, dato, function (data) {
        formaterAvganger(data);
    });
}

function formaterAvganger(avgangsliste) {

}

function visDropDownFra(stasjoner) {

    //Henter ut hvert stasjonsnavn fra databasen
    stasjonerList = [];
    stasjoner.forEach(s => {
        stasjonerList.push(s.stasjonsNavn);
    })

    const fraFelt = $("#FraFelt");
    fraFelt.autocomplete({
        source: stasjonerList,
        onSelect: function (suggestion) {
            alert(suggestion.value);
        }
    })

}

function visDropDownTil(stasjoner) {
    stasjonerList = [];
    stasjoner.forEach(s => {
        stasjonerList.push(s.stasjonsNavn);
    })

    const tilFelt = $("#TilFelt");
    tilFelt.autocomplete({
        source: stasjonerList,
        onSelect: function (suggestion) {
            alert(suggestion.value);
        }
    })
}


