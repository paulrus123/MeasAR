using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MeasurablePoint : MonoBehaviour
{
    public enum PointState { ACTIVE, BRIDGE, END}; //BRIDGE point bridges to another point, END point is last point in line segment, ACTIVE is active point
    public PointState state
    {
        get { return m_state; }
        set
        {
            m_state = value;
            if(m_state != PointState.ACTIVE)
            {
                SetLineDestination(transform.position);
            }
        }
    }
    private PointState m_state;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    public void SetLineDestination(Vector3 destination)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, destination);
    }
}
