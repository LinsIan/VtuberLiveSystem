// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LiveSystem.Data
{   
    [Serializable]
    public class ModelAsset
    {
        [field: SerializeField]
        public AssetReferenceGameObject PrefabRef { get; private set; }
        [field: SerializeField]
        public AssetReferenceSprite SpriteRef { get; private set; }
    }
}

