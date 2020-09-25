//velg FRA og TIL på DATO, returner liste med tilgjengelige AVGANGER, velg der så lagre bestilling

$(function () {
    $.get("/hentAlleStasjoner", function (data) {
        formaterStasjoner(data);
    });
});

function formaterStasjoner(stasjonsarray) {

}


function lagreBestilling() {
    $.post("/lagreBestilling", bestilling, function (){

    });
}

$("#FraFelt").click(function (){
    visStasjoner();
});

$("#TilFelt").click(function () {
    visStasjoner();
});

$("#lagreKnapp").click(function () {
    if (validerFelt() != 0) {
        alert("Feil i bestillingskjema");
    });

function validerFelt() {
    let feil = 0;
    var fra = $("#FraFelt").val();
    var til = $("#TilFelt").val();
    var dato = $("#dato").val();
    
    if (fra === "") {
        feil++;
        $("#feilmelding").html("Feil i FRA-boksen" + "\nSett inn gyldig verdi for FRA\n");
        
    }
    else if (til === "") {
        feil++;
        $("#feilmelding").html("Feil i TIL-boksen" + "\nSett inn gyldig verdi for TIL\n");
        
    }
    else if (dato === "") {
        feil++;
        $("#feilmelding").html("Dato er ikke valgt" + "\nVelg Dato\n");
        
    } else if (dato.split("-")[2] !== "2020") {
        feil++;
        $("#feilmelding").html("Vi kan kun tilby turer ut året foreløpig");
    }
    return feil;
}


     
function formaterData(beestilling){
        let ut ="<table class='table table-striped'><tr><th>Fra</th><th>Til</th><th>Dato</th>" +
            "<th>Tid</th><th>Pris</th>";

    ut += "<tr>";
    ut += "<td>" + beestilling.fra + "</td><td>" +
};