$(function () {

    const id = window.location.search.substring(1);
    const url = "avganger/hentEnAvgang?" + id;
    $.get(url, function (avgang) {
        $("#id").val(avgang.id); // må ha med id inn skjemaet, hidden i html
        $("#FraFelt").val(avgang.fra);
        $("#TilFelt").val(avgang.til);
        $("#TidFelt").val(avgang.tid);
    });
});

function endreAvgang() {
    const avgang = {
        id: $("#id").val(),
        fra: $("#FraFelt").val(),
        til: $("#TilFelt").val(),
        tid: $("#TidFelt").val()
    };
    $.post("avganger/endreAvgang", avgang, function (OK) {
        if (OK) {
            window.location.href = 'admin.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}