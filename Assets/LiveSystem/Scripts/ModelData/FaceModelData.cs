// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiveSystem.ModelData
{
    [System.Serializable]
    public struct FaceModelData
    {
        [Range(-30f, 30f)] public float AngleX;
        [Range(-30f, 30f)] public float AngleY;
        [Range(-30f, 30f)] public float AngleZ;
        [Range(0f, 2f)] public float EyeLOpen;
        [Range(0f, 2f)] public float EyeROpen;
        [Range(-1f, 1f)] public float EyeBallX;
        [Range(-1f, 1f)] public float EyeBallY;
        //眉毛綁表情，先不做
        [Range(0f, 1f)] public float MouthOpenY;
        [Range(-10f, 10f)] public float BodyAngleX;
        [Range(-10f, 10f)] public float BodyAngleY;
        [Range(-10f, 10f)] public float BodyAngleZ;
        //[Range(0f, 1f)] public float Breath; //應該是程式or動畫控制

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