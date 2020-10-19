$(function () {
    hentAlleStasjoner();
});

function hentAlleStasjoner() {
    $.get("stasjon/hentAlleStasjoner", function (stasjoner) {
        formaterStasjoner(stasjoner);
    });

    function formaterStasjoner(stasjoner) {
        let ut = "<table class='table table-striped'>" +
            "<tr>" +
            "<th>Nummer på stopp</th><th>Stasjonsnavn</th><th></th><th></th>" +
            "</tr>";
        for (let stasjon of stasjoner) {
            ut += "<tr>" +
                "<td>" + stasjon.nummerPaaStopp + "</td>" +
                "<td>" + stasjon.stasjonsNavn + "</td>" +
                "<a class='btn btn-primary' href='endreStasjon.html?id="+stasjon.id+"'>Endre</a></td > "+
                "<td> <button class='btn btn-danger' onclick='fjernStasjon()" + stasjon.id + ")'>Slett</button></td>" +
                "</tr>";
        }
        ut += "</table>";
        $("#stasjonene").html(ut);
    }
    }

    $(function () {
        hentAlleBestillinger();
    });

    function hentAlleBestillinger() {
        $.get("bestilling/hentAlleBestillinger", function (bestillinger) {
            formaterBestillinger(bestillinger);
        });

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
                    "<a class='btn btn-primary' href='endreBestilling.html?id=" + bestilling.id + "'>Endre</a></td >" +
                    "<td> <button class='btn btn-danger' onclick='slettEnBestilling()" + bestilling.id + ")'>Slett</button></td>" +
                    "</tr>";
            }
            ut += "</table>";
            $("#bestillingene").html(ut);
        }
    }

        $(function () {
            hentAlleAvganger();
        });

        function hentAlleAvganger() {
            $.get("avgang/hentAlleAvganger", function (avganger) {
                formaterAvganger(avganger);
            });

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
                        "<a class='btn btn-primary' href='endreAvgang.html?id=" + avgang.id + "'>Endre</a></td > " +
                        "<td> <button class='btn btn-danger' onclick='slettAvgang()" + avgang.id + ")'>Slett</button></td>" +
                        "</tr>";
                }
                ut += "</table>";
                $("#avgangene").html(ut);
            }

        }