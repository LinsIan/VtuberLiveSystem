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
        bool isFirst = true;

        public Live2DFaceModelDataCalculater(FaceLandmarkKeyPoints points) : base(points)
        {
        }

        protected override FaceModelData Calculate(NormalizedLandmarkList data)
        {
            var landmark = data.Landmark;

            if (isFirst)
            {
                foreach(var mark in landmark)
                {
                    filters.Add(new ScalarKalmanFilter());
                }
                isFirst = false;
            }

            //for (int i = 0; i < landmark.Count; i++)
            //{
            //    Vector3 point = new Vector3(landmark[i].X, landmark[i].Y, landmark[i].Z);
            //    var filt = filters[i].Filt(point);
            //    landmark[i].X = filt.x;
            //    landmark[i].Y = filt.y;
            //    landmark[i].Z = filt.z;
            //}

            var leftEye = GetCentralPoint(keyPoints.LeftEyePoints, data);
            var rightEye = GetCentralPoint(keyPoints.RightEyePoints, data);
            var nose = landmark[keyPoints.NosePoint];

            var eulerAngle = GetFaceEulerAngles(landmark[keyPoints.FaceDirectionPoints[Direction.Mid]], landmark[keyPoints.FaceDirectionPoints[Direction.Left]], landmark[keyPoints.FaceDirectionPoints[Direction.Right]]);

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

            var eyeLOpen = Mathf.Clamp01((landmark[keyPoints.LeftEyePoints[Direction.Down]].Y - landmark[keyPoints.LeftEyePoints[Direction.Up]].Y) * 100 - 0.75f);
            var eyeROpen = Mathf.Clamp01((landmark[keyPoints.RightEyePoints[Direction.Down]].Y - landmark[keyPoints.RightEyePoints[Direction.Up]].Y) * 100 - 0.75f);

            var eyeBallX = (landmark[keyPoints.LeftIrisPoint].X - leftEye.X) * -200;
            var eyeBallY = (landmark[keyPoints.LeftIrisPoint].Y - leftEye.Y) * -200;
            
            var mouthOpenY = (landmark[keyPoints.InnerLipsPoints[Direction.Down]].Y - landmark[keyPoints.InnerLipsPoints[Direction.Up]].Y) * 100 - 0.4f;

            var bodyAngleX = angle.x / 3;
            var bodyAngleY = angle.y / 3;
            var bodyAngleZ = angle.z / 3;

            return new FaceModelData(angle.x, angle.y, angle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }
    }

}

