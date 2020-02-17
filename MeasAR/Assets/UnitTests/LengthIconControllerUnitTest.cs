using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class LengthIconControllerUnitTest
    {
        LengthIconController controller;
        MeasurablePoint measurablePoint;
        UnitsToggleControllerMock unitsToggleControllerMock;

        [SetUp]
        public void Setup()
        {
            var controllerGO = Object.Instantiate(new GameObject());
            controller = controllerGO.AddComponent<LengthIconController>();
            var ImageGO = Object.Instantiate(new GameObject());
            controller.ImageGO = ImageGO;
            controller.text = ImageGO.AddComponent<Text>();

            measurablePoint = SetupMeasurablePoint();
            unitsToggleControllerMock = SetupUnitsToggleMock();
        }

        private UnitsToggleControllerMock SetupUnitsToggleMock()
        {
            var unitsToggleMockGO = Object.Instantiate(new GameObject());
            var controllerMock = unitsToggleMockGO.AddComponent<UnitsToggleControllerMock>();
            controllerMock.toggleButton = unitsToggleMockGO.AddComponent<Button>();
            controllerMock.selectorIcon = unitsToggleMockGO.transform;
            return controllerMock;
        }

        private MeasurablePoint SetupMeasurablePoint()
        {
            var measureablePointGO = Object.Instantiate(new GameObject());
            controller.measurablePoint = measureablePointGO.AddComponent<MeasurablePoint>();
            return controller.measurablePoint;
        }

        [UnityTest] 
        public IEnumerator LengthIconInactiveIfPointIsEndTest()
        {
            controller.measurablePoint.state = MeasurablePoint.PointState.END;
            yield return null;
            Assert.IsTrue(controller.ImageGO.activeSelf == false);
        }

        [UnityTest]
        public IEnumerator LengthIconActiveIfPointIsActiveOrBridgeTest()
        {
            controller.measurablePoint.state = MeasurablePoint.PointState.ACTIVE;
            yield return null;
            Assert.IsTrue(controller.ImageGO.activeSelf == true);

            controller.measurablePoint.state = MeasurablePoint.PointState.BRIDGE;
            yield return null;
            Assert.IsTrue(controller.ImageGO.activeSelf == true);
        }

        [UnityTest]
        public IEnumerator LengthIconDisplaysCorrectDistanceInImperialUnitsTest()
        {

            controller.measurablePoint.state = MeasurablePoint.PointState.BRIDGE;
            UnitsToggleControllerMock.SetUnits(UnitsToggleController.Units.IMPERIAL);
            SetLineLength(1f);

            yield return null;
            Assert.IsTrue(controller.ImageGO.activeSelf == true);
            Assert.AreEqual(controller.text.text, "3' 3\"");
        }

        [UnityTest]
        public IEnumerator LengthIconDisplaysCorrectDistanceInMetricUnitsTest()
        {

            controller.measurablePoint.state = MeasurablePoint.PointState.BRIDGE;
            UnitsToggleControllerMock.SetUnits(UnitsToggleController.Units.METRIC);
            SetLineLength(0.1234f);

            yield return null;
            Assert.IsTrue(controller.ImageGO.activeSelf == true);
            Assert.AreEqual(controller.text.text, "12 cm");
        }

        private void SetLineLength(float length)
        {
            measurablePoint.transform.position = new Vector3(0, 0, 0);
            measurablePoint.SetLineDestination(new Vector3(length, 0, 0)); //1 m
        }
    }

    public class UnitsToggleControllerMock : UnitsToggleController
    {
        public static void SetUnits(Units new_unit)
        {
            unit = new_unit;
        }
    }
}
