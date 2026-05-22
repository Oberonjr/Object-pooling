using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pooling Instantiation Strategy", menuName = "Instantiation Strategies/Custom Pooling Instantiation Strategy")]
public class CustomPoolingInstantiationStrategy : InstantiationStrategy
{
    public override void InitializePrefabs(int numberOfPrefabs, GameObject prefab, List<GameObject> objects)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.TryGetComponent(out Rigidbody rb);
            if(rb != null) rb.useGravity = false;
            objects.Add(obj);
        }
    }

    public override GameObject CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent = null)
    {
        int objIndex = -1;
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                objects[i].transform.position = position;
                objects[i].SetActive(true);
                if(parent != null) objects[i].transform.SetParent(parent);
                objIndex = i;
                break;
            }
        }

        if (objIndex != -1)
        {
            return objects[objIndex];
        }
        else
        {
            Debug.LogError("No inactive object available in pool.");
            return null;
        }
    }

    public override void DestroyPrefab(GameObject prefab)
    {
        prefab.SetActive(false);
    }
    
    public override void DestroyPrefabs(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
