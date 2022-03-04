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
    public class FaceDataCalculater : Calculater
    {
        public Action<FaceData> OnFaceDataOutput { get; set; }

        protected FaceLandmarkKeyPoints keyPoints;
        protected readonly List<ScalarKalmanFilter> filters;
        protected readonly float landmarkScale = 100;
        protected readonly float BodyRate = 3;
        protected readonly float EyeOpenConstanst = 0.7f;
        protected readonly float WinkEyeDistance = 0.3f;
        protected readonly float MouthOpenConstanst = 0.4f;

        public FaceDataCalculater(FaceLandmarkKeyPoints points)
        {
            keyPoints = points;
            filters = new List<ScalarKalmanFilter>();
            //for (int i = 0; i < points.FaceDirectionPoints.Count; i++)
            for (int i = 0; i < points.AllPoints.Count; i++)
            {
                filters.Add(new ScalarKalmanFilter());
            }
        }
        
        public override void OnLandmarksOutput(NormalizedLandmarkList data)
        {
            if (data == null)
            {
                return;
            }

            OnFaceDataOutput?.Invoke(Calculate(data));
        }

        public override void OnMultiLandmarksOutput(List<NormalizedLandmarkList> data)
        {
        }

        protected virtual FaceData Calculate(NormalizedLandmarkList data)
        {
            //if (isFirst)
            //{
            //    foreach (var mark in landmarks)
            //    {
            //        filters.Add(new ScalarKalmanFilter());
            //    }
            //    isFirst = false;
            //}

            //for (int i = 0; i < landmarks.Count; i++)
            //{
            //    Vector3 point = new Vector3(landmarks[i].X, landmarks[i].Y, landmarks[i].Z);
            //    var filt = filters[i].Filt(point);
            //    landmarks[i].X = filt.x;
            //    landmarks[i].Y = filt.y;
            //    landmarks[i].Z = filt.z;
            //}

            return new FaceData();
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
                if (point >= landmarks.Count)
                {
                    continue;
                }
                var landmark = landmarks[point];
                Vector3 filt = filters[i].Filt(new Vector3(landmark.X, landmark.Y, landmark.Z));
                landmark.X = filt.x;
                landmark.Y = filt.y;
                landmark.Z = filt.z;
            }
        }
    }
}