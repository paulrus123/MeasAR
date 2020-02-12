using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PointAndLinePlacementController : MonoBehaviour
{
    public ARReferencePointManager referencePointManager;
    public GameObject MeasureablePointPrefab;
    public Button addPointButton;
    public Button doneButton;

    public enum LineState { NOT_STARTED, STARTED};
    public LineState state;
    public List<MeasurablePoint> measureblePoints;

    private void Start()
    {
        state = LineState.NOT_STARTED;
        measureblePoints = new List<MeasurablePoint>();
        addPointButton.onClick.AddListener(AddButtonClicked);
        doneButton.onClick.AddListener(DoneButtonClicked);
    }

    private void OnDestroy()
    {
        addPointButton.onClick.RemoveListener(AddButtonClicked);
        doneButton.onClick.RemoveListener(DoneButtonClicked);
    }

    private void AddButtonClicked()
    {
        ARReferencePoint result = PlaceAnchor();
        if(result!=null)
        {
            var go = Instantiate(MeasureablePointPrefab);
            go.transform.position = result.transform.position;
            go.transform.parent = result.transform;
            measureblePoints.Add(go.GetComponent<MeasurablePoint>());
            go.GetComponent<MeasurablePoint>().state = MeasurablePoint.PointState.ACTIVE;
        }
    }

    private void DoneButtonClicked()
    {
        if (measureblePoints.Count > 0)
        {
            measureblePoints[measureblePoints.Count - 1].state = MeasurablePoint.PointState.END;
        }
    }

    public ARReferencePoint PlaceAnchor()
    {
        if (!RayCastController.isRaycastingToTrackable)
            return null;
        else
        {
            return referencePointManager.AddReferencePoint(RayCastController.hitPose);
        }
    }

    private void Update()
    {
        SetActivePointPathToCrosshairPosition();
    }

    private void SetActivePointPathToCrosshairPosition()
    {
        if (measureblePoints.Count > 0)
        {
            MeasurablePoint point = measureblePoints[measureblePoints.Count - 1];
            if (point.state == MeasurablePoint.PointState.ACTIVE)
            {
                if (RayCastController.isRaycastingToTrackable)
                    point.SetLineDestination(RayCastController.hitPose.position);
            }
        }
    }
}
