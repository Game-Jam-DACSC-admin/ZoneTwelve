var group = [];

module.exports = function(socket){
  join(socket, group);
  group.push(socket);

  socket.on("world", function(data){
    data.uid = socket.id;
    var index = group.indexOf(socket);
    group.map(function(usr, i){
      if(index!=i)
        usr.emit("world", data);
    })
  });

  socket.on('disconnect', function(){
    var index = group.indexOf(socket);
    if(index>-1)
      group.splice(index, 1);
  });
}

function join(socket, group){
  // var index = group.indexOf(socket);
  group.map(function(usr){
    socket.emit("group", {msg:"hello new user", uid:usr.id});
    usr.emit("group", {msg:"new user join", uid:socket.id});
  })
}