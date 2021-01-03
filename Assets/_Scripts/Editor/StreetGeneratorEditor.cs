using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StreetGenerator))]
public class StreetGeneratorEditor : Editor
{
    /// <summary>
    /// The target street generator
    /// </summary>
    private StreetGenerator streetGenerator => (StreetGenerator)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Remove Streets"))
            streetGenerator.RemoveStreets();

        if (GUILayout.Button("Create Streets"))
            streetGenerator.GenerateStreets();

        EditorGUILayout.LabelField(string.Format("Generation time: {0:0.00} ms", streetGenerator.lastGenerationDuration));
    }
}
