var group = [];

module.exports = function(socket){
  join(socket, group);
  group.push(socket);

  socket.on("world", function(data){
    data.uid = socket.id;

    //格式化個變數值的型態
    data = dataFormat(data);

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
    leave(socket.id, group);
  });
}

function join(socket, group){
  // var index = group.indexOf(socket);
  var uid = socket.id;
  group.map(function(usr){
    socket.emit("group", {uid:usr.id, join:""});
    usr.emit("group", {uid:uid, join:""});
  });
}

function leave(uid, group){
  group.map(function(usr){
    usr.emit("group", {uid:uid, leave:""});
  });
}

function dataFormat(data){
  if(data.x)
    data.x = Number(data.x);
  if(data.y)
    data.y = Number(data.y);
  return data;
}