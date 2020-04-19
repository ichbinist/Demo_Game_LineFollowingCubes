using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line_Render : MonoBehaviour
{
    //Publics
    public GameObject linePoint;
    public GameObject finishingPoint;
    [Header("Add New Points to Position")]
    public Vector3 position = new Vector3(0,0,0);
    [Header("Points to Draw Line and to follow")]
    public List<GameObject> linePoints;
    //Privates
    [SerializeField]
    private LineRenderer linerender;
    private bool added = false;

    void Start()
    {
      linerender = gameObject.AddComponent<LineRenderer>();
    }

    void setLinePoints(){
      foreach (Transform child in gameObject.transform){if(child.CompareTag("point")){linePoints.Add(child.gameObject);} }
    }

    public void addPoint(){
      added = true;
      GameObject Addedobject = Instantiate(linePoint, position, Quaternion.identity);
      Addedobject.transform.parent = transform;
      linePoints.Add(Addedobject);
    }

    public void resetPoints(){
      foreach(GameObject linePoint in linePoints){DestroyImmediate(linePoint);}
      linePoints.Clear();
    }
    void Update()
    {

    }
}
