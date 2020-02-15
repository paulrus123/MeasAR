using UnityEngine;
using UnityEngine.UI;

public class LengthIconController : MonoBehaviour
{
    public MeasurablePoint measurablePoint;
    public Text text;
    public GameObject ImageGO;

    void Update()
    {
        text.text = measurablePoint.GetDistance().ToString("F1");
        if (measurablePoint.state == MeasurablePoint.PointState.END)
            ImageGO.SetActive(false);
        else
        {
            ImageGO.SetActive(true);
            transform.position = measurablePoint.GetMidPoint();
            transform.LookAt(CameraHandler.position);
        }
    }
}
