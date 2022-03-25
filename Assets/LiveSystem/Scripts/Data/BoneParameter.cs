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
    public class BoneParameter
    {
        [field: SerializeField]
        public Transform Bone { get; private set; }

        [field: SerializeField]
        public Vector3 MaxRotation { get; private set; }

        [field: SerializeField]
        public Vector3 MinRoataion { get; private set; }

        public void SetBoneRotation(Vector3 rotation)
        {
            Bone.rotation = Quaternion.Euler(new Vector3(
                Mathf.Clamp(rotation.x, MinRoataion.x, MaxRotation.x),
                Mathf.Clamp(rotation.y, MinRoataion.y, MaxRotation.y),
                Mathf.Clamp(rotation.z, MinRoataion.z, MaxRotation.z)
            ));
        }
        
    }
}