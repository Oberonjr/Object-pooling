using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingInstantiation : MonoBehaviour
{
    [SerializeField] private GameObject instantiationPrefab;
    [SerializeField] private int repetitionAmount;
    [SerializeField] private float spawnDelay;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private int instantiationDistance;
    [SerializeField] private Camera camera;
    [SerializeField] private InstantiationStrategy instantiationStrategy;
    private List<GameObject> instantiatedObjects = new List<GameObject>();
    private DespawnArea despawnArea; 
   
    void Start()
    {
        despawnArea = DespawnArea.Instance;
        instantiationStrategy.InitializePrefabs(repetitionAmount, instantiationPrefab, instantiatedObjects);
        despawnArea.ObjectCollision += DespawnObject;

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(spawnDelay);
        GameObject obj = instantiationStrategy.CreatePrefab(instantiationPrefab, centerPosition.position, instantiatedObjects, centerPosition);
        if (obj != null)
        {
            obj.TryGetComponent(out Rigidbody rb);
            if (rb != null)
            {
                rb.useGravity = true;
            }
            else
            {
                Debug.Log("Couldn't find a rigidbody attached to: "  + obj.name);
            }
        }
        else
        {
            Debug.LogWarning("Couldn't find object to spawn when calling SpawnObjects().");
        }

        StartCoroutine(SpawnObjects());
    }
    
    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    private void DespawnObject(GameObject obj)
    {
        obj.TryGetComponent(out Rigidbody rb);
        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
        }
        instantiationStrategy.DestroyPrefab(obj);
    }

    public void DespawnAllObjects()
    {
        instantiationStrategy.DestroyPrefabs(instantiatedObjects);
    }
}
