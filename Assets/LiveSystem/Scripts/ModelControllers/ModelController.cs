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

    /// <summary>
    /// 負責 AssetReference的載入、切換 & game object的操作
    /// </summary>
    public class ModelController
    {
        protected GameObject modelObj;
        protected AssetReferenceGameObject modelRef;

        public virtual void Start()
        {

        }

        public virtual void UpdateModel()
        {
            
        }

        public virtual void SetModel()
        {
            
        }

    }
}