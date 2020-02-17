using UnityEngine;

/* 
 * MeasurablePoint is attached to each gameobject visualizing a measured point. The distance between the current point and the neext point
 * (if applicable) is exposed by the "GetDistance()" function.
 * This class references an attached LineRenderer to visualize the distance to the next point (or crosshair if point is ACTIVE)
 */
[RequireComponent(typeof(LineRenderer))]
public class MeasurablePoint : MonoBehaviour
{
    public Material dashedLineMat;
    public Material solidLineMat;
    public MeasurablePoint nextPoint;

    public enum PointState { ACTIVE, BRIDGE, END}; //BRIDGE point bridges to another point, END point is last point in line segment, ACTIVE is active point
    public PointState state
    {
        get { return m_state; }
        set
        {
            m_state = value;
            switch(m_state)
            {
                case PointState.ACTIVE:
                    lineRenderer.material = dashedLineMat;
                    break;
                case PointState.BRIDGE:
                    lineRenderer.material = solidLineMat;
                    break;
                case PointState.END:
                    lineRenderer.material = solidLineMat;
                    break;
            }
        }
    }

    public void SetLineDestination(Vector3 destination)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, destination);
    }

    //Gets distance in Unity Coordinates (m)
    public float GetDistance()
    {
        return Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
    }

    //Returns midpoint position of line in worldspace coordinates
    public Vector3 GetMidPoint()
    {
        return (lineRenderer.GetPosition(0) + lineRenderer.GetPosition(1)) * 0.5f;
    }

    private PointState m_state = PointState.ACTIVE;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = dashedLineMat;
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        switch(state)
        {
            case (PointState.ACTIVE):
                PointTowardsCrossHair();
                break;
            case (PointState.BRIDGE):
                PointTowardsNextPoint();
                break;
            case (PointState.END):
                PointTowardsSelf();
                break;
            default:
                break;
        }
    }

    private void PointTowardsCrossHair()
    {
        if (RayCastController.isRaycastingToTrackable)
            SetLineDestination(RayCastController.hitPose.position);
        else
            SetLineDestination(transform.position);
    }

    private void PointTowardsNextPoint()
    {
        if (nextPoint != null)
            SetLineDestination(nextPoint.transform.position);
    }

    private void PointTowardsSelf()
    {
        SetLineDestination(transform.position);
    }
}
