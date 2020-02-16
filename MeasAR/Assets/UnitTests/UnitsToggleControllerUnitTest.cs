using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class UnitsToggleControllerUnitTest
    {

        UnitsToggleController controller;
        Vector3 leftIconPosition;
        Vector3 rightIconPosition; 

        [SetUp]
        public void Setup()
        {
            leftIconPosition = new Vector3(1, 0, 0);
            rightIconPosition = new Vector3(-1, 0, 0);

            var controllerGO = Object.Instantiate(new GameObject());
            controller = controllerGO.AddComponent<UnitsToggleController>();

            var selectorIconGO = Object.Instantiate(new GameObject());
            controller.selectorIcon = selectorIconGO.transform;
            controller.leftLocalPosition = leftIconPosition;
            controller.rightLocalPosition = rightIconPosition;
            controller.toggleButton = selectorIconGO.AddComponent<Button>();
        }

        [UnityTest]
        public IEnumerator ControllerInitializesToImperialUnitsTest()
        {
            Assert.IsTrue(UnitsToggleController.unit == UnitsToggleController.Units.IMPERIAL);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ControllerSetsToMetricAndImperialTest()
        {

            Assert.IsTrue(UnitsToggleController.unit == UnitsToggleController.Units.IMPERIAL);
            controller.ToggleUnits();
            yield return null;
            Assert.IsTrue(UnitsToggleController.unit == UnitsToggleController.Units.METRIC);
            controller.ToggleUnits();
            yield return null;
            Assert.IsTrue(UnitsToggleController.unit == UnitsToggleController.Units.IMPERIAL);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ToggleUIIconSetsLeftWithImperialTest()
        {
            Assert.IsTrue(UnitsToggleController.unit == UnitsToggleController.Units.IMPERIAL);
            Assert.IsTrue(controller.selectorIcon.transform.localPosition == leftIconPosition);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ToggleUIIconSetsRightWithMetricTest()
        {
            Assert.IsTrue(UnitsToggleController.unit == UnitsToggleController.Units.IMPERIAL);
            Assert.IsTrue(controller.selectorIcon.transform.localPosition == leftIconPosition);
            controller.ToggleUnits();
            yield return null;
            Assert.IsTrue(UnitsToggleController.unit == UnitsToggleController.Units.METRIC);
            Assert.IsTrue(controller.selectorIcon.transform.localPosition == rightIconPosition);
            yield return null;
        }
    }
}
