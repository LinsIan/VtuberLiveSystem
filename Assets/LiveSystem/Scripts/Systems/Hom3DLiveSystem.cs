using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity.Holistic;

namespace LiveSystem
{
    public class Hom3DLiveSystem : LiveSystem
    {
        protected override IEnumerator InitSubSystem()
        {
            var newModelController = new Home3DModelController(modelData, LiveMode.FaceOnly);
            var graph = solution?.GetComponent<HolisticTrackingGraph>();
            var faceDataCalculater = new Home3DFaceDataCalculater(keyPoints);
            //lefthand、righthand
            //pose (world?)
            //leftiris、rightiris

            graph.OnFaceLandmarksOutput.AddListener(faceDataCalculater.OnLandmarkDataOutput);
            faceDataCalculater.OnFaceDataOutput += newModelController.OnFaceDataOutput;
            calculaters.Add(faceDataCalculater);
            modelController = newModelController;

            yield return base.InitSubSystem();
        }
    }
}