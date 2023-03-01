"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true;

//connection.on("ReceiveMessage", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);
//    // We can assign user-supplied strings to an element's textContent because it
//    // is not interpreted as markup. If you're assigning in any other way, you 
//    // should be aware of possible script injection concerns.
//    li.textContent = `${user} says ${message}`;
//});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

document.getElementById("addGroupBtn").addEventListener("click", function (event) {
    var user = document.getElementById("name").value;
    var group = document.getElementById("group").value;
    connection.invoke("AddGroup", group, user).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("RecAddGroupMsg", function (message) {
    var msg = message;
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("msgDiv").appendChild(li);
});

// 群組訊息Button事件
document.getElementById("submitGroupBtn").addEventListener("click", function (e) {
    var user = document.getElementById("name").value;
    var group = document.getElementById("group").value;
    var message = document.getElementById("msg").value;

    connection.invoke("SendMessageToGroup", group, user, message).catch(function (err) {
        return console.error(err.toString());
    });
});

// 群組訊息接收事件
connection.on("ReceiveGroupMessage", function (groupName, user, message) {
    var msg = `[群組訊息(${groupName})]${user}：${msg}`;
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("msgDiv").appendChild(li);
});

// 全頻道訊息訊息事件
connection.on("ReceiveMessage", function (user, message) {
    var msg = `[全頻道訊息(${groupName})]${user}：${msg}`;
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("msgDiv").appendChild(li);
});