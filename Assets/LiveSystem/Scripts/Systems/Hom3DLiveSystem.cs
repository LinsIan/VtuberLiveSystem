// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity.Holistic;
using VRM;

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

            graph.OnFaceLandmarksOutput.AddListener(faceDataCalculater.OnLandmarksOutput);
            graph.OnLeftIrisLandmarksOutput.AddListener(faceDataCalculater.OnLeftIrisLandmarksOutput);
            graph.OnRightHandLandmarksOutput.AddListener(faceDataCalculater.OnRightIrisLandmarksOutput);
            faceDataCalculater.OnFaceDataOutput += newModelController.OnFaceDataOutput;
            calculaters.Add(faceDataCalculater);
            modelController = newModelController;
            
            yield return base.InitSubSystem();
        }
    }
}