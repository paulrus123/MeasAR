using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class PointAndLinePlacementUnitTest
    {
        PointAndLinePlacementController pointAndLinePlacementController;
        Button addPointButton;
        Button doneButton;
        RayCastControllerMock rayCastControllerMock;



        [SetUp]
        public void Setup()
        {
            GameObject pointAndLineGo = Object.Instantiate(new GameObject());
            GameObject addPointButtonGO = Object.Instantiate(new GameObject());
            GameObject doneButtonGO = Object.Instantiate(new GameObject());
            GameObject raycasterGo = Object.Instantiate(new GameObject());

            addPointButton = addPointButtonGO.AddComponent<Button>();
            doneButton = doneButtonGO.AddComponent<Button>();

            pointAndLinePlacementController = pointAndLineGo.AddComponent<PointAndLinePlacementController>();
            pointAndLinePlacementController.addPointButton = addPointButton;
            pointAndLinePlacementController.doneButton = doneButton;
            rayCastControllerMock = raycasterGo.AddComponent<RayCastControllerMock>();


            Assert.NotNull(pointAndLinePlacementController);
        }

        [Ignore("Ignored until can successfully mock ARReferencePointManager")]
        [UnityTest]
        public IEnumerator PointAndLinePlacementUnitTestWithEnumeratorPasses()
        {
            Assert.AreEqual(pointAndLinePlacementController.measureblePoints.Count, 0);
            rayCastControllerMock.SetIsRaycasting(true);
            rayCastControllerMock.SetPose(new Pose());


            addPointButton.onClick.Invoke();
            yield return null;
        }
    }
}
