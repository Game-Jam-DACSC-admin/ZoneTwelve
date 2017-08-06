// using System;
// using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class index : MonoBehaviour {
  
  public GameObject[] players = new GameObject[10];
  // List<GameObject> mList = new List<GameObject>();

  public Text Message;
  public GameObject Clone;
  public Dictionary<string, string> Data = new Dictionary<string, string>();

  private SocketIOComponent socket;

	void Start(){
		socket = GameObject.FindObjectOfType<SocketIOComponent>().GetComponent<SocketIOComponent>();
    socket.On("world", socketWorld);
    socket.On("group", socketGroup);
    socket.On("error", socketError);
    socket.On("close", socketClose);
    // players = GameObject.FindGameObjectWithTag("Player");
    Debug.Log(this.transform.position);
	}

	void Update(){
    playerMove();
	}

  public void playerMove(){
    if(Input.GetKey(KeyCode.LeftArrow))
      moveTo(-0.1f, 0);
    if(Input.GetKey(KeyCode.RightArrow))
      moveTo(0.1f, 0);
    if(Input.GetKey(KeyCode.UpArrow))
      moveTo(0, 0.1f);
    if(Input.GetKey(KeyCode.DownArrow))
      moveTo(0, -0.1f);
  }

  public void moveTo(float x, float y){
    Vector3 position = this.transform.position;
    position.x+=x;
    position.y+=y;
    this.transform.position = position;
    Data["x"] = position.x.ToString();
    Data["y"] = position.y.ToString();
    Data["move"] = "true";
    // dynamic obj = new ExpandoObject();
    socket.Emit("world", new JSONObject(Data));
  }

  public void socketWorld(SocketIOEvent data){
    // Debug.Log(data.data["move"]==true);
    if(data.data["move"]){
      Vector3 position = GameObject.Find(data.data["uid"].ToString()).transform.position;
      float x = float.Parse(data.data["x"].ToString());
      float y = float.Parse(data.data["y"].ToString());
      position.x = x;
      position.y = y;
      GameObject.Find(data.data["uid"].ToString()).transform.position = position;
      // Debug.Log(position);
    }
    if(data.data["msg"]){
      Message.text = data.data["msg"].ToString();
    }
  }

  public void socketGroup(SocketIOEvent data){
    // Debug.Log(data.data["type"]==true);
    if(data.data["join"]){
      Debug.Log("["+data.data["uid"]+"] join");
      GameObject obj = Instantiate(this.Clone);
      obj.name = data.data["uid"].ToString();
    }else if(data.data["leave"]){
      GameObject.Destroy(GameObject.Find(data.data["uid"].ToString()), 1.0f);
    }
  }

	public void socketError(SocketIOEvent e){
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}

	public void socketClose(SocketIOEvent e){
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}
}
