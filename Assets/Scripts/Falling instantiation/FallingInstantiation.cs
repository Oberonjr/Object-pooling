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
    private int repetitionCounter = 0;
   
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
                repetitionCounter++;
                if (repetitionCounter >= repetitionAmount)
                {
                    repetitionCounter = 0;
                    StopSpawning();
                }
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

    public void SpawnAllAtOnce()
    {
        if(instantiatedObjects == null)  instantiatedObjects = new List<GameObject>();
        if (instantiatedObjects.Count <= 0)
        {
            instantiationStrategy.InitializePrefabs(repetitionAmount,instantiationPrefab, instantiatedObjects);
        }
        float frustumHeight = 2 * instantiationDistance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad); 
        float frustumWidth = frustumHeight * camera.aspect;
        float cellSizeX = instantiationPrefab.transform.localScale.x * 1.05f;
        float cellSizeY = instantiationPrefab.transform.localScale.y * 1.05f;
        int maxCols = Mathf.FloorToInt(frustumWidth / cellSizeX);
        float originX = -(maxCols*cellSizeX) /2 + cellSizeX / 2;
        float originY = frustumHeight / 2 - cellSizeY / 2;
        float originZ = instantiationDistance;
        
        Vector3 localPosition = new Vector3();
        while(repetitionCounter < repetitionAmount)
        {
            int currentCol = repetitionCounter%maxCols;
            int currentRow = Mathf.FloorToInt(repetitionCounter/maxCols);
            localPosition.x = originX + currentCol*cellSizeX;
            localPosition.y = originY - currentRow*cellSizeY;
            localPosition.z = originZ;
                
            Vector3 worldPosition = camera.transform.position + camera.transform.right * localPosition.x + camera.transform.up * localPosition.y + camera.transform.forward * localPosition.z;
            GameObject obj = instantiationStrategy.CreatePrefab(instantiationPrefab, worldPosition, instantiatedObjects, centerPosition);
            if (obj != null)
            {
                obj.TryGetComponent(out Rigidbody rb);
                if (rb != null)
                {
                    rb.useGravity = true;
                }
            }

            repetitionCounter++;
        }
        repetitionCounter = 0;
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
