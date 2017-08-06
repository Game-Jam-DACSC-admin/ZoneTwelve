using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class index : MonoBehaviour {
  
  public GameObject[] players;
  
  public Text Message;
  public GameObject Clone;
  public Dictionary<string, string> Data = new Dictionary<string, string>();
  
  private SocketIOComponent socket;
  
	void Start(){
		socket = GameObject.FindObjectOfType<SocketIOComponent>().GetComponent<SocketIOComponent>();
    // socket.On("open", TestOpen);
    // socket.On("user", Groups);
    // socket.On("boop", TestBoop);
    socket.On("world", socketWorld);
    socket.On("group", socketGroup);
    socket.On("error", socketError);
    socket.On("close", socketClose);
    // players = GameObject.FindGameObjectWithTag("Player");
    Debug.Log(this.transform.position);
	}
  
	void Update(){
    if(Input.GetKey(KeyCode.LeftArrow)){
      move(-1, 0);
    }
    if(Input.GetKey(KeyCode.RightArrow)){
      move(1, 0);      
    }
    if(Input.GetKey(KeyCode.UpArrow)){
      move(0, 1);
    }
    if(Input.GetKey(KeyCode.DownArrow)){
      move(0, -1);
    }
    if(Input.GetKeyDown(KeyCode.A))
      Instantiate(this.Clone);  
	}
  public void move(int x, int y){
    Vector3 position = this.transform.position;
    position.x+=x;
    position.y+=y;
    this.transform.position = position;
    Data["x"] = position.x.ToString();
    Data["y"] = position.y.ToString();
    socket.Emit("world", new JSONObject(Data));
  }
  	
	public void TestBoop(SocketIOEvent e){
		Debug.Log(e.data);
    if(e.data["msg"]){
      Message.text = e.data["msg"].ToString();
    }
	}
	
  public void socketWorld(SocketIOEvent data){
    Debug.Log(data.data);
  }
  
  public void socketGroup(SocketIOEvent data){
    Debug.Log(data.data["uid"]);
  }
  
	public void socketError(SocketIOEvent e){
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void socketClose(SocketIOEvent e){
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}
  
}
