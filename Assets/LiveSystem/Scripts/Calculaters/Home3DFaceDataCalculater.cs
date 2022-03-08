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
    public class Home3DFaceDataCalculater : FaceDataCalculater
    {
        protected Vector2 leftIris = Vector2.zero;
        
        public Home3DFaceDataCalculater(FaceLandmarkKeyPoints points) : base(points)
        {
        }

        public void OnLeftIrisLandmarksOutput(NormalizedLandmarkList data)
        {
            if (data == null)
            {
                return;
            }
            var leftIrisLandmark = data.Landmark[keyPoints.LeftIrisPoint - keyPoints.FaceMeshCount];
            leftIris = new Vector2(leftIrisLandmark.X, leftIrisLandmark.Y);
        }

        public void OnRightIrisLandmarksOutput(NormalizedLandmarkList data)
        {
        }
        
        protected override FaceData Calculate(NormalizedLandmarkList data)
        {
            var landmarks = data.Landmark;
            var eyeLOpen = (landmarks[keyPoints.LeftEyePoints[Direction.Down]].Y - landmarks[keyPoints.LeftEyePoints[Direction.Up]].Y) * landmarkScale - EyeOpenConstanst;
            var eyeROpen = (landmarks[keyPoints.RightEyePoints[Direction.Down]].Y - landmarks[keyPoints.RightEyePoints[Direction.Up]].Y) * landmarkScale - EyeOpenConstanst;
            var mouthOpenY = (landmarks[keyPoints.InnerLipsPoints[Direction.Down]].Y - landmarks[keyPoints.InnerLipsPoints[Direction.Up]].Y) * landmarkScale - MouthOpenConstanst;
            
            if (eyeLOpen - eyeROpen <= WinkEyeDistance && eyeROpen - eyeLOpen <= WinkEyeDistance)
            {
                eyeROpen = eyeLOpen;
            }
            
            FiltData(data);

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

            /*
                body:
                    X軸左到右1 ~ 0
                    Z軸遠到近 0 ~ -1
                    Y軸 1/5的facez
                    基準點 (0.5, 0, -0.5)
             */

            var leftEye = GetCenterPoint(keyPoints.LeftEyePoints, data);
            var eyeBallX = (leftIris.x - leftEye.x) * -landmarkScale;
            var eyeBallY = (leftIris.y - leftEye.y) * -landmarkScale;

            var nose = landmarks[keyPoints.NosePoint];
            var noseDirectoin = new Vector3(nose.X - 0.5f, nose.Y, nose.Z + 0.5f);
            var bodyAngle = Quaternion.FromToRotation(Vector3.up, noseDirectoin).eulerAngles;

            var bodyAngleX = bodyAngle.x;
            var bodyAngleY = eulerAngle.y / BodyRate;
            var bodyAngleZ = bodyAngle.z;

            return new FaceData(eulerAngle.x, eulerAngle.z, eulerAngle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }
    }
}