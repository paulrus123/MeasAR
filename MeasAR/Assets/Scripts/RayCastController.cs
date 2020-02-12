using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RayCastController : MonoBehaviour
{
    public static bool isRaycastingToTrackable { get; protected set; }
    public static Pose hitPose { get; protected set; }
    public Camera arCamera;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hitResults;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        hitResults = new List<ARRaycastHit>();
    }

    void Update()
    {
        Ray ray = arCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if(raycastManager.Raycast(ray,hitResults))
        {
            hitPose = hitResults[0].pose;
            isRaycastingToTrackable = true;
        }
        else
        {
            isRaycastingToTrackable = false;
        }
    }
}
