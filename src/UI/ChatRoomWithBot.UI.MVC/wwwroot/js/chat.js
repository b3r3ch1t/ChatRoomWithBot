

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatroom").build();

//Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} --> ${new Date().toLocaleString()} says ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    console.log("ok !"); 


}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("messageInput").value;
    var roomId = document.getElementById("roomId").value

    var data = `{"Message":"${message}", "RoomId":"${roomId}"}`; 
 

    $.ajax({
        url: '/api/ChatRoom/SendMessage',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            document.getElementById("messageInput").value = '';
        },
        data:  data 
    });

});