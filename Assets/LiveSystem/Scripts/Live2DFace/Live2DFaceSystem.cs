// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity.FaceMesh;

namespace LiveSystem
{
    public class Live2DFaceSystem : LiveSystem
    {
        private FaceMeshGraph graph;
        private FaceLandmarksCalculater landmarksCalculater;

        protected override void Start()
        {
            landmarksCalculater = new FaceLandmarksCalculater();
            graph = solution?.GetComponent<FaceMeshGraph>();
            graph.OnMultiFaceLandmarksOutput.AddListener(landmarksCalculater.OnMultiDataOutput);
        }


    }
}
