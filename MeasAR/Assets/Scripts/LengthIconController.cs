using UnityEngine;
using UnityEngine.UI;

/*
 * LengthIconController is responsible for rendering the length of the measured line 
 * using the correct units
 */
public class LengthIconController : MonoBehaviour
{
    public MeasurablePoint measurablePoint;
    public Text text;
    public GameObject ImageGO;

    void Update()
    {
        if (measurablePoint.state == MeasurablePoint.PointState.END)
            ImageGO.SetActive(false);
        else
        {
            ImageGO.SetActive(true);
            transform.position = measurablePoint.GetMidPoint();
            transform.LookAt(CameraHandler.position);
        }
        if (UnitsToggleController.unit == UnitsToggleController.Units.IMPERIAL)
            SetTextImperial();
        else
            SetTextMetric();
    }

    private void SetTextMetric()
    {
        float distanceInCentimeters = measurablePoint.GetDistance() * 100f;
        text.text = distanceInCentimeters.ToString("F0") + " cm";
    }

    private void SetTextImperial()
    {
        float distanceInInches = measurablePoint.GetDistance() * 100f / 2.54f;
        text.text = distanceInInches.ToString("F0") + "\"";
    }
}
