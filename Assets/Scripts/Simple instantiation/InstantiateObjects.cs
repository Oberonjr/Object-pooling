using UnityEngine;
using System.Collections.Generic;

public class InstantiateObjects : MonoBehaviour
{
    [SerializeField] private GameObject instantiationPrefab;
    [SerializeField] private int repetitionAmount;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private int instantiationDistance;
    [SerializeField] private Camera camera;
    [SerializeField] private InstantiationStrategy instantiationStrategy;
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(camera ==  null) camera = Camera.main;
        instantiationStrategy.InitializePrefabs(repetitionAmount,instantiationPrefab, instantiatedObjects);   
    }

    public void CreatePrefabs()
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
        for (int i = 0; i < repetitionAmount; i++)
        {
                int currentCol = i%maxCols;
                int currentRow = Mathf.FloorToInt(i/maxCols);
                localPosition.x = originX + currentCol*cellSizeX;
                localPosition.y = originY - currentRow*cellSizeY;
                localPosition.z = originZ;
                
            Vector3 worldPosition = camera.transform.position + camera.transform.right * localPosition.x + camera.transform.up * localPosition.y + camera.transform.forward * localPosition.z;
            
            try
            {
                instantiationStrategy.CreatePrefab(instantiationPrefab,
                    worldPosition, instantiatedObjects, centerPosition, i);
            }
            catch(System.Exception e)
            {
                instantiationStrategy.CreatePrefab(instantiationPrefab,
                    worldPosition, instantiatedObjects, i);
            }
            
        }
    }

    public void DestroyPrefabs()
    {
        instantiationStrategy.DestroyPrefab(instantiatedObjects);
    }

    public void HardDestroyPrefabs()
    {
        for (int i = centerPosition.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(centerPosition.GetChild(i).gameObject);
        }
        instantiatedObjects.Clear();
    }
}
