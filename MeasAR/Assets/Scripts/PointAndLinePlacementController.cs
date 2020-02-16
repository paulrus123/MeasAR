using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/* 
 * PointAndLinePlacementController is responsible for adding, modifying, and deleting all of 
 * the MeasurablePoints in the scene. It sets the appropriate state for each point so that
 * the measured line is shown to the correct destination
 */
public class PointAndLinePlacementController : MonoBehaviour
{
    public ARReferencePointManager referencePointManager;
    public GameObject MeasureablePointPrefab;

    public List<MeasurablePoint> measureblePoints;

    public void AddNewPoint()
    {
        ARReferencePoint result = CreateAnchor();
        if (result != null)
        {
            var go = Instantiate(MeasureablePointPrefab);
            go.transform.position = result.transform.position;
            go.transform.parent = result.transform;
            MeasurablePoint currentPoint = go.GetComponent<MeasurablePoint>();

            measureblePoints.Add(currentPoint);
            currentPoint.state = MeasurablePoint.PointState.ACTIVE;
            if (measureblePoints.Count > 1)
            {
                MeasurablePoint lastPoint = measureblePoints[measureblePoints.Count - 2];
                lastPoint.nextPoint = currentPoint;
                if (lastPoint.state != MeasurablePoint.PointState.END)
                {
                    lastPoint.state = MeasurablePoint.PointState.BRIDGE;
                }
            }
        }
    }

    //TODO:refactor this function
    public void RemovePoint(MeasurablePoint point)
    {
        //Check if point exists
        int index = measureblePoints.FindIndex((MeasurablePoint obj) => Object.ReferenceEquals(obj, point));
        if(index == -1)
        {
            Debug.Log("Tried to remove a non-tracked point");
            Destroy(point.gameObject);
            return;
        }

        int isFirstMiddleOrLast = -1; //1: first, 2: middle, 3: last
        //Check if point was first
        int previousIndex = index - 1;
        int nextIndex = index + 1;
        if (previousIndex < 0)
            isFirstMiddleOrLast = 1;
        else if (measureblePoints[previousIndex].state == MeasurablePoint.PointState.END)
            isFirstMiddleOrLast = 1;
        //check if point is end 
        else if (nextIndex >= measureblePoints.Count)
            isFirstMiddleOrLast = 3;
        else if ((measureblePoints[index].state == MeasurablePoint.PointState.END) || (measureblePoints[index].state == MeasurablePoint.PointState.ACTIVE))
            isFirstMiddleOrLast = 3;
        else
            isFirstMiddleOrLast = 2;

        switch(isFirstMiddleOrLast)
        {
            case 1:
            {
                    //Delete point and anchor
                    ARReferencePoint referencePoint = point.transform.parent.GetComponent<ARReferencePoint>();
                    referencePointManager.RemoveReferencePoint(referencePoint);
                    break;
            }
            case 2:
            {
                    //Delete point and anchor
                    ARReferencePoint referencePoint = point.transform.parent.GetComponent<ARReferencePoint>();
                    referencePointManager.RemoveReferencePoint(referencePoint);
                    //Make previous point point towards next point
                    measureblePoints[previousIndex].state = MeasurablePoint.PointState.BRIDGE;
                    measureblePoints[previousIndex].nextPoint = measureblePoints[nextIndex];
                    break;
            }
            case 3:
            {
                    //Delete point and anchor
                    ARReferencePoint referencePoint = point.transform.parent.GetComponent<ARReferencePoint>();
                    referencePointManager.RemoveReferencePoint(referencePoint);
                    //make previous point an end point
                    measureblePoints[previousIndex].state = MeasurablePoint.PointState.END;
                    break;
            }
        }
        measureblePoints.Remove(point);

    }

    public void FinishCurrentLine()
    {
        if (measureblePoints.Count > 0)
        {
            measureblePoints[measureblePoints.Count - 1].state = MeasurablePoint.PointState.END;
        }
    }

    public bool CheckIfAnyPointIsActive()
    {
        bool result = false;
        foreach(var point in measureblePoints)
        {
            if (point.state == MeasurablePoint.PointState.ACTIVE)
                result = true;
        }

        return result;
    }

    private void Start()
    {
        measureblePoints = new List<MeasurablePoint>();
    }

    //TODO: Consider anchoring ReferencePoint to a detected plane instead of just anchoring to worldspace pose
    private ARReferencePoint CreateAnchor()
    {
        if (!RayCastController.isRaycastingToTrackable)
            return null;
        else
        {
            return referencePointManager.AddReferencePoint(RayCastController.hitPose);
        }
    }
}
