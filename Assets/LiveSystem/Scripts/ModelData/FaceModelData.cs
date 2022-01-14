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
    [Serializable]
    public readonly struct FaceModelData
    {
        public float AngleX { get; }
        public float AngleY { get; }
        public float AngleZ { get; }
        public float EyeLOpen { get; }
        public float EyeROpen { get; }
        public float EyeBallX { get; }
        public float EyeBallY { get; }
        public float MouthOpenY { get; }
        public float BodyAngleX { get; }
        public float BodyAngleY { get; }
        public float BodyAngleZ { get; }

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

        public static FaceModelData Lerp(in FaceModelData a, in FaceModelData b, float t = 1)
        {
            var angleX = Mathf.Lerp(a.AngleX, b.AngleX, t);
            var angleY = Mathf.Lerp(a.AngleY, b.AngleY, t);
            var angleZ = Mathf.Lerp(a.AngleZ, b.AngleZ, t);
            var eyeLOpen = Mathf.Lerp(a.EyeLOpen, b.EyeLOpen, t);
            var eyeROpen = Mathf.Lerp(a.EyeROpen, b.EyeROpen, t);
            var eyeBallX = Mathf.Lerp(a.EyeBallX, b.EyeBallX, t);
            var eyeBallY = Mathf.Lerp(a.EyeBallY, b.EyeBallY, t);
            var mouthOpenY = Mathf.Lerp(a.MouthOpenY, b.MouthOpenY, t);
            var bodyAngleX = Mathf.Lerp(a.BodyAngleX, b.BodyAngleX, t);
            var bodyAngleY = Mathf.Lerp(a.BodyAngleY, b.BodyAngleY, t);
            var bodyAngleZ = Mathf.Lerp(a.BodyAngleZ, b.BodyAngleZ, t);

            //angleX = (float)Math.Round(angleX, 2);
            //angleY = (float)Math.Round(angleY, 2);
            //angleZ = (float)Math.Round(angleZ, 2);

            return new FaceModelData(angleX, angleY, angleZ, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }
    }

}