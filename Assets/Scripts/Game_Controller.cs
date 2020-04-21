using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
  private GameObject[] cubes;
  private int finishcount = 0;
  private bool[] teams = {false,false,false,false,false};
    void Start(){
    cubes = GameObject.FindGameObjectsWithTag("cube");
    }

    void Update()
    {
        foreach(GameObject cube in cubes){
          if(cube.GetComponent<Line_Render>().isfinished == true && cube.GetComponent<Line_Render>().isadded == false){
            finishcount++;
            cube.GetComponent<Line_Render>().isadded = true;
          }
        }
        if(finishcount >= cubes.Length){
          Debug.Log("Scene Finished");
          gameObject.SetActive(false);
        }
    }

    public void TeamMovement(int i){
      foreach(GameObject cube in cubes){
      /*if(cube.GetComponent<Line_Render>().movestart == true){
        teams[cube.GetComponent<Line_Render>().team] = true;
      }else if(cube.GetComponent<Line_Render>().movestart == true){
        teams[cube.GetComponent<Line_Render>().team] = false;
      }*/

      if(cube.GetComponent<Line_Render>().team != 0 && cube.GetComponent<Line_Render>().team == i){
        cube.GetComponent<Line_Render>().movestart = true;
      }
      }
    }
}
