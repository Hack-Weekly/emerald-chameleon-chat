"use strict";

this.loginToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE2YjcxNDRiLWUwYmMtNGZhZi05ZTFmLTYyNTI1NGI4ZTYyYiIsIlVzZXJuYW1lIjoiVGVzdFVzZXIiLCJzdWIiOiIiLCJpYXQiOjE2Nzk0Mzg4NDgsImV4cCI6MTY3OTY1NDg0OCwiaXNzIjoibG9jYWxob3N0In0.bTLP2GnFP3gTsbpE3OcH-OcRMnNsbZ0at1CKetRcufk"

//pass login token to /chatHub to identify logged in user
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub", { accessTokenFactory: () => this.loginToken })
    .build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function ( user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});

connection.start()
    .then(function () {
        document.getElementById("sendButton").disabled = false;
    })
    .catch(function (err) {
    return console.error(err.toString());
    });

document.getElementById("sendButton").addEventListener("click", function (event) {
    var roomName = document.getElementById("roomInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage",roomName, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("newRoomButton").addEventListener("click", function (event) {
    var newRoomName = document.getElementById("newRoomInput").value;
    var newRoomDescription = "This is a test description";
    connection.invoke("CreateChatroom", newRoomName, newRoomDescription).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});