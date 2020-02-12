using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    public Vector3 cameraPositionOffset;
    public Vector3 cameraRotationOffset;
         
    // Update is called once per frame
    void Update()
    {
        if(RayCastController.isRaycastingToTrackable)
        {
            SetCrossHairToTrackable(RayCastController.hitPose);
        }
        else
        {
            SetCrossHairPoseToCamera();
        }
    }

    void SetCrossHairPoseToCamera()
    {
        transform.localPosition = cameraPositionOffset;
        transform.localEulerAngles = cameraRotationOffset;
    }

    void SetCrossHairToTrackable(Pose pose)
    {
        transform.position = pose.position;
        transform.eulerAngles = pose.rotation.eulerAngles;
    }
}
