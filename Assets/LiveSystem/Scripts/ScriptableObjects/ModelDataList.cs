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
    [CreateAssetMenu(fileName = "FaceLandmarkKeyPoints", menuName = "ScriptableObjects/ModelDataList", order = 1)]
    public class ModelDataList : ScriptableObject
    {
        [field: SerializeField]
        public List<ModelData> Models { get; private set; }
    }
}

