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
    [CreateAssetMenu(fileName = "ModelData", menuName = "ScriptableObjects/ModelData", order = 1)]
    public class ModelData : ScriptableObject
    {
        [field: SerializeField]
        public int ID { get; private set; }

        [field: SerializeField]
        public string Description { get; private set; }

        [field: SerializeField]
        public ModelType ModelType { get; private set; }

        [field: SerializeField]
        public int CurrentAsset { get; private set; } = 0;

        [field: SerializeField]
        public List<ModelAsset> Assets { get; private set; }

        [field: SerializeField]
        public List<Sensitivity> Sensitivities { get; private set; }

    }
}

