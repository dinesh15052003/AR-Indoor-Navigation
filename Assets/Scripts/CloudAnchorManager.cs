using Google.XR.ARCoreExtensions;
using Google.XR.ARCoreExtensions.Samples.PersistentCloudAnchors;
using System;
using System.Collections;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CloudAnchorManager : MonoBehaviour
{
    public GameObject AnchorPrefab;
    public TMP_Text DebugText;

    private ARAnchor arAnchor;
    private ARAnchorManager arAnchorManager;

    private HostCloudAnchorPromise _hostPromise = null;
    private HostCloudAnchorResult _hostResult = null;

    private ResolveCloudAnchorPromise _resolvePromise = null;
    private ResolveCloudAnchorResult _resolveResult = null;

    private CloudAnchorHistory _hostedCloudAnchor;

    public static string mode = null;

    #region Public Functions
    public void AnchorContent()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Environment");
        if (obj != null)
        {
             arAnchor = obj.AddComponent<ARAnchor>();
        }
    }

    public void OnHostBtnClicked()
    {
        mode = "hosting";
    }

    public void OnResolveBtnClicked()
    {
        mode = "resolving";
    }
    #endregion

    #region Private Functions
    private void Awake()
    {
        arAnchorManager = FindObjectOfType<ARAnchorManager>();
    }
 
    private void Start()
    {
        
    }

    private void Update()
    {
        if (mode == "hosting")
        {
            HostingCloudAnchor();
        }
        else if (mode == "resolving")
        {
            ResolvingCloudAnchor();
        }
    }


    private void HostingCloudAnchor()
    {
        // There is no anchor for hosting.
        if (arAnchor == null)
        {
            return;
        }

        // There is a pending or finished hosting task.
        if (_hostPromise != null || _hostResult != null)
        {
            return;
        }

        FeatureMapQuality quality = arAnchorManager.EstimateFeatureMapQualityForHosting(GetCameraPose());
        DebugText.text = "\nCurrent mapping quality: " + quality;

        if (quality == FeatureMapQuality.Good)
        {
            DebugText.text = "\nMapping quality has reached sufficient threshold, creating Cloud Anchor.\n";
            DebugText.text = string.Format("FeatureMapQuality has reached {0}, triggering CreateCloudAnchor.", arAnchorManager.EstimateFeatureMapQualityForHosting(GetCameraPose()));

            // Creating a Cloud Anchor with lifetime = 1 day.
            // This is configurable up to 365 days when keyless authentication is used.
            var promise = arAnchorManager.HostCloudAnchorAsync(arAnchor, 1);
            if (promise.State == PromiseState.Done)
            {
                DebugText.text = "Failed to host a Cloud Anchor.";
            }
            else
            {
                _hostPromise = promise;
                StartCoroutine(HostAnchor());
            }
        }
        
    }

    private IEnumerator HostAnchor()
    {
        yield return _hostPromise;
        _hostResult = _hostPromise.Result;
        _hostPromise = null;

        if (_hostResult.CloudAnchorState == CloudAnchorState.Success)
        {
            _hostedCloudAnchor = new CloudAnchorHistory("CloudAnchor", _hostResult.CloudAnchorId);
            DebugText.text = "\ntrue, " + _hostResult.CloudAnchorId;
            PlayerPrefs.SetString("ID", _hostResult.CloudAnchorId);
            DebugText.text += "\n Added Cloud Anchor Id " + PlayerPrefs.GetString("ID");
        }
        else
        {
            DebugText.text = "\nfalse, " + _hostResult.CloudAnchorState.ToString();
        }
    }

    private void ResolvingCloudAnchor()
    {
        // There is no anchorID for resolving
        if (!PlayerPrefs.HasKey("ID"))
        {
            DebugText.text = "No AnchorID to resolve";
            return;
        }
        // There is a pending or finished hosting task
        if (_resolvePromise != null || _resolveResult != null)
        {
            return;
        }
        // ARCore session is not ready for resolving.
        if (ARSession.state != ARSessionState.SessionTracking)
        {
            return;
        }
        DebugText.text = "Attempting to resolve "+PlayerPrefs.GetString("ID");
        string cloudId = PlayerPrefs.GetString("ID");
        var promise = arAnchorManager.ResolveCloudAnchorAsync(cloudId);
        if (promise.State == PromiseState.Done)
        {
            DebugText.text = "Failed to resolve Cloud Anchor " + cloudId;
        }
        else
        {
            _resolvePromise = promise;
            StartCoroutine(ResolveAnchor(cloudId, promise));
        }
    }

    private IEnumerator ResolveAnchor(string cloudId, ResolveCloudAnchorPromise promise)
    {
        yield return promise;
        var result = promise.Result;
        _resolveResult = result;
        _resolvePromise = null;

        if (result.CloudAnchorState == CloudAnchorState.Success)
        {
            DebugText.text = "true, " + cloudId;
            Instantiate(AnchorPrefab, result.Anchor.transform);
        }
        else
        {
            DebugText.text = "false, " + cloudId;
        }
    }

    private Pose GetCameraPose()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        return new Pose(mainCamera.transform.position, mainCamera.transform.rotation);
    }
    #endregion

}
