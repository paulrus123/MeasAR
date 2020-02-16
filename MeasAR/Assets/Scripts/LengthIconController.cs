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

        if(measurablePoint.state == MeasurablePoint.PointState.ACTIVE)
        {
            CurrentLineLengthUIController.isActive = true;
            CurrentLineLengthUIController.currentLengthText = text.text;
        }
    }

    private void SetTextMetric()
    {
        float distanceInCentimeters = measurablePoint.GetDistance() * 100f;
        text.text = distanceInCentimeters.ToString("F0") + " cm";
    }

    private void SetTextImperial()
    {
        float distanceInFeet = measurablePoint.GetDistance() * 3.28084f;

        float roundedDistanceInFeet = Mathf.Floor(distanceInFeet);
        float inches = Mathf.Floor((distanceInFeet - roundedDistanceInFeet) * 12);

        //float distanceInInches = measurablePoint.GetDistance() * 100f / 2.54f;
        text.text = roundedDistanceInFeet.ToString("F0") + "' " + inches.ToString("F0") + "\"";
    }
}
