// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiveSystem.ModelData
{
    [System.Serializable]
    public struct FaceModelData
    {
        public readonly (string Id, float Value) AngleX;
        public readonly (string Id, float Value) AngleY;
        public readonly (string Id, float Value) AngleZ;
        public readonly (string Id, float Value) EyeLOpen;
        public readonly (string Id, float Value) EyeROpen;
        public readonly (string Id, float Value) EyeBallX;
        public readonly (string Id, float Value) EyeBallY;
        public readonly (string Id, float Value) MouthOpenY;
        public readonly (string Id, float Value) BodyAngleX;
        public readonly (string Id, float Value) BodyAngleY;
        public readonly (string Id, float Value) BodyAngleZ;
        //public float Breath; //應該是程式or動畫控制

        public FaceModelData(float angleX, float angleY, float angleZ, float eyeLOpen, float eyeROpen, float eyeBallX, float eyeBallY,
            float mouthOpenY, float bodyAngleX, float bodyAngleY, float bodyAngleZ)
        {
            AngleX = ("ParamAngleX", angleX);
            AngleY = ("ParamAngleY", angleY);
            AngleZ = ("ParamAngleZ", angleZ);
            EyeLOpen = ("ParamEyeLOpen", eyeLOpen);
            EyeROpen = ("ParamEyeROpen", eyeROpen);
            EyeBallX = ("ParamEyeBallX", eyeBallX);
            EyeBallY = ("ParamEyeBallY", eyeBallY);
            MouthOpenY = ("ParamMouthOpenY", mouthOpenY);
            BodyAngleX = ("ParamBodyAngleX", bodyAngleX);
            BodyAngleY = ("ParamBodyAngleY", bodyAngleY);
            BodyAngleZ = ("ParamBodyAngleZ", bodyAngleZ);
        }
    }

}