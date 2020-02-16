using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsToggleController : MonoBehaviour
{
    public enum Units { METRIC, IMPERIAL};
    public static Units unit { get; private set; }
    public Vector3 leftLocalPosition;
    public Vector3 rightLocalPosition;
    public Transform selectorIcon;

    private Button toggleButton;

    void Start()
    {
        unit = Units.METRIC;
        toggleButton = GetComponent<Button>();
        toggleButton.onClick.AddListener(ToggleUnits);
    }

    private void OnDestroy()
    {
        toggleButton.onClick.RemoveListener(ToggleUnits);
    }

    void ToggleUnits()
    {
        if(unit == Units.IMPERIAL)
        {
            unit = Units.METRIC;
            selectorIcon.localPosition = leftLocalPosition;
        }
        else
        {
            unit = Units.IMPERIAL;
            selectorIcon.localPosition = rightLocalPosition;
        }
    }
}
