using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line_Render : MonoBehaviour
{
    //Publics
    public GameObject linePoint;
    public GameObject finishingPoint;
    public bool collision = false;
    public bool isfinished = false;
    public bool isadded = false;
    public float speed = 6f;
    [Header("Add New Points to Position")]
    public Vector3 position = new Vector3(0,0,0);
    [Header("Points to Draw Line and to follow")]
    public List<GameObject> linePoints;

    //Privates
    [SerializeField]
    private LineRenderer linerender;
    private int i = 1;
    private bool movestart = false;

    [SerializeField]
    private GameObject[] obstacles;


    void Start()
    {
        linerender = GetComponent<LineRenderer>();
        linerender.SetWidth(0.2f,0.2f);
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");
    }

    public void addPoint(){
      linerender = GetComponent<LineRenderer>();
      GameObject Addedobject = Instantiate(linePoint, transform.position+position, Quaternion.identity);
      linePoints.Add(Addedobject);
    }

    public void addEnd(){

      GameObject Addedobject = Instantiate(finishingPoint, transform.position+position, Quaternion.identity);
      linePoints.Add(Addedobject);
    }

    public void PointsToLine(){
      if(gameObject.GetComponent<LineRenderer>() == null){
        linerender = gameObject.AddComponent<LineRenderer>();
        linerender.SetVertexCount(linePoints.Count);
        for (int i = 0; i < linePoints.Count; i++){
         linerender.SetPosition(i, linePoints[i].transform.position);
        }
      }else if(linerender == null){
        linerender = GetComponent<LineRenderer>();
      }else{
      linerender = GetComponent<LineRenderer>();
      linerender.SetVertexCount(linePoints.Count);
      for (int i = 0; i < linePoints.Count; i++){
       linerender.SetPosition(i, linePoints[i].transform.position);
      }
    }
    }

    void OnMouseDown(){
     startMovement();
     foreach(GameObject obstacle in obstacles){
       obstacle.GetComponent<Obstacle_Control>().switchactive();
     }
  }

    void startMovement(){
      movestart = true;
    }

    public void resetPoints(){
      foreach(GameObject linePoint in linePoints){if(linePoint != linePoints[0])DestroyImmediate(linePoint);}
      GameObject temp = linePoints[0];
      linerender.SetVertexCount(1);
      linePoints.Clear();
      linePoints.Add(temp);
    }

    void FixedUpdate()
    {
      if(transform.position == linerender.GetPosition(i) && i<linePoints.Count){i++;}
      if(transform.position == linerender.GetPosition(linePoints.Count)){isfinished = true;}
      if(movestart == true){
      transform.position = Vector3.MoveTowards(transform.position, linerender.GetPosition(i), speed * Time.deltaTime);

      for (int j = 0; j < linePoints.Count; j++){
       linerender.SetPosition(j, linePoints[j].transform.position);
      }

      }
    }

    void OnTriggerEnter(Collider other)
   {
     Debug.Log("Collision");
      if (other.CompareTag("cube")){
        Application.LoadLevel(Application.loadedLevel);
      }else if (!other.CompareTag("point") && other.transform.parent.CompareTag("obstacle") && other.transform.parent.GetComponent<Obstacle_Control>().active){
        Application.LoadLevel(Application.loadedLevel);
      }
   }

   void OnTriggerStay(Collider other){
     bool colTrigger = false;
     if (other.CompareTag("point")){
       foreach(GameObject linePoint in linePoints){if(other.gameObject == linePoint){colTrigger = true;}}
       if(colTrigger == true && transform.position == other.gameObject.transform.position){
       other.transform.parent = this.transform;
       other.gameObject.transform.position = transform.position;}
     }
   }
}
