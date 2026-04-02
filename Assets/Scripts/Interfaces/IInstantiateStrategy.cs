using System.Collections.Generic;
using UnityEngine;

public interface IInstantiateStrategy
{
    void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects);
    void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent);
    void DestroyPrefab(List<GameObject> objects);
}
