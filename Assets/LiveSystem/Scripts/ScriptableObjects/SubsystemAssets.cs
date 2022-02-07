// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.Data;
using UnityEngine.AddressableAssets;

namespace LiveSystem
{
    [CreateAssetMenu(fileName = "SubsystemAssets", menuName = "ScriptableObjects/SubsystemAssets", order = 1)]
    public class SubsystemAssets : ScriptableObject
    {
        [SerializeField]
        private AssetReferenceGameObject live2DSolutionRef;

        [SerializeField]
        private AssetReferenceGameObject home3DSolutionRef;

        public AssetReferenceGameObject GetSolutionRef(ModelType modelType)
        {
            return modelType switch
            {
                ModelType.Live2D => live2DSolutionRef,
                ModelType.Home3D => home3DSolutionRef,
                _ => null,
            };
        }
    }
}
