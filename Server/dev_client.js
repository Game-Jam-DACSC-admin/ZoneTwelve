var io = require('socket.io-client');
var socket = io.connect('ws://localhost:3000', {reconnect: true});

socket.on('connect', function(socket){
  console.log('Connected!');
});
socket.emit('world', {msg:"hello"});