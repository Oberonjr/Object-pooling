using System.Collections.Generic;
using UnityEngine;

public class InstantiationStrategy : ScriptableObject, IInstantiateStrategy
{
    public virtual void InitializePrefabs(int numberOfPrefabs, GameObject prefab, List<GameObject> objects)
    {
        
    }

    public virtual GameObject CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent)
    {
        return null;
    }

    public virtual void DestroyPrefab(GameObject prefab)
    {
        
    }

    public virtual void DestroyPrefabs(List<GameObject> objects)
    {
        
    }
}
