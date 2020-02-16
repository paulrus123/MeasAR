using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentLineLengthUIController : MonoBehaviour
{
    public static string currentLengthText;
    public static bool isActive = false;
    public Text text;
    public GameObject icon;

    void LateUpdate()
    {
        text.text = currentLengthText;
        icon.SetActive(isActive);
        isActive = false;
    }

}
