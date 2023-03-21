"use strict";

this.loginToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE2YjcxNDRiLWUwYmMtNGZhZi05ZTFmLTYyNTI1NGI4ZTYyYiIsIlVzZXJuYW1lIjoiVGVzdFVzZXIiLCJzdWIiOiIiLCJpYXQiOjE2NzkzNjY0MDMsImV4cCI6MTY3OTU4MjQwMywiaXNzIjoibG9jYWxob3N0In0.LN_19tQ5j6oYBbXeJatsL5P9rsINNIPW1d8lRWMjm34"

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

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    //var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});