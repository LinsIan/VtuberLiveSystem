// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;
using Mediapipe.Unity;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

namespace LiveSystem
{

    public class LiveSystem : MonoBehaviour
    {
        [SerializeField] protected SubsystemAssets subsystemAssets;
        [SerializeField] protected FaceLandmarkKeyPoints keyPoints;
        protected Solution solution;
        protected ModelController modelController;
        protected ModelData modelData;
        protected LiveMode liveMode;
        protected List<Calculater> calculaters = new List<Calculater>();

        protected virtual void Start()
        {
        }

        protected virtual void Pause()
        {
            solution.Pause();
        }

        protected virtual void Stop()
        {
        }

        protected virtual void Update()
        {
            modelController.UpdateModel();
        }

        public void SetData(ModelData newData)
        {
        }

        public void SetLiveMode(LiveMode mode)
        {
            modelController.SetLiveMode(mode);
        }

        public void BuildSubSystem()
        {
              
            var handler = subsystemAssets.GetSolutionRef(modelData.ModelType).InstantiateAsync();

            switch (modelData.ModelType)
            {
                case ModelType.Live2D:
                    solution = handler.Result.GetComponent<Mediapipe.Unity.IrisTracking.IrisTrackingSolution>();
                    var faceModelCalculater = new Live2DFaceModelDataCalculater(keyPoints);
                    calculaters.Add(faceModelCalculater);
                    var graph = solution?.GetComponent<Mediapipe.Unity.IrisTracking.IrisTrackingGraph>();
                    graph.OnFaceLandmarksWithIrisOutput.AddListener(faceModelCalculater.OnDataOutput);
                    if (modelController is Live2DModelController controller)
                    {
                        faceModelCalculater.OnFaceModelDataOutput += controller.OnFaceModelDataOutput;
                    }
                    break;

                case ModelType.Home3D:
                    break;
            }
        }
    }
}