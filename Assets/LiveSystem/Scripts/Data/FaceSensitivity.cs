// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiveSystem.Data
{
    [Serializable]
    public class FaceSensitivity
    {
        public static readonly float DefaultSensitivity = 1;
        public static readonly float MinSensitivity = 0;
        public static readonly float MaxSensitivity = 2;

        [field: SerializeField] public float AngleX { get; set; }
        [field: SerializeField] public float AngleY { get; set; }
        [field: SerializeField] public float AngleZ { get; set; }
        [field: SerializeField] public float EyeOpen { get; set; }
        [field: SerializeField] public float EyeBall { get; set; }
        [field: SerializeField] public float MouthOpenY { get; set; }
        [field: SerializeField] public float BodyAngleX { get; set; }
        [field: SerializeField] public float BodyAngleY { get; set; }
        [field: SerializeField] public float BodyAngleZ { get; set; }

        public FaceSensitivity()
        {
            Reset();
        }

        public void Reset()
        {
            AngleX = DefaultSensitivity;
            AngleY = DefaultSensitivity;
            AngleZ = DefaultSensitivity;
            EyeOpen = DefaultSensitivity;
            EyeBall = DefaultSensitivity;
            MouthOpenY = DefaultSensitivity;
            BodyAngleX = DefaultSensitivity;
            BodyAngleY = DefaultSensitivity;
            BodyAngleZ = DefaultSensitivity;
        }
    }
}

