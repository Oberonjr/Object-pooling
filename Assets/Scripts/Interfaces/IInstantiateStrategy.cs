using System.Collections.Generic;
using UnityEngine;

public interface IInstantiateStrategy
{
    void InitializePrefabs(int numberOfPrefabs, GameObject prefab, List<GameObject> objects);
    GameObject CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent);
    void DestroyPrefab(GameObject prefab);
    void DestroyPrefabs(List<GameObject> objects);
}
