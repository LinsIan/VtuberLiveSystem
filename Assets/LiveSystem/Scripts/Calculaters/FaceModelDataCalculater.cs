// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;
using LiveSystem.ExtensionMethods;
using LiveSystem.ModelData;

namespace LiveSystem
{
    public class FaceModelDataCalculater : Calculater<NormalizedLandmarkList>
    {
        public Action<FaceModelData> OnFaceModelDataOutput { get; set; }

        protected FaceLandmarkKeyPoints keyPoints;
        protected readonly int FaceMeshCount = 468;
        protected readonly int IrisCount = 5;
        protected readonly List<ScalarKalmanFilter> filters = new List<ScalarKalmanFilter>();

        public FaceModelDataCalculater(FaceLandmarkKeyPoints points)
        {
            keyPoints = points;
        }
        
        public override void OnDataOutput(NormalizedLandmarkList data)
        {
            if (data == null)
            {
                return;
            }

            OnFaceModelDataOutput?.Invoke(Calculate(data));
        }

        public override void OnMultiDataOutput(List<NormalizedLandmarkList> data)
        {

        }

        protected virtual FaceModelData Calculate(NormalizedLandmarkList data)
        {
            var landmark = data.Landmark;

            var leftEye = GetCentralPoint(keyPoints.LeftEyePoints, data);
            var rightEye = GetCentralPoint(keyPoints.RightEyePoints, data);
            var nose = landmark[keyPoints.NosePoint];

            var angle = GetFaceEulerAngles(landmark[keyPoints.FaceDirectionPoints[Direction.Mid]], landmark[keyPoints.FaceDirectionPoints[Direction.Left]], landmark[keyPoints.FaceDirectionPoints[Direction.Right]]);
            var eyeLOpen = 1f + landmark[keyPoints.LeftEyePoints[Direction.Up]].Y - landmark[keyPoints.LeftEyePoints[Direction.Down]].Y;
            var eyeROpen = 1f + landmark[keyPoints.RightEyePoints[Direction.Up]].Y - landmark[keyPoints.RightEyePoints[Direction.Down]].Y;
            var eyeBallX = landmark[keyPoints.LeftIrisPoint].X - leftEye.X;
            var eyeBallY = landmark[keyPoints.LeftIrisPoint].Y - leftEye.Y;
            var mouthOpenY = landmark[keyPoints.InnerLipsPoints[Direction.Up]].Y - landmark[keyPoints.InnerLipsPoints[Direction.Down]].Y;
            var bodyAngleX = angle.x / 3;
            var bodyAngleY = angle.y / 3;
            var bodyAngleZ = angle.z / 3;
            return new FaceModelData(angle.x, angle.y, angle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);

        }

        protected (float X, float Y, float Z) GetCentralPoint(Dictionary<Direction,int> points, NormalizedLandmarkList data)
        {
            var landmark = data.Landmark;
            var centralPoint = (X: 0f, Y: 0f, Z: 0f);

            foreach (KeyValuePair<Direction,int> point in points)
            {
				centralPoint.X += landmark[point.Value].X;
                centralPoint.Y += landmark[point.Value].Y;
                centralPoint.Z += landmark[point.Value].Z;
			}
            centralPoint.X /= points.Count;
            centralPoint.Y /= points.Count;
            centralPoint.Z /= points.Count;

            return centralPoint;
        }
       
        protected Vector3 GetFaceEulerAngles(NormalizedLandmark midPoint, NormalizedLandmark rightPoint, NormalizedLandmark leftPoint)
        {
            var mid = new Vector3(midPoint.X, midPoint.Y, midPoint.Z);
            var right = new Vector3(rightPoint.X, rightPoint.Y, rightPoint.Z);
            var left = new Vector3(leftPoint.X, leftPoint.Y, leftPoint.Z);

            //angle X&Y
            var faceDirection = mid - (right + left) / 2;
            var angle = Quaternion.FromToRotation(Vector3.back, faceDirection.normalized).eulerAngles;

            //angle Z
            var skewVector = left - right;
            skewVector.z = 0;
            angle.z = Quaternion.FromToRotation(Vector3.right, skewVector).eulerAngles.z;
            return angle;
        }

        protected void InitFilter()
        {

        }

        protected void FiltData()
        {
            

        }
    }
}