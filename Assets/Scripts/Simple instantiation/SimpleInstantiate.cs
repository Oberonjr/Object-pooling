using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SimpleInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject instantiationPrefab;
    [SerializeField] private int repetitionAmount;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private int instantiationDistance;
    [SerializeField]private Camera camera;
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

    // Update is called once per frame
    void Update()
    {
        
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
                yPosition = yPosition - (1.05f * instantiationPrefab.transform.localScale.y);
                iteration = 0;
            }
            float xPosition = (_rootPosition.x - oppLength/4)+( iteration) * instantiationPrefab.transform.localScale.x * 1.05f;
            iteration++;
            GameObject newObject = Instantiate(instantiationPrefab, new Vector3(xPosition, yPosition, instantiationDistance), Quaternion.identity);
            instantiatedObjects.Add(newObject);
        }
    }

    public void DestroyPrefabs()
    {
        foreach (GameObject obj in instantiatedObjects)
        {
            #if UNITY_EDITOR
            DestroyImmediate(obj);
            #else
            Destroy(obj, 0.1f);
            #endif
        }
        instantiatedObjects.Clear();
    }
}
