// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using UnityEngine;
using System.Collections.Generic;
using System;
using Mediapipe;
using LiveSystem.ExtensionMethods;
using LiveSystem.ModelData;

namespace LiveSystem
{
    public class Live2DFaceModelDataCalculater : FaceModelDataCalculater
    {
        Vector3 angle = Vector3.zero;

        Vector3 filtX, no;

        protected override FaceModelData Calculate(NormalizedLandmarkList data)
        {
            var landmark = data.Landmark;

            var leftEye = GetCentralPoint(LeftEyeKeyPointIds, data);
            var rightEye = GetCentralPoint(RightEyeKeyPointIds, data);
            //Debug.Log(landmark[NosePoint].ToString());
            var nose = landmark[NosePoint].Round(Digits);

            Vector3 n = new Vector3(nose.X, nose.Y, nose.Z);
            var fx = filter.Filt(n);
            Debug.Log("(" + (filtX.x - fx.x) + "," + (filtX.y - fx.y) + "," + (filtX.z - fx.z) + ") , (" + (no.x - n.x) + "," + (no.y - n.y) + "," + (no.z - n.z) + ")");
            filtX = fx; no = n;

            var eulerAngle = GetFaceEulerAngles(landmark[FaceDirectionPointIds.mid].Round(Digits), landmark[FaceDirectionPointIds.left].Round(Digits), landmark[FaceDirectionPointIds.right].Round(Digits));
            //Debug.Log("EulerAngle : " + eulerAngle.x + " , " + eulerAngle.y + " , " + eulerAngle.z);
            if (eulerAngle.y > 180)
            {
                eulerAngle.y -= 360;
            }

            eulerAngle.x *= -1;
            if (eulerAngle.x < -180)
            {
                eulerAngle.x += 360;
            }

            eulerAngle.z *= -1;
            if (eulerAngle.z < -180)
            {
                eulerAngle.z += 360;
            }

            angle.x = eulerAngle.y;
            angle.y = eulerAngle.x;
            angle.z = eulerAngle.z;

            var eyeLOpen = Mathf.Clamp01((landmark[LeftEyePointIds.down].Round(Digits).Y - landmark[LeftEyePointIds.up].Round(Digits).Y) * 100 - 0.75f);
            var eyeROpen = Mathf.Clamp01((landmark[RightEyePointIds.down].Round(Digits).Y - landmark[RightEyePointIds.up].Round(Digits).Y) * 100 - 0.75f);

            var eyeBallX = (landmark[LeftPupilPoint].Round(Digits).X - leftEye.X) * -200;
            var eyeBallY = (landmark[LeftPupilPoint].Round(Digits).Y - leftEye.Y) * -200;
            
            var mouthOpenY = (landmark[InnerLipsPointIds.down].Round(Digits).Y - landmark[InnerLipsPointIds.up].Round(Digits).Y) * 100 - 0.4f;
            //Debug.Log(landmark[InnerLipsPointIds.up].Y + " , " + landmark[InnerLipsPointIds.down].Y);

            var bodyAngleX = angle.x / 2;
            var bodyAngleY = angle.y / 2;
            var bodyAngleZ = angle.z / 2;

            return new FaceModelData(angle.x, angle.y, angle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }
    }

}

