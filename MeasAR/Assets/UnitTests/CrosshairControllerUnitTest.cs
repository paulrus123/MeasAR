using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CrosshairControllerUnitTest
    {
        CrossHairController crossHairController;
        RayCastControllerMock rayCastControllerMock;

        [SetUp]
        public void Setup()
        {
            GameObject crossHairGo = Object.Instantiate(new GameObject());
            crossHairController = crossHairGo.AddComponent<CrossHairController>();

            GameObject raycasterGo = Object.Instantiate(new GameObject());
            rayCastControllerMock = raycasterGo.AddComponent<RayCastControllerMock>();
        }

        [UnityTest]
        public IEnumerator CrosshairControllerIsInstantiatedTest()
        {
            Assert.NotNull(crossHairController);
            yield return null;
        }

        [UnityTest]
        public IEnumerator CrossHairFollowsCameraTest()
        {
            rayCastControllerMock.SetIsRaycasting(false);
            yield return null;
            Assert.IsTrue(AreVectorsEqual(crossHairController.gameObject.transform.localPosition, crossHairController.cameraPositionOffset));
            Assert.IsTrue(AreVectorsEqual(crossHairController.gameObject.transform.localEulerAngles, crossHairController.cameraRotationOffset));
        }

        [UnityTest]
        public IEnumerator CrossHairFollowsTrackable()
        {
            Pose pose = new Pose();
            pose.position = new Vector3(1, 2, 3);
            pose.rotation.eulerAngles = new Vector3(4, 5, 6);
            rayCastControllerMock.SetIsRaycasting(true);
            rayCastControllerMock.SetPose(pose);
            yield return null;
            Assert.IsTrue(AreVectorsEqual(crossHairController.gameObject.transform.position, pose.position));
            Assert.IsTrue(AreVectorsEqual(crossHairController.gameObject.transform.eulerAngles, pose.rotation.eulerAngles));
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(crossHairController.gameObject);
            Object.Destroy(rayCastControllerMock.gameObject);
        }

        public bool AreVectorsEqual(Vector3 a, Vector3 b)
        {
            if (Vector3.Distance(a, b) < 0.001f)
                return true;
            else
                return false;
        }
    }

    public class RayCastControllerMock : RayCastController
    {
        public void SetIsRaycasting(bool value) { isRaycastingToTrackable = value; }
        public void SetPose(Pose pose) { hitPose = pose; }
        private void Start() { /*Do Nothing*/}
        private void Update() { /*Do Nothing*/}
    }
}
