using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConeTestScript))]
public class ConeTestScriptEditor : Editor
{
    private ConeTestScript coneTestScript => (ConeTestScript)target;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        if (EditorGUI.EndChangeCheck())
        {
            coneTestScript.DoCheck();
        }

        if (GUILayout.Button("Do Check"))
        {
            coneTestScript.DoCheck();
        }
    }
}
