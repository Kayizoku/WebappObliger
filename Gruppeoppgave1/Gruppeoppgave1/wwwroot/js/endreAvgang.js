$(function () {

    const id = window.location.search.substring(1);
    const url = "avganger/hentEnAvgang?" + id;
    $.get(url, function (avgang) {
        $("#id").val(avgang.id); 
        $("#FraFelt").val(avgang.fra);
        $("#TilFelt").val(avgang.til);
        $("#TidFelt").val(avgang.tid);
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
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
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'login.html';  
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
}