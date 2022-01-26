// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using UnityEngine;

namespace LiveSystem.Data
{
    [Serializable]
    public class LandmarkPoint
    {
        [field: SerializeField]
        public Direction Direction { get; private set; }

        [field: SerializeField]
        public int Index { get; private set; }
    }
}