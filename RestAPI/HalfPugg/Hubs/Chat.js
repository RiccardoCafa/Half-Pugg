<script src="~/Scripts/jquery.signalR-2.2.2.min.js"></script>
<script src="~/Scripts/jquery-3.4.1.min.js"></script>
<script src="~/signalr/hubs"></script>

$(function () {
    var chat = $.connection.chatHub;
    chat.client.SendMessage = function (name, menssage) {

        var containerNome = $('<span/>').text(name).html();
        var containerMensagem = $('<div/>').text(menssage).html();

        $("#conversa").append(
            '<li><strong>'
            + containerNome +
            '</strong>: '
            + containerMensagem + '</li>');
    };

    $.connection.hub.start().done(function () {
        $("#enviar").click(function () {
            var nome = $("#nome").val();
            var mensagem = $("#mensagem").val();
            chat.server.Send(nome, mensagem);
            $("#mensagem").val('');
        });
    });
});