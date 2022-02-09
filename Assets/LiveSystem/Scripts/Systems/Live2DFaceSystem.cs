// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Mediapipe.Unity.IrisTracking;
using System.Linq;

namespace LiveSystem
{
    public class Live2DFaceSystem : LiveSystem
    {
        [SerializeField] private FaceLandmarkKeyPoints keyPoints;
        private IrisTrackingGraph graph;
        private FaceModelDataCalculater faceModelCalculater;

        protected override IEnumerator InitSubSystem()
        {
            var newModelController = new Live2DModelController(modelData);
            graph = solution?.GetComponent<IrisTrackingGraph>();
            faceModelCalculater = new Live2DFaceModelDataCalculater(keyPoints);

            graph.OnFaceLandmarksWithIrisOutput.AddListener(faceModelCalculater.OnDataOutput);
            faceModelCalculater.OnFaceModelDataOutput += newModelController.OnFaceModelDataOutput;
            modelController = newModelController;

            yield return base.InitSubSystem();
        }
    }
}
