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
        RayCastControllerMock rayCastControllerMock;

        [SetUp]
        public void Setup()
        {
            GameObject pointAndLineGo = Object.Instantiate(new GameObject());
            GameObject raycasterGo = Object.Instantiate(new GameObject());

            pointAndLinePlacementController = pointAndLineGo.AddComponent<PointAndLinePlacementController>();
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

            yield return null;
        }
    }
}
