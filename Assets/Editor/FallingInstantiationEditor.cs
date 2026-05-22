using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FallingInstantiation))]
public class FallingInstantiationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        FallingInstantiation instantiatedObject = (FallingInstantiation)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Start Spawning"))
        {
            instantiatedObject.StartSpawning();
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Despawn Objects"))
        {
            instantiatedObject.DespawnAllObjects();
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Hard Destroy  Prefabs"))
        {
            try
            {
                instantiatedObject.DespawnAllObjects();
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                Debug.Log("Hard destroy error: probably no parent found");
            }
        }
    }
}