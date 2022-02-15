// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mediapipe;
using LiveSystem.Data;

namespace LiveSystem
{
    public class FaceModelDataCalculater : Calculater
    {
        public Action<FaceData> OnFaceModelDataOutput { get; set; }

        private FaceModelDataCalculater faceModelCalculater;
        protected FaceLandmarkKeyPoints keyPoints;
        protected readonly List<ScalarKalmanFilter> filters;
        protected readonly float landmarkScale = 100;
        protected readonly float BodyRate = 3;
        protected readonly float EyeOpenConstanst = 0.7f;
        protected readonly float WinkEyeDistance = 0.3f;
        protected readonly float MouthOpenConstanst = 0.4f;

        public FaceModelDataCalculater(FaceLandmarkKeyPoints points)
        {
            keyPoints = points;
            filters = new List<ScalarKalmanFilter>();
            //for (int i = 0; i < points.FaceDirectionPoints.Count; i++)
            for (int i = 0; i < points.AllPoints.Count; i++)
            {
                filters.Add(new ScalarKalmanFilter());
            }
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

        protected virtual FaceData Calculate(NormalizedLandmarkList data)
        {
            var landmark = data.Landmark;

            var leftEye = GetCenterPoint(keyPoints.LeftEyePoints, data);
            var rightEye = GetCenterPoint(keyPoints.RightEyePoints, data);
            var nose = landmark[keyPoints.NosePoint];

            var angle = GetFaceEulerAngles(landmark[keyPoints.FaceDirectionPoints[Direction.Mid]], landmark[keyPoints.FaceDirectionPoints[Direction.Left]], landmark[keyPoints.FaceDirectionPoints[Direction.Right]]);
            var eyeLOpen = 1f + landmark[keyPoints.LeftEyePoints[Direction.Up]].Y - landmark[keyPoints.LeftEyePoints[Direction.Down]].Y;
            var eyeROpen = 1f + landmark[keyPoints.RightEyePoints[Direction.Up]].Y - landmark[keyPoints.RightEyePoints[Direction.Down]].Y;
            var eyeBallX = landmark[keyPoints.LeftIrisPoint].X - leftEye.x;
            var eyeBallY = landmark[keyPoints.LeftIrisPoint].Y - leftEye.y;
            var mouthOpenY = landmark[keyPoints.InnerLipsPoints[Direction.Up]].Y - landmark[keyPoints.InnerLipsPoints[Direction.Down]].Y;
            var bodyAngleX = angle.x / 3;
            var bodyAngleY = angle.y / 3;
            var bodyAngleZ = angle.z / 3;
            return new FaceData(angle.x, angle.y, angle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }

        protected Vector3 GetCenterPoint(Dictionary<Direction,int> points, NormalizedLandmarkList data)
        {
            var landmarks = data.Landmark;
            var sum = points
                .Select(point => new Vector3(landmarks[point.Value].X, landmarks[point.Value].Y, landmarks[point.Value].Z))
                .Aggregate((result, point) => result + point);

            return sum / points.Count;
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

        protected void FiltData(NormalizedLandmarkList data)
        {
            var landmarks = data.Landmark;
            for (int i = 0; i < keyPoints.AllPoints.Count; i++)
            {
                var point = keyPoints.AllPoints[i];
                var landmark = landmarks[point];
                Vector3 filt = filters[i].Filt(new Vector3(landmark.X, landmark.Y, landmark.Z));
                landmark.X = filt.x;
                landmark.Y = filt.y;
                landmark.Z = filt.z;
            }

            //int i = 0;
            //foreach (var point in keyPoints.FaceDirectionPoints)
            //{
            //    var landmark = landmarks[point.Value];
            //    Vector3 filt = filters[i++].Filt(new Vector3(landmark.X, landmark.Y, landmark.Z));
            //    landmark.X = filt.x;
            //    landmark.Y = filt.y;
            //    landmark.Z = filt.z;
            //}

        }
    }
}