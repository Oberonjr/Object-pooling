using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(BenchmarkRunner))]
public class BenchmarkRunnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        string dir = Application.dataPath + "/../Benchmarks";
        string path = dir + "/results.csv";
        
        EditorGUILayout.Space();
        if (GUILayout.Button("Open Output Folder"))
        {
            System.IO.Directory.CreateDirectory(dir);
            EditorUtility.RevealInFinder(path);
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Clear Results File"))
        {
            if (System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.Delete(path);
            }
        }
    }
}
