using System.Collections.Generic;
using UnityEngine;

public interface IInstantiateStrategy
{
    void InitializePrefabs(int numberOfPrefabs, GameObject prefab, List<GameObject> objects);
    void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, int objIndex = 1);
    void CreatePrefab(GameObject prefab, Vector3 position, List<GameObject> objects, Transform parent, int objIndex = 1);
    void DestroyPrefab(List<GameObject> objects);
}
