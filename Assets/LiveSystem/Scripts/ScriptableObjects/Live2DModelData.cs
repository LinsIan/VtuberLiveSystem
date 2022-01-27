// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.Data;

namespace LiveSystem
{
    [CreateAssetMenu(fileName = "Live2DModelData", menuName = "ScriptableObjects/Live2DModelData", order = 1)]
    public class Live2DModelData : ScriptableObject
    {
        [field: SerializeField]
        public List<ModelAsset> Assets { get; private set; }

        [field: SerializeField]
        public FaceSensitivity FaceSensitivity { get; private set; }

        [field: SerializeField]
        public int CurrentAsset { get; private set; } = 0;

        [field: SerializeField]
        public float BreathingRate { get; private set; } = 1;
    }
}

