$(function () {
    hentAlleStasjoner();
});

function hentAlleStasjoner() {
    $.get("bruker/hentAlleStasjonerAdmin", function (stasjoner) {
        formaterStasjoner(stasjoner);
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function formaterStasjoner(stasjoner) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Nummer på stopp</th><th>Stasjonsnavn</th><th></th><th></th>" +
        "</tr>";
    for (let stasjon of stasjoner) {
        ut += "<tr>" +
            "<td>" + stasjon.nummerPaaStopp + "</td>" +
            "<td>" + stasjon.stasjonsNavn + "</td>" +
            "<td>" + "<a class='btn btn-primary' href = 'endreStasjon.html?id="+stasjon.id+"'>Endre</a></td>" +
            "<td><button class='btn btn-danger' onclick='fjernStasjon(" + stasjon.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#stasjonene").html(ut);
}


$(function () {
    hentAlleBestillinger();
});

function hentAlleBestillinger() {
    $.get("bruker/hentAlleBestillingerAdmin", function (bestillinger) {
        formaterBestillinger(bestillinger);
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function formaterBestillinger(bestillinger) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Pris</th><th>Fra</th><th>Til</th><th>Dato</th><th>Tid</th><th></th><th></th>" +
        "</tr>";
    for (let bestilling of bestillinger) {
        ut += "<tr>" +
            "<td>" + bestilling.pris + "</td>" +
            "<td>" + bestilling.fra + "</td>" +
            "<td>" + bestilling.til + "</td>" +
            "<td>" + bestilling.dato + "</td>" +
            "<td>" + bestilling.tid + "</td>" +
            "<td><a class='btn btn-primary' href='endreBestilling.html?id=" + bestilling.id + "'>Endre</a></td >" +
            "<td> <button class='btn btn-danger' onclick='slettEnBestilling(" + bestilling.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#bestillingene").html(ut);
}


$(function () {
    hentAlleAvganger();
});

function hentAlleAvganger() {
    $.get("bruker/hentAlleAvgangerAdmin", function (avganger) {
        formaterAvganger(avganger);
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function formaterAvganger(avganger) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Fra</th><th>Til</th><th>Tid</th><th></th><th></th>" +
        "</tr>";
    for (let avgang of avganger) {
        ut += "<tr>" +
            "<td>" + avgang.fra + "</td>" +
            "<td>" + avgang.til + "</td>" +
            "<td>" + avgang.tid + "</td>" +
            "<td><a class='btn btn-primary' href='endreAvgang.html?id=" + avgang.id + "'>Endre</a></td> " +
            "<td><button class='btn btn-danger' onclick='slettAvgang(" + avgang.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#avgangene").html(ut);
}

$(function () {
    hentAlleRuter();
});

function hentAlleRuter() {
    $.get("bruker/hentAlleRuterAdmin", function (ruter) {
        formaterRuter(ruter);
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function formaterRuter(ruter) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Navn</th><th>Stasjoner på rute</th><th></th><th></th>" +
        "</tr>";
    for (let rute of ruter) {
        ut += "<tr>" +
            "<td>" + rute.navn + "</td>" +
            "<td>" + bestilling.stasjonerPaaRute + "</td>" +
            "<td><a class='btn btn-primary' href='endreRute.html?id=" + rute.id + "'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettRute(" + rute.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#rutene").html(ut);
}


function fjernStasjon(id) {
    var url = "stasjoner/fjernStasjon?id=" + id;
    $.post(url, id, function (data) {
        document.location.href = "admin.html";
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function slettEnBestilling(id) {
    var url = "bestillinger/slettEnBestilling?id=" + id;
    $.post(url, id, function (data) {
        document.location.href = "admin.html";
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function slettAvgang(id) {
    var url = "avganger/slettAvgang?id=" + id;
    $.post(url, id, function (data) {
        document.location.href = "admin.html";
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}

function slettRute(id) {
    var url = "rute/slettRute?id=" + id;
    $.post(url, id, function (data) {
        document.location.href = "admin.html";
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}