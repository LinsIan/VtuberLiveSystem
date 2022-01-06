// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;
using LiveSystem.ModelData;

namespace LiveSystem
{
    public class FaceModelDataCalculater : Calculater<NormalizedLandmarkList>
    {
        public Action<FaceModelData> OnFaceModelDataOutput { get; set; }

        protected readonly List<int> FaceOvalPoints = new List<int> { 10, 338, 297, 332, 284, 251, 389, 356, 454, 323, 361, 288, 397, 365, 379, 378, 400, 377, 152, 148, 176, 149, 150, 136, 172, 58, 132, 93, 234, 127, 162, 21, 54, 103, 67, 109 };
        protected readonly List<int> LeftEyePoints = new List<int> { 33, 7, 163, 144, 145, 153, 154, 155, 133, 33, 246, 161, 160, 159, 158, 157, 173 };
        protected readonly List<int> LeftEyebrowPoints = new List<int> { 46, 53, 52, 65, 55, 70, 63, 105, 66, 107 };
        protected readonly List<int> RightEyePoints = new List<int> { 263, 249, 390, 373, 374, 380, 381, 382, 362, 466, 388, 387, 386, 385, 384, 398 };
        protected readonly List<int> RightEyebrowPoints = new List<int> { 276, 283, 282, 295, 285, 300, 293, 334, 296, 336 };
        protected readonly List<int> InnerLipsPoints = new List<int> { 78, 95, 88, 178, 87, 14, 317, 402, 318, 324, 308, 191, 80, 81, 82, 13, 312, 311, 310, 415 };
        protected readonly List<int> OuterLipsPoints = new List<int> { 61, 146, 91, 181, 84, 17, 314, 405, 321, 375, 291, 185, 40, 39, 37, 0, 267, 269, 270, 409 };

        protected readonly int FaceMeshCount = 468;
        protected readonly int IrisCount = 5;

        //Keypoints
        protected readonly List<int> FaceDirectionKeyPoints = new List<int> { 6, 127, 356 };
        protected readonly List<int> OuterLipsKeyPoints = new List<int> { 0, 17 };
        protected readonly List<int> InnerLipsKeyPoints = new List<int> { 13, 14 };
        protected readonly List<int> HorizonMouthKeyPoints = new List<int> { 61, 291 };
        protected readonly List<int> LeftEyeKeyPoints = new List<int> { 33, 133, 145, 159 };
        protected readonly List<int> RightEyeKeyPoints = new List<int> { 263, 362, 373, 386};
        protected readonly int NosePoint = 1;
        protected readonly int ChinPoint = 152;
        protected readonly int LeftPupilPoint = 468;
        protected readonly int RightPupilPoint = 473;
        

        public override void OnDataOutput(NormalizedLandmarkList data)
        {
            if (data == null)
            {
                return;
            }

            var landmarks = data.Landmark;
            var leftEye = GetKeyPoint(LeftEyeKeyPoints, data);
            var rightEye = GetKeyPoint(RightEyeKeyPoints, data);
            var nose = data.Landmark[NosePoint];

            var angle = GetFaceEulerAngles(landmarks[FaceDirectionKeyPoints[0]], landmarks[FaceDirectionKeyPoints[1]], landmarks[FaceDirectionKeyPoints[2]]);
            var eyeLOpen = 1f;
            var eyeROpen = 1f;
            var eyeBallX = 0f;
            var eyeBallY = 0f;
            var mouthOpenY = 0;//landmarks[InnerLipsKeyPoints[0]] - ;
            var bodyAngleX = angle.x / 2;
            var bodyAngleY = angle.y / 2;
            var bodyAngleZ = angle.z / 2;

            var modelData = new FaceModelData(angle.x,angle.y,angle.z,eyeLOpen,eyeROpen,eyeBallX,eyeBallY,mouthOpenY,bodyAngleX,bodyAngleY,bodyAngleZ);
            OnFaceModelDataOutput?.Invoke(modelData);
        }

        public override void OnMultiDataOutput(List<NormalizedLandmarkList> data)
        {

        }
        
        private (float X, float Y, float Z) GetKeyPoint(List<int> keypoints, NormalizedLandmarkList data)
        {
            var keypoint = (X: 0f, Y: 0f, Z: 0f);

            foreach (var point in keypoints)
            {
				keypoint.X += data.Landmark[point].X;
                keypoint.Y += data.Landmark[point].Y;
                keypoint.Z += data.Landmark[point].Z;
			}
            keypoint.X /= keypoints.Count;
            keypoint.Y /= keypoints.Count;
            keypoint.Z /= keypoints.Count;

            return keypoint;
        }

        private Vector3 GetFaceEulerAngles(NormalizedLandmark midPoint, NormalizedLandmark rightPoint, NormalizedLandmark leftPoint)
        {
            var mid = new Vector3(midPoint.X, midPoint.Y, midPoint.Z);
            var right = new Vector3(rightPoint.X, rightPoint.Y, rightPoint.Z);
            var left = new Vector3(leftPoint.X, leftPoint.Y, leftPoint.Z);

            //angle X&Y
            var faceDirection = mid - (right + left) / 2;
            var angle = Quaternion.FromToRotation(Vector3.forward, faceDirection.normalized).eulerAngles;

            //angle Z
            //angle.z = 0;
            var skewVector = left - right;
            skewVector.z = 0;
            angle.z = Quaternion.FromToRotation(Vector3.right, skewVector).eulerAngles.z;

            return angle;
        }
    }
}