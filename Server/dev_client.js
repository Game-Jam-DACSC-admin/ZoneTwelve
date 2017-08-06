var io = require('socket.io-client');
var socket = io.connect('ws://localhost:3000', {reconnect: true});
var t = 0;

socket.on('connect', function(data){
  console.log('Connected!');
});

socket.on("world", function(data){
  console.log("World: ",data);
})

socket.on("group", function(data){
  console.log("Group: ",data);
})
setInterval(function(){
  t+=0.01;
  var x = 4*Math.sin(t);
  var y = 4*Math.cos(t);
  socket.emit('world', {
    move:"true", 
    x:x,
    y:y,
    msg:`x:${Math.floor(x)} y:${Math.floor(y)}`
  });
},100);

// setTimeout(function(){
  // socket.disconnect();
// }, 3000)