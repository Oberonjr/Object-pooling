using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InstantiateObjects))]
public class InstantiationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        InstantiateObjects instantiatedObject = (InstantiateObjects)target;
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
        EditorGUILayout.Space();
        if (GUILayout.Button("Hard Destroy  Prefabs"))
        {
            try
            {
                instantiatedObject.HardDestroyPrefabs();
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                Debug.Log("Hard destroy error: probably no parent found");
            }
        }
    }
}
