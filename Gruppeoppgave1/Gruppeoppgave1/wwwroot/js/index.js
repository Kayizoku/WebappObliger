//variabler som brukes til å regne ut pris
var pris = 0;
var antallStasjoner = 0;
var fraStasjon;
var tilStasjon;
var alleStasjoner = [];
var stasjonerList;

$(function () {
    //hentAlleBestillinger();
    visStasjonerAuto();
    visAvgangerAuto();
    assignSubmitFunction();
});


function assignSubmitFunction() {
    $("#bestill").on("submit", function (e) {
        e.preventDefault();

        var data = lagre(e);
        if (!data) return;

        $.ajax({
            type: "POST",
            url: "Bestilling/Lagre",
            data: data,

            success: function (data) {
                document.location = "BetalingLosning.html";
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Error, status = " + textStatus + ", " +
                    "error thrown: " + errorThrown.message
                );
            }
        });
    });
};

//Brukes ikke
function lagreBestilling(bestilling) {
    alert("Bestillingen er lagret");
    /*

    alert("pause");

    $.post("bestillinger/lagreBestilling", bestilling, function () {
        document.location = kvittering.js;
    }, "json");
    */

    $.ajax({
        type: "POST",
        url: "Bestilling/Lagre",
        data: bestilling,

        always: function (data) {
            alert("yessssss");
            //document.location = "kvittering.js";
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Error, status = " + textStatus + ", " +
                "error thrown: " + errorThrown
            );
        }
    });

}


//henter alle bestillinger i et array (Flyttet til kvittering.js)
/*
function hentAlleBestillinger() {
    $.get("bestillinger/hentAlleBestillinger", function (data) {
        formaterBestillinger(data);
    });
}

function formaterBestillinger(bestillinger) {
    let ut = "<table class='table table-striped'><tr><th>Fra</th><th>Til</th><th>Dato</th><th>Tid</th><th>Pris</th></tr>";

    bestillinger.forEach(bestilling => {
        ut += "<tr><td>" + bestilling.fra + "</td><td>" + bestilling.til + "</td><td>" +
            bestilling.dato + "</td><td>" + bestilling.tid + "</td><td>" + bestilling.pris + "</td></tr>";

    });

    ut += "</table>";
    $("#visAlleBestillinger").html(ut);
}

*/
//Gammel statisk priskalk
/*




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

    //return prisLokal;
    //Venter på ny priskalk()
    return 123;
}
*/

function prisCalc(frastasjon, tilstasjon) {
    var fraNr, tilNr;
    
    alleStasjoner.forEach(s => {
        if (frastasjon == s.stasjonsNavn) {
            fraNr = s.nummerPaaStopp;
        }
        if (tilstasjon == s.stasjonsNavn) {
            tilNr = s.nummerPaaStopp;
        }
    })

    var lokalpris = (Math.abs(fraNr - tilNr)) * 50;
    return lokalpris;
}


function lagre(event) {
    if (validerFelt(event) != 0) {
        $("#feilmelding").get(0).classList.remove("invisible");
        return false;
    }

    pris = prisCalc($("#FraFelt").val(), $("#TilFelt").val());


    const bestilling = {
        Fra: $("#FraFelt").val(),
        Til: $("#TilFelt").val(),
        Dato: $("#dato").val(),
        Pris: pris,
        Tid: $("#TidFelt").val()
    };
    console.log(bestilling)
    //lagreBestilling(bestilling);
    //hentAlleBestillinger();
    resetInput();
    return bestilling;
    //location.reload();
}

function resetInput() {
    $("#FraFelt").val("");
    $("#TilFelt").val("");
    $("#dato").val("");
    $("avgangValgt").val("");

}

//generelt inputvalidering metode
function validerFelt(event) {
    let feil = 0;
    var fra = $("#FraFelt").val();
    var til = $("#TilFelt").val();
    var dato = $("#dato").val();

    if (fra === "") {
            feil++;
            $("#feilmelding").html("Sett inn gyldig verdi for FRA");
            event.preventDefault();
    }
    else if (til === "") {
        feil++;
        $("#feilmelding").html("Sett inn gyldig verdi for TIL");
        event.preventDefault();
    }
    else if (!stasjonerList.includes(fra)) {
        feil++;
        $("#feilmelding").html("Denne stasjonen er ikke tilgjengelig: " + fra);
        event.preventDefault();
    }
    else if (!stasjonerList.includes(til)){
        feil++;
        $("#feilmelding").html("Denne stasjonen er ikke tilgjengelig: " + til);
        event.preventDefault();
    }
    else if (fra === til) {
        feil++;
        $("#feilmelding").html("Du må velge ulike FRA- og TIL-stasjoner");
        event.preventDefault();
    }
    else if (dato === "") {
        feil++;
        $("#feilmelding").html("Dato er ikke valgt");
        event.preventDefault();
    }
    else if (dato.split("-")[0] !== "2020") {
        feil++;
        $("#feilmelding").html("Vi kan kun tilby turer ut året foreløpig");
    }
    return feil;
}

function visStasjonerAuto() {
    $.get("stasjoner/hentAlleStasjoner", function (data) {
        visDropDownFra(data);
        visDropDownTil(data);
    });
}

function visAvgangerAuto() {
    $.get("avganger/hentAlleAvganger", function (data) {
        var $dropdown = $("#TidFelt");
        data.forEach(x => {
            $dropdown.append($('<option>').html(x.tid).attr({
                name: x.tid,
                id: x.tid
            }))
        })
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
        alleStasjoner.push(s);
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





/*
 * Skal være med i de funksjonene som trenger tilgang av admin
 * For når innlogging failer
 * .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html'; 
        }
        else {
            $("#feil").html("Feil på server. Prøv igjen om en liten stund");
        }
    });*/