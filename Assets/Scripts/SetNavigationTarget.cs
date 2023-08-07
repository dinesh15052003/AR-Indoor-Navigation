using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigationTarget : MonoBehaviour
{
    public Transform Kitchen, BedRoom1, BedRoom2, DiningRoom, LivingRoom, PoojaRoom;

    public GameObject FloorCube;

    public GameObject canvas;

    private GameObject arCamera;

    private Transform targetTransform;
    private NavMeshPath path;
    private LineRenderer line;
    private NavMeshHit navMeshHit;

    private bool toggle = false;

    public void SetTargetTransform(int index)
    {
        switch (index)
        {
            case 0: 
                targetTransform = Kitchen; break;
            case 1:
                targetTransform = BedRoom1; break;
            case 2:
                targetTransform = BedRoom2; break;
            case 3:
                targetTransform = DiningRoom; break;
            case 4:
                targetTransform = LivingRoom; break;
            case 5:
                targetTransform = PoojaRoom; break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera");
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = true;
    }

    public void OnTogglePathBtnClicked()
    {
        toggle = !toggle;
        line.enabled = toggle;
    }

    // Update is called once per frame
    void Update()
    {
        if (CloudAnchorManager.mode != "resolving")
        {
            canvas.SetActive(false); 
            line.enabled = false;
        }
        else
        {
            canvas.SetActive(true);
            FloorCube.GetComponent<MeshRenderer>().material.shader = Shader.Find("VR/SpatialMapping/Occlusion");
        }


        transform.position = arCamera.transform.position;
        if (targetTransform != null )
        {
            NavMesh.SamplePosition(targetTransform.position, out navMeshHit, 100f, NavMesh.AllAreas);
            Vector3 targetPosistion = navMeshHit.position;
            NavMesh.CalculatePath(transform.position, targetPosistion, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
        }
    }
}
