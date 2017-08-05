const _port = 3000;
var user = [];
var io = require('socket.io')({transports: ['websocket']}).attach(_port);

console.log("Server runing in port "+_port);

io.on('connection', function (socket) {
  join();
  socket.emit("boop", {uid:socket.id, msg:"Welcome"});
  user.push(socket);

  socket.on('beep', function(data){
    data.uid = socket.id;
    console.log(data);
    var index = user.indexOf(socket);
    user.map(function(usr, i){
      if(i!=index)
        usr.emit('boop', data);
    })
  });

  socket.on('disconnect', function(data){
    var index = user.indexOf(socket);
    if(index>-1)
      user.splice(index, 1);
    console.log('User: '+user.length);
  });
  
  function join(){
    user.map(function(usr){
      console.log(usr.id, socket.id);
      socket.emit("user", {msg:"hello new user", uid:usr.id});
      usr.emit("user", {msg:"new user join", uid:socket.id});
    })
  }

});