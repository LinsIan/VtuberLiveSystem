// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using Mediapipe;
using LiveSystem.Data;

namespace LiveSystem
{
    public class Live2DFaceModelDataCalculater : FaceModelDataCalculater
    {
        public Live2DFaceModelDataCalculater(FaceLandmarkKeyPoints points) : base(points)
        {
        }

        protected override FaceData Calculate(NormalizedLandmarkList data)
        {
            var landmarks = data.Landmark;

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

            var eyeLOpen = (landmarks[keyPoints.LeftEyePoints[Direction.Down]].Y - landmarks[keyPoints.LeftEyePoints[Direction.Up]].Y) * landmarkScale - EyeOpenConstanst;
            var eyeROpen = (landmarks[keyPoints.RightEyePoints[Direction.Down]].Y - landmarks[keyPoints.RightEyePoints[Direction.Up]].Y) * landmarkScale - EyeOpenConstanst;

            if (eyeLOpen - eyeROpen <= WinkEyeDistance && eyeROpen - eyeLOpen <= WinkEyeDistance)
            {
                eyeROpen = eyeLOpen;
            }
            var mouthOpenY = (landmarks[keyPoints.InnerLipsPoints[Direction.Down]].Y - landmarks[keyPoints.InnerLipsPoints[Direction.Up]].Y) * landmarkScale - MouthOpenConstanst;

            FiltData(data);
                
            //rightEye = GetCenterPoint(keyPoints.RightEyePoints, data);
            //var nose = landmarks[keyPoints.NosePoint];
            var eulerAngle = GetFaceEulerAngles(landmarks[keyPoints.FaceDirectionPoints[Direction.Mid]], landmarks[keyPoints.FaceDirectionPoints[Direction.Left]], landmarks[keyPoints.FaceDirectionPoints[Direction.Right]]);

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

            var leftEye = GetCenterPoint(keyPoints.LeftEyePoints, data);
            var eyeBallX = (landmarks[keyPoints.LeftIrisPoint].X - leftEye.x) * -landmarkScale;
            var eyeBallY = (landmarks[keyPoints.LeftIrisPoint].Y - leftEye.y) * -landmarkScale;
            var bodyAngleX = eulerAngle.x / BodyRate;
            var bodyAngleY = eulerAngle.y / BodyRate;
            var bodyAngleZ = eulerAngle.z / BodyRate;

            return new FaceData(eulerAngle.y, eulerAngle.x, eulerAngle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }
    }

}   

