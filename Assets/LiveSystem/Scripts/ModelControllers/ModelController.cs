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

    public class ModelController
    {
        protected ModelData modelData;
        protected GameObject modelObj;
        protected AssetReferenceGameObject modelRef;

        public ModelController(ModelData data)
        {
            modelData = data;
        }

        public virtual IEnumerator Init()
        {
            if (modelObj != null)
            {
                ReleaseModel();
            }
            yield return InstantiateModel();
        }

        public virtual void UpdateModel()
        {
        }

        public IEnumerator SetData(ModelData newData)
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

        protected float ApplySen(float value, float s)
        {
            return (s * value) - (0.5f * (s - 1));
        }
    }
}