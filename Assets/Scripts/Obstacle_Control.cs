using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Control : MonoBehaviour
{
    public bool active = false;

    public void switchactive(){
      active = !active;
    }
    void Update(){
      if(active == true){
          changecolor(Color.black);
      }else{
          changecolor(Color.white);
      }
    }
    void changecolor(Color color){
      for(int i = 0; i < transform.childCount;i++){
        transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = color;
      }
    }
}
