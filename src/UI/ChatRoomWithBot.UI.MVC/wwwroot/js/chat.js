

"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatroom").build();

//Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {

    $('#messagesList').empty();

    var arr = $.parseJSON(message);
    console.log("ReceiveMessage arr ==>", arr);

    $.each(arr, function (index, value) {

       // var dateLocal = new Date(value.Date);

        var sDate = new Date(Date.parse(value.Date, "MM/dd/yyyy"));
        var content = value.UserName + '(' + sDate.toLocaleString() + ') --> ' + value.Message;

        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);

        li.textContent = content;


        // $('#messagesList').appendChild(li);

        console.log("ReceiveMessage content ==>", content);
    });

    //var li = document.createElement("li");
    //document.getElementById("messagesList").appendChild(li);
    //// We can assign user-supplied strings to an element's textContent because it
    //// is not interpreted as markup. If you're assigning in any other way, you 
    //// should be aware of possible script injection concerns.
    //li.textContent = `${user}(${new Date().toLocaleString()})--> ${message}`;

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    var roomId = $('#roomId').val();

    connection.invoke('JoinGroup', roomId);
    console.log("ok !");
    $('#messageError').hide();

    $('#messageError').val('');

}).catch(function (err) {
    return console.error(err.toString());
});


$("#sendButton").click(function (e) {
    event.preventDefault();
    var message = $("#messageInput").val();
    var roomId = $("#roomId").val()

    var data = `{"Message":"${message}", "RoomId":"${roomId}"}`;
    $('#messageError').hide();

    $('#messageError').val('');

    $.ajax({
        url: '/api/ChatRoom/SendMessage',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: data,
        success: function (data) {
            $('#messageInput').val('');
            console.log('data=>', data);

            $('#messageError').hide();

            $('#messageError').val('');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            $('#messageError').val('the message wasn`t send');
            $('#messageError').show();

            console.log('erro=>', textStatus);
        }
    });


})