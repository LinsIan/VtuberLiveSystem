// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

namespace LiveSystem
{
    public class Home3DModel : MonoBehaviour
    {
        [SerializeField] private Transform neck;
        [SerializeField] private Transform spine;
        [SerializeField] private Transform leftEye;
        [SerializeField] private Transform rightEye;
        [SerializeField] private VRMBlendShapeProxy blendShapeProxy;

        public void SetNeckRotation(Quaternion rotatoin)
        {
            neck.rotation = rotatoin;
        }

        public void SetSpineRotation(Quaternion rotation)
        {
            spine.rotation = rotation;
        }

        public void SetLeftEyeRotation(Quaternion rotation)
        {
            
        }

        public void SetRightEyeRotation(Quaternion rotation)
        {

        }

        public void SetBlendShapeValue(BlendShapePreset blendShape, float value)
        {
            BlendShapeAvatar avatar = blendShapeProxy.BlendShapeAvatar;
            blendShapeProxy.ImmediatelySetValue(avatar.GetClip(blendShape).Key, value);
        }

        public void SetBlendShapeValueSmoothly(BlendShapePreset blendShape, float value)
        {
            //TODO:表情功能變化
        }
    }
}