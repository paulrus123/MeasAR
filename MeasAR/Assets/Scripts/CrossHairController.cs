using UnityEngine;

/* 
 * CrossHairController controls the crosshair icon. The crosshair will visualize
 * where the device camera is currently raycasting to, if the raycast hits a 
 * trackable object (e.g. detected plane). The CrossHair controller also exposes
 * if the attached GameObject is colliding with a placed point. 
 */
public class CrossHairController : MonoBehaviour
{
    public Vector3 cameraPositionOffset;
    public Vector3 cameraRotationOffset;

    public bool isHoveringOverPoint = false;
    public MeasurablePoint hoveredOverPoint;

    // Update is called once per frame
    private void Update()
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

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<MeasurablePoint>() != null)
        {
            isHoveringOverPoint = true;
            hoveredOverPoint = other.GetComponent<MeasurablePoint>();
        }
    }

    /* LateUpdate is called after all other Components' Update methods are called. This enables
     * us to set the isHoveringOverPointFalse so it can be re-enabled (if necessary) at the next update
     * cycle.
     * 
     * Reference: https://answers.unity.com/questions/795964/changing-a-bool-if-colliding-and-setting-back-to-f.html
     */
    private void LateUpdate()
    {
        isHoveringOverPoint = false;
    }

    private void SetCrossHairPoseToCamera()
    {
        transform.localPosition = cameraPositionOffset;
        transform.localEulerAngles = cameraRotationOffset;
    }

    private void SetCrossHairToTrackable(Pose pose)
    {
        transform.position = pose.position;
        transform.eulerAngles = pose.rotation.eulerAngles;
    }
}
