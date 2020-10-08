//variabler som brukes til å regne ut pris
var pris = 0;
var antallStasjoner = 0;

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
    let ut = "<table><tr><th>Fra</th><th>Til</th><th>Dato><th>Avgang</th></tr>";

    for (const bestilling in bestillinger) {
        ut += "<tr><td>" + bestilling.Fra + "</td><td>" + bestilling.Til + "</td><td>" +
            bestilling.Dato + "</td><td>" + bestilling.Avgang + "</td></tr>";
    }

    ut += "</table>";
    $("#visAlleBestillinger").html(ut);
}



function lagre() {
    if (validerFelt() != 0) {
        console.log("Feil i bestillingskjema");
        return;
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
        console.log("Du må velge ulike FRA- og TIL-stasjoner!" + fra + "," + til);
        event.preventDefault();
    }
    else if (fra === "") {
        feil++;
        $("#feilmelding").innerHTML = "Feil i FRA-boksen" + "\nSett inn gyldig verdi for FRA\n";
        console.log("Feil i FRA-boksen" + "\nSett inn gyldig verdi for FRA\n");
        event.preventDefault();
    }
    else if (til === "") {
        feil++;
        $("#feilmelding").innerHTML= "Feil i TIL-boksen" + "\nSett inn gyldig verdi for TIL\n";
        console.log("Feil i TIL-boksen" + "\nSett inn gyldig verdi for TIL\n");
        event.preventDefault();
    }
    else if (dato === "") {
        feil++;
        $("#feilmelding").innerHTML = "Dato er ikke valgt \nVelg Dato\n";
        console.log("Dato er ikke valgt \nVelg Dato\n");
        event.preventDefault();
    }
    else if (dato.split("-")[2] !== "2020") {
        feil++;
        $("#feilmelding").innerHTML = "Vi kan kun tilby turer ut året foreløpig";
        console.log("Vi kan kun tilby turer ut året foreløpig");
    }
    return feil;
}

/*
function prisKalk() {
    var
}
*/

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

    const fraFelt = $("#FraFelt")[0];
    

    stasjoner.forEach(stasjon => {
        console.log(stasjon.stasjonsNavn);

        const option = document.createElement("option");
        option.value = stasjon.stasjonsNavn;
        option.innerHTML = stasjon.stasjonsNavn;

        fraFelt.appendChild(option);
        
    });
}

function visDropDownTil(stasjoner) {
    const tilFelt = $("#TilFelt")[0];

    stasjoner.forEach(stasjon => {
        console.log(stasjon.stasjonsNavn);

        const option = document.createElement("option");
        option.value = stasjon.stasjonsNavn;
        option.innerHTML = stasjon.stasjonsNavn;

        
        tilFelt.appendChild(option);
    });
}


