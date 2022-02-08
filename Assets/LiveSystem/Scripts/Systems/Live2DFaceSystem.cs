// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Mediapipe.Unity.IrisTracking;


namespace LiveSystem
{
    public class Live2DFaceSystem : LiveSystem
    {
        [SerializeField]
        private FaceLandmarkKeyPoints keyPoints;
        private IrisTrackingGraph graph;
        private FaceModelDataCalculater faceModelCalculater;

        protected override void Start()
        {
            //這段用一個builder？
            faceModelCalculater = new Live2DFaceModelDataCalculater(keyPoints);
            graph = solution?.GetComponent<IrisTrackingGraph>();
            graph.OnFaceLandmarksWithIrisOutput.AddListener(faceModelCalculater.OnDataOutput);

            //用強轉型給ModelController設delegate
            if (modelController is Live2DModelController controller)
            {
                faceModelCalculater.OnFaceModelDataOutput += controller.OnFaceModelDataOutput;
            }
            else
            {
                //轉型失敗
            }
        }
    }
}
