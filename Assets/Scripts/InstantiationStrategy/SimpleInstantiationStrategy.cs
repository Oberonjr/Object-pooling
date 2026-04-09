using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Instantiation Strategy", menuName = "Instantiation Strategies/Simple Instantiation Strategy")]
public class SimpleInstantiationStrategy : InstantiationStrategy, IInstantiateStrategy
{
    public override void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, int objIndex = 1)
    {
        GameObject instantiatedObject = Instantiate(prefab, position, Quaternion.identity);
        objects.Add(instantiatedObject);
    }

    public override void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent,  int objIndex = 1)
    {
        GameObject instantiatedObject = Instantiate(prefab, position, Quaternion.identity);
        instantiatedObject.transform.SetParent(parent.transform);
        objects.Add(instantiatedObject);
    }

    public override void DestroyPrefab(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            #if UNITY_EDITOR
            DestroyImmediate(obj);
            #else
            Destroy(obj, 0.1f);
            #endif
        }
        objects.Clear();
    }
}
