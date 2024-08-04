using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generate))]
public class GenerateInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Generate Gen = (Generate)target;
        if (GUILayout.Button("Generate"))
        {
            Gen.Run();
        }
    }
}
