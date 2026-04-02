using UnityEngine;
using System.Collections.Generic;

public class InstantiateObects : MonoBehaviour
{
    [SerializeField] private GameObject instantiationPrefab;
    [SerializeField] private int repetitionAmount;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private int instantiationDistance;
    [SerializeField] private Camera camera;
    [SerializeField] private InstantiationStrategy instantiationStrategy;
    private Vector3 _rootPosition;
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(camera ==  null) camera = Camera.main;
        if (centerPosition == null)
        {
            _rootPosition = new Vector3();
        }
        else
        {
            _rootPosition = centerPosition.position;
        }
    }

    public void CreatePrefabs()
    {
        int iteration = 0;
        float hypLength = Mathf.Abs(instantiationDistance/Mathf.Cos(camera.fieldOfView/2));
        float oppLength = Mathf.Abs((hypLength*Mathf.Sin(camera.fieldOfView/2))*2);
        float yPosition = _rootPosition.y + oppLength/12;
        for (int i = 0; i < repetitionAmount; i++)
        {
            if (iteration > oppLength/2)
            {
                yPosition -= 1.05f * instantiationPrefab.transform.localScale.y;
                iteration = 0;
            }
            float xPosition = (_rootPosition.x - oppLength/4)+( iteration) * instantiationPrefab.transform.localScale.x * 1.05f;
            iteration++;
            try
            {
                instantiationStrategy.CreatePrefab(instantiationPrefab,
                    new Vector3(xPosition, yPosition, instantiationDistance), instantiatedObjects, centerPosition);
            }
            catch(System.Exception e)
            {
                instantiationStrategy.CreatePrefab(instantiationPrefab,
                    new Vector3(xPosition, yPosition, instantiationDistance), instantiatedObjects);
            }
            
        }
    }

    public void DestroyPrefabs()
    {
        instantiationStrategy.DestroyPrefab(instantiatedObjects);
    }

    public void HardDestroyPrefabs()
    {
        for (int i = centerPosition.childCount - 1; i > 0; i--)
        {
            DestroyImmediate(centerPosition.GetChild(i).gameObject);
        }
    }
}
