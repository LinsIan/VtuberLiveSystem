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
        protected bool isPause = true;

        public ModelController(ModelData data)
        {
            modelData = data;
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

        protected void ApplySensitivity(ref float value, in float sensitivity)
        {
            value = (sensitivity * value) - ((sensitivity - 1) * SensitivityConstant);
        }
    }
}