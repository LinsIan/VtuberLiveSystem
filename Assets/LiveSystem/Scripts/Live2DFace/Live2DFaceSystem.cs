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
