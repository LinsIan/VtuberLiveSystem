// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using LiveSystem.Data;

namespace LiveSystem
{
    //runtime home 3d model
    public class Home3DModel : MonoBehaviour
    {
        [SerializeField] private BoneParameter neck;
        [SerializeField] private BoneParameter spine;
        [SerializeField] private BoneParameter leftEye;
        [SerializeField] private BoneParameter rightEye;

        private Dictionary<ParamId, BoneParameter> parameters;

        [SerializeField] private VRMBlendShapeProxy blendShapeProxy;

        private void Awake()
        {
            parameters = new Dictionary<ParamId, BoneParameter>(ParamIdComparer.Instance)
            {
                { ParamId.ParamNeck, neck },
                { ParamId.ParamSpine, spine },
                { ParamId.ParamLeftEye, leftEye },
                { ParamId.ParamRightEye, rightEye }
            };
        }

        public void SetBoneRotation(ParamId paramId, Vector3 rotation)
        {
            parameters[paramId].SetBoneRotation(rotation);
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