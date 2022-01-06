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
        public static readonly string AngleXParamID = "ParamAngleX";
        public static readonly string AngleYParamID = "ParamAngleY";
        public static readonly string AngleZParamID = "ParamAngleZ";
        public static readonly string EyeLOpenParamID = "ParamEyeLOpen";
        public static readonly string EyeROpenParamID = "ParamEyeROpen";
        public static readonly string EyeBallXParamID = "ParamEyeBallX";
        public static readonly string EyeBallYParamID = "ParamEyeBallY";
        public static readonly string MouthOpenYParamID = "ParamMouthOpenY";
        public static readonly string BodyAngleXParamID = "ParamBodyAngleX";
        public static readonly string BodyAngleYParamID = "ParamBodyAngleY";
        public static readonly string BodyAngleZParamID = "ParamBodyAngleZ";

        public readonly float AngleX;
        public readonly float AngleY;
        public readonly float AngleZ;
        public readonly float EyeLOpen;
        public readonly float EyeROpen;
        public readonly float EyeBallX;
        public readonly float EyeBallY;
        public readonly float MouthOpenY;
        public readonly float BodyAngleX;
        public readonly float BodyAngleY;
        public readonly float BodyAngleZ;
        //public float Breath; //應該是程式or動畫控制

        public FaceModelData(float angleX, float angleY, float angleZ, float eyeLOpen, float eyeROpen, float eyeBallX, float eyeBallY,
            float mouthOpenY, float bodyAngleX, float bodyAngleY, float bodyAngleZ)
        {
            AngleX = angleX;
            AngleY = angleY;
            AngleZ = angleZ;
            EyeLOpen = eyeLOpen;
            EyeROpen = eyeROpen;
            EyeBallX = eyeBallX;
            EyeBallY = eyeBallY;
            MouthOpenY = mouthOpenY;
            BodyAngleX = bodyAngleX;
            BodyAngleY = bodyAngleY;
            BodyAngleZ = bodyAngleZ;
        }
    }

}