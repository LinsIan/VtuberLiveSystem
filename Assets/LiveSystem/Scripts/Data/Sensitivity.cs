// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using UnityEngine;

namespace LiveSystem.Data
{
    [Serializable]
    public class Sensitivity
    {
        public static readonly float Default = 1;
        public static readonly float Min = 0;
        public static readonly float Max = 2;

        [field: SerializeField]
        public ParamId Id { get; private set; }

        [SerializeField, Range(0, 2)]
        private float value;

        public float Value
        {
            get => value;
            set => this.value = Mathf.Clamp(value, Min, Max);
        }

        public Sensitivity()
        {
            Reset();
        }

        public void Reset()
        {
            Value = Default;
        }
    }
}
