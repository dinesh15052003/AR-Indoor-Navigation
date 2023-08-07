using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class UnityEventResolver : UnityEvent<Transform> { }

public class ARCloudAnchorManager : MonoBehaviour
{
    //[SerializeField] private Camera arCamera;
    //[SerializeField] private float resolveAnchorPassedTimeout = 10.0f;
    //private ARAnchorManager arAnchorManager = null;
    //private ARAnchor pendingHostAnchor = null;
    //private ARCloudAnchor cloudAnchor = null;
    //private string anchorToResolve;
    //private bool anchorUpdateInProgress = false;
    //private bool anchorResolveInProgress = false;
    //private float safeToResolvePassed = 0;
    //private UnityEventResolver resolver = null;

    private void Awake()
    {
        //resolver = new UnityEventResolver();
        //resolver.AddListener((t) => ARPla)
    }

    //private Pose GetCameraPose()
    //{
        //return new Pose(arCamera.transform.position, arCamera.transform.rotation);
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
