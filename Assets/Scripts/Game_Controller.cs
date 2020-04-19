using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
  private GameObject[] cubes;
  private int finishcount = 0;
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
}
