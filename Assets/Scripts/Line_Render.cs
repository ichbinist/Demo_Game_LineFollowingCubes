using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line_Render : MonoBehaviour
{
    //Publics
    public GameObject linePoint;
    public GameObject finishingPoint;
    public float speed = 4f;
    [Header("Add New Points to Position")]
    public Vector3 position = new Vector3(0,0,0);
    [Header("Points to Draw Line and to follow")]
    public List<GameObject> linePoints;
    //Privates
    [SerializeField]
    private LineRenderer linerender;
    private bool added = false;
    private int i = 1;
    private bool movestart = false;

    void Start()
    {
        linerender.SetWidth(0.2f,0.2f);
    }

    void setLinePoints(){
      foreach (Transform child in gameObject.transform){if(child.CompareTag("point")){linePoints.Add(child.gameObject);} }
    }

    public void addPoint(){
      added = true;
      GameObject Addedobject = Instantiate(linePoint, transform.position+position, Quaternion.identity);
      Addedobject.transform.parent = transform;
      linePoints.Add(Addedobject);
    }

    public void addEnd(){
      added = true;
      GameObject Addedobject = Instantiate(finishingPoint, transform.position+position, Quaternion.identity);
      Addedobject.transform.parent = transform;
      linePoints.Add(Addedobject);
    }
    
    public void PointsToLine(){
      if(gameObject.GetComponent<LineRenderer>() == null){
        linerender = gameObject.AddComponent<LineRenderer>();
        linerender.SetVertexCount(linePoints.Count);
        for (int i = 0; i < linePoints.Count; i++){
         linerender.SetPosition(i, linePoints[i].transform.position);
        }
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
  }

    void startMovement(){
      movestart = true;
    }

    public void resetPoints(){
      foreach(GameObject linePoint in linePoints){DestroyImmediate(linePoint);}
      linerender.SetVertexCount(0);
      linePoints.Clear();
    }

    void FixedUpdate()
    {
      if(transform.position == linerender.GetPosition(i) && i<linePoints.Count){i++;}
      if(movestart == true){
      transform.position = Vector3.MoveTowards(transform.position, linerender.GetPosition(i), speed * Time.deltaTime);
      }
    }
}
