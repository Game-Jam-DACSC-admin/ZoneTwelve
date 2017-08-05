using UnityEngine;

public class Create : MonoBehaviour{
  // 這是用來複製的 GameObject 物件, 
  // 該變數必須是 public, 這樣才有辦法從外面把物件拖曳近來. 
  public GameObject gobj;


  // 這裡會在持續檢查是否有按下鍵盤的 "A" 按鍵, 
  // 若有按下 "A" 按鍵的話則複製物件到場景上. 
  private void Update(){
   // 將 gobj 存放的物件複製一份到場景上
    if(Input.GetKeyDown(KeyCode.A)){
        Instantiate(this.gobj);
    }
  }
}