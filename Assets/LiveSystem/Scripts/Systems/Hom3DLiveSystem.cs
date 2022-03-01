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
        //public BlendShapeKey key;

        public BlendShapeClip clip;

        //public BlendShapeBinding binding; 

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

            Debug.Log(clip.Values.Length);
            foreach (var item in clip.Values)
            {
                Debug.Log(item.ToString());
            }

            var key = clip.Key;
            Debug.Log(key.Name);


            yield return base.InitSubSystem();
        }
    }
}