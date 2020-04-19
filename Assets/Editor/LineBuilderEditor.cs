using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line_Render))]
public class LineBuilderEditor : Editor
{
  public override void OnInspectorGUI()
      {
          DrawDefaultInspector();

          Line_Render script = (Line_Render)target;
          if(GUILayout.Button("Add Point"))
          {
              script.addPoint();
          }
          if(GUILayout.Button("Reset Points"))
          {
              script.resetPoints();
          }

      }
}
