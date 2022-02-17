// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using LiveSystem.Data;

namespace LiveSystem
{

    public abstract class ModelController
    {
        protected readonly float SensitivityConstant = 0.5f;
        protected ModelData modelData;
        protected GameObject modelObj;
        protected AssetReferenceGameObject modelRef;
        protected LiveMode liveMode;
        protected bool isPause = true;

        public ModelController(ModelData data, LiveMode mode = LiveMode.FaceOnly)
        {
            modelData = data;
            liveMode = mode;
        }

        public virtual IEnumerator Init()
        {
            isPause = true;
            if (modelObj != null)
            {
                ReleaseModel();
            }
            yield return InstantiateModel();
        }

        public virtual void UpdateModel()
        {
        }

        public virtual void CalibrateModel()
        {
        }

        public virtual void SetLiveMode(LiveMode newMode)
        {
        }

        public IEnumerator SetModelData(ModelData newData)
        {
            modelData = newData;
            yield return Init();
        }

        public IEnumerator ChangeModel(int index)
        {
            modelData.CurrentAsset = index;
            yield return Init();
        }

        protected IEnumerator InstantiateModel()
        {
            modelRef = modelData.Assets[modelData.CurrentAsset].PrefabRef;
            var handle = modelRef.InstantiateAsync();
            yield return handle;
            modelObj = handle.Result;
        }

        protected void ReleaseModel()
        {
            modelRef.ReleaseInstance(modelObj);
        }

        protected void ApplySensitivity(ParamId id, ref float value, float sensitivity)
        {
            switch (id)
            {
                case ParamId.ParamEyeLOpen:
                case ParamId.ParamEyeROpen:
                case ParamId.ParamMouthOpenY:
                    var constantRate = (sensitivity >= 1) ? (sensitivity - 1) : 0;
                    value = Mathf.Clamp01((sensitivity * value) - (constantRate * SensitivityConstant));
                    break;
                default:
                    value *= sensitivity;
                    break;
            }

            
        }
    }
}