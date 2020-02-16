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

    public Button toggleButton;

    private void Awake()
    {
        unit = Units.IMPERIAL;
    }

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleUnits);
        selectorIcon.localPosition = leftLocalPosition;
    }

    private void OnDestroy()
    {
        toggleButton.onClick.RemoveListener(ToggleUnits);
    }

    public void ToggleUnits()
    {
        if(unit == Units.IMPERIAL)
        {
            unit = Units.METRIC;
            selectorIcon.localPosition = rightLocalPosition;
        }
        else
        {
            unit = Units.IMPERIAL;
            selectorIcon.localPosition = leftLocalPosition;
        }
    }
}
