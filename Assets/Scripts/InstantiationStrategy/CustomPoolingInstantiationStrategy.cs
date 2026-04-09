using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Instantiation Strategy", menuName = "Instantiation Strategies/Custom Pooling Instantiation Strategy")]
public class CustomPoolingInstantiationStrategy : InstantiationStrategy
{
    public override void InitializePrefabs(int numberOfPrefabs, GameObject prefab, List<GameObject> objects)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    public override void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, int  objIndex = 1)
    {
        objects[objIndex].transform.position = position;
        objects[objIndex].SetActive(true);
    }

    public override void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent, int  objIndex = 1)
    {
        objects[objIndex].transform.position = position;
        objects[objIndex].SetActive(true);
        objects[objIndex].transform.SetParent(parent);
    }

    public override void DestroyPrefab(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
