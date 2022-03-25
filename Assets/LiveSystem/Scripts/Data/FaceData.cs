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
    public readonly struct FaceData
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

        public FaceData(float angleX, float angleY, float angleZ, float eyeLOpen, float eyeROpen, float eyeBallX, float eyeBallY,
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

        public static FaceData Lerp(in FaceData a, in FaceData b, float t = 1)
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

            return new FaceData(angleX, angleY, angleZ, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }

        public static FaceData Round(in FaceData data, int digit)
        {
            var angleX = (float)Math.Round(data.AngleX, digit);
            var angleY = (float)Math.Round(data.AngleY, digit);
            var angleZ = (float)Math.Round(data.AngleZ, digit);
            var eyeLOpen = (float)Math.Round(data.EyeLOpen, digit);
            var eyeROpen = (float)Math.Round(data.EyeROpen, digit);
            var eyeBallX = (float)Math.Round(data.EyeBallX, digit);
            var eyeBallY = (float)Math.Round(data.EyeBallY, digit);
            var mouthOpenY = (float)Math.Round(data.MouthOpenY, digit);
            var bodyAngleX = (float)Math.Round(data.BodyAngleX, digit);
            var bodyAngleY = (float)Math.Round(data.BodyAngleY, digit);
            var bodyAngleZ = (float)Math.Round(data.BodyAngleZ, digit);

            return new FaceData(angleX, angleY, angleZ, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }

        public static FaceData operator +(FaceData a, FaceData b)
            => new FaceData(
                a.AngleX + b.AngleX,
                a.AngleY + b.AngleY,
                a.AngleZ + b.AngleZ,
                a.EyeLOpen + b.EyeLOpen,
                a.EyeROpen + b.EyeROpen,
                a.EyeBallX + b.EyeBallX,
                a.EyeBallY + b.EyeBallY,
                a.MouthOpenY + b.MouthOpenY,
                a.BodyAngleX + b.BodyAngleX,
                a.BodyAngleY + b.BodyAngleZ,
                a.BodyAngleZ + b.BodyAngleZ
            );

        public static FaceData operator -(FaceData a, FaceData b)
            => new FaceData(
                a.AngleX - b.AngleX,
                a.AngleY - b.AngleY,
                a.AngleZ - b.AngleZ,
                a.EyeLOpen - b.EyeLOpen,
                a.EyeROpen - b.EyeROpen,
                a.EyeBallX - b.EyeBallX,
                a.EyeBallY - b.EyeBallY,
                a.MouthOpenY - b.MouthOpenY,
                a.BodyAngleX - b.BodyAngleX,
                a.BodyAngleY - b.BodyAngleZ,
                a.BodyAngleZ - b.BodyAngleZ
            );

        public static FaceData operator -(FaceData a)
            => new FaceData(
                -a.AngleX,
                -a.AngleY,
                -a.AngleZ,
                -a.EyeLOpen,
                -a.EyeROpen,
                -a.EyeBallX,
                -a.EyeBallY,
                -a.MouthOpenY,
                -a.BodyAngleX,
                -a.BodyAngleY,
                -a.BodyAngleZ
            );
    }
}