using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    private PlacementIndicator placementIndicator;
    private GameObject obj = null;

    private ARPlaneManager arPlaneManager;

    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        //if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //{
        //    if (obj == null)
        //    {
        //        obj = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
        //        placementIndicator.gameObject.SetActive(false);
        //        DisablePlaneVisibility();
        //    }
        //}
    }

    private void DisablePlaneVisibility()
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        arPlaneManager.enabled = false;
    }

    public void Activate()
    {
        GameObject obj = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
        DisablePlaneVisibility();
    }


}
