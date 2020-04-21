using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line_Render : MonoBehaviour
{
    //Publics
    public GameObject linePoint;
    public GameObject finishingPoint;
    public GameObject stopPoint;
    public int team = 0;
    public bool collision = false;
    public bool isfinished = false;
    public bool isadded = false;
    public float speed = 6f;
    public bool clicked = false;
    [Header("Add New Points to Position")]
    public Vector3 position = new Vector3(0,0,0);
    [Header("Points to Draw Line and to follow")]
    public List<GameObject> linePoints;
    public bool movestart = false;
    //Privates
    [SerializeField]
    private LineRenderer linerender;
    private int i = 1;
    private GameObject controller;

    [SerializeField]
    private GameObject[] obstacles;


    void Start()
    {
        linerender = GetComponent<LineRenderer>();
        linerender.SetWidth(0.2f,0.2f);
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        teamcolor();
        controller = GameObject.FindGameObjectWithTag("controller");
    }

    void teamcolor(){
      switch (team)
      {
      case 0:
          GetComponent<Renderer>().material.color = Color.blue;
          break;
      case 1:
          GetComponent<Renderer>().material.color = Color.black;
          break;
      case 2:
          GetComponent<Renderer>().material.color = Color.yellow;
          break;
      case 3:
          GetComponent<Renderer>().material.color = Color.green;
          break;
      case 4:
          GetComponent<Renderer>().material.color = Color.red;
          break;
      default:
          break;
      }
    }
    public void addPoint(){
      GameObject Addedobject = Instantiate(linePoint, transform.position+position, Quaternion.identity);
      linePoints.Add(Addedobject);
    }

    public void addEnd(){
      GameObject Addedobject = Instantiate(finishingPoint, transform.position+position, Quaternion.identity);
      linePoints.Add(Addedobject);
    }

    public void addStop(){
      GameObject Addedobject = Instantiate(stopPoint, transform.position+position, Quaternion.identity);
      linePoints.Add(Addedobject);
      }

    public void PointsToLine(){
      if(gameObject.GetComponent<LineRenderer>() == null){
        gameObject.AddComponent<LineRenderer>();
        linerender = GetComponent<LineRenderer>();
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
     startMovement(true);
     foreach(GameObject obstacle in obstacles){
       obstacle.GetComponent<Obstacle_Control>().switchactive();
     }
     controller.GetComponent<Game_Controller>().TeamMovement(team);
  }

    void startMovement(bool start){
      movestart = start;
      clicked = false;
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
     bool colTrigger = false;
     Debug.Log("Collision");
      if (other.CompareTag("cube")){
        Application.LoadLevel(Application.loadedLevel);
      }else if (other.CompareTag("obstacle") && other.GetComponent<Obstacle_Control>().active){
        Application.LoadLevel(Application.loadedLevel);
      }else if(other.CompareTag("stop")){
        foreach(GameObject stopPoint in linePoints){if(other.gameObject == stopPoint){colTrigger = true;}}
        if(colTrigger == true){
          startMovement(false);
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.transform.parent = this.transform;
        other.gameObject.transform.position = transform.position;
      }
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
