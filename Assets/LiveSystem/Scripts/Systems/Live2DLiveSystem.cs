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
    public class Live2DLiveSystem : LiveSystem
    {
        [SerializeField] private FaceLandmarkKeyPoints keyPoints;
        private Calculater calculater;

        protected override IEnumerator InitSubSystem()
        {
            var newModelController = new Live2DModelController(modelData);
            var graph = solution?.GetComponent<IrisTrackingGraph>();
            var faceModelCalculater = new FaceDataCalculater(keyPoints);

            graph.OnFaceLandmarksWithIrisOutput.AddListener(faceModelCalculater.OnDataOutput);
            faceModelCalculater.OnFaceModelDataOutput += newModelController.OnFaceModelDataOutput;
            modelController = newModelController;
            calculater = faceModelCalculater;

            yield return base.InitSubSystem();
        }
    }
}
