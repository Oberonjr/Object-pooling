using System.Collections.Generic;
using UnityEngine;

public class InstantiationStrategy : ScriptableObject, IInstantiateStrategy
{
    public virtual void InitializePrefabs(int numberOfPrefabs, GameObject prefab, List<GameObject> objects)
    {
        
    }
    
    public virtual void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, int  objIndex = 1)
    {
        
    }

    public virtual void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent,  int objIndex = 1)
    {
        
    }

    public virtual void DestroyPrefab(List<GameObject> objects)
    {
        
    }
}
