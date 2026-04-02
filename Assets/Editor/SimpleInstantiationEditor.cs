using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleInstantiate))]
public class SimpleInstantiationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        SimpleInstantiate instantiatedObject = (SimpleInstantiate)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Instantiate"))
        {
            instantiatedObject.CreatePrefabs();
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Destroy Prefabs"))
        {
            instantiatedObject.DestroyPrefabs();
        }
    }
}
