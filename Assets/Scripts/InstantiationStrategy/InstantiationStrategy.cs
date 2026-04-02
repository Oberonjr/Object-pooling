using System.Collections.Generic;
using UnityEngine;

public class InstantiationStrategy : ScriptableObject, IInstantiateStrategy
{
    public virtual void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects)
    {
        
    }

    public virtual void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent)
    {
        
    }

    public virtual void DestroyPrefab(List<GameObject> objects)
    {
        
    }
}
