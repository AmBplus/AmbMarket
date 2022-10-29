
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/OnlineVisitor")
    .build();
connection.start();