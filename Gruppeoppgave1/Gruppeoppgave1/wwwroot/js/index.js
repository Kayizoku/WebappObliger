//velg FRA og TIL på DATO, returner liste med tilgjengelige AVGANGER, velg der så lagre bestilling

$(function () {
    $.get("/hentAlleStasjoner", function (data) {
        formaterStasjoner(data);
    });
});

function formaterStasjoner(stasjonsarray) {
    //skal være autocomplete med plugin hvis det er lov
}


function lagreBestilling() {
    $.post("api/bestilling/lagre", bestilling, function (){

    });
}

function hentAlleBestillinger() {
    $.get("api/bestilling/hentAlle", function (data) {
        formaterBestillinger();
    });
}

$("#FraFelt").click(function (){
    visStasjonerAuto();
});

$("#TilFelt").click(function () {
    visStasjonerAuto();
});

$("#lagreKnapp").click(function () {
    if (validerFelt() != 0) {
        alert("Feil i bestillingskjema");
        //veldig usikker på denne, hvor skal lagre være?
    });

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


     
function formaterData(beestilling){
        let ut ="<table class='table table-striped'><tr><th>Fra</th><th>Til</th><th>Dato</th>" +
            "<th>Tid</th><th>Pris</th>";

    ut += "<tr>";
    ut += "<td>" + beestilling.fra + "</td><td>";
}

function visStasjonerAuto() {
    //skal bruke autocomplete plugin hvis lov
}

function velgAvganger(fra, til, dato) {
    $.get("api/bestilling/velgAvganger", fra, til, dato, function (data) {
        formaterAvganger(data);
    });
}

function formaterAvganger(avgangsliste) {
    //formaterer i html så det kan velges
}

function formaterBestillinger() {
    //formater i html og vis i visalle div
}