"use strict";

this.loginToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE2YjcxNDRiLWUwYmMtNGZhZi05ZTFmLTYyNTI1NGI4ZTYyYiIsIlVzZXJuYW1lIjoiVGVzdFVzZXIiLCJzdWIiOiIiLCJpYXQiOjE2ODAxNzk1MzEsImV4cCI6MTY4MDM5NTUzMSwiaXNzIjoibG9jYWxob3N0In0.HNorKZ1adULRe7JLCtpYlraU_LvfSs9diL4Zf66CUhg"

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
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
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

document.getElementById("joinRoomButton").addEventListener("click", function (event) {
    var roomName = document.getElementById("joinRoomInput").value;
    connection.invoke("JoinRoom", roomName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("leaveRoomButton").addEventListener("click", function (event) {
    var roomName = document.getElementById("joinRoomInput").value;
    connection.invoke("LeaveRoom", roomName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("getActiveRooms").addEventListener("click", function (event) {
    connection.invoke("GetActiveChatRooms").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("activeRoomsMessage", function (message) {
    const list = document.getElementById('activeRooms');
    list.innerHTML = '';
    message.forEach(item => {
        const li = document.createElement('li');
        li.innerText = item;
        list.appendChild(li);
    });
});
