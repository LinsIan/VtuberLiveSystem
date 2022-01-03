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
        public (string Id, float Value) AngleX;
        public (string Id, float Value) AngleY;
        public (string Id, float Value) AngleZ;
        public (string Id, float Value) EyeLOpen;
        public (string Id, float Value) EyeROpen;
        public (string Id, float Value) EyeBallX;
        public (string Id, float Value) EyeBallY;
        public (string Id, float Value) MouthOpenY;
        public (string Id, float Value) BodyAngleX;
        public (string Id, float Value) BodyAngleY;
        public (string Id, float Value) BodyAngleZ;
        //public float Breath; //應該是程式or動畫控制

        //public FaceModelData(float angleX, float angleY, float angleZ, float eyeLOpen, float eyeROpen, float eyeBallX, float eyeBallY,
        //    float mouthOpenY, float bodyAngleX, float bodyAngleY, float bodyAngleZ)
        //{
        //    AngleX.Value = ("";
        //    AngleY.Value = angleY;
        //    AngleZ.Value = angleZ;
        //    EyeLOpen.Value = eyeLOpen;
        //    EyeROpen.Value = eyeROpen;
        //    EyeBallX.Value = eyeBallX;
        //    EyeBallY.Value = eyeBallY;
        //    MouthOpenY.Value = mouthOpenY;
        //    BodyAngleX.Value= bodyAngleX;
        //    BodyAngleY.Value = bodyAngleY;
        //    BodyAngleZ.Value = bodyAngleZ;
        //}
    }

}