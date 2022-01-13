// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections.Generic;
using Mediapipe;
using LiveSystem.ModelData;

namespace LiveSystem
{
    public class Live2DFaceModelDataCalculater : FaceModelDataCalculater
    {
        protected override FaceModelData Calculate(NormalizedLandmarkList data)
        {
            var landmarks = data.Landmark;
            
            var leftEye = GetKeyPoint(new List<NormalizedLandmark> {
                landmarks[LeftEyePointIds.left],
                landmarks[LeftEyePointIds.right],
                landmarks[LeftEyePointIds.down],
                landmarks[LeftEyePointIds.up]
            });
            var rightEye = GetKeyPoint(new List<NormalizedLandmark> {
                landmarks[RightEyePointIds.left],
                landmarks[RightEyePointIds.right],
                landmarks[RightEyePointIds.down],
                landmarks[RightEyePointIds.up]
            });
            var nose = landmarks[NosePoint];

            var angle = GetFaceEulerAngles(landmarks[FaceDirectionPointIds.mid], landmarks[FaceDirectionPointIds.left], landmarks[FaceDirectionPointIds.right]);
            var eyeLOpen = 1f + landmarks[LeftEyePointIds.up].Y - landmarks[LeftEyePointIds.down].Y;
            var eyeROpen = 1f + landmarks[RightEyePointIds.up].Y - landmarks[RightEyePointIds.down].Y;
            var eyeBallX = landmarks[LeftPupilPoint].X - leftEye.X;
            var eyeBallY = landmarks[LeftPupilPoint].Y - leftEye.Y;
            var mouthOpenY = landmarks[InnerLipsPointIds.up].Y - landmarks[InnerLipsPointIds.down].Y;
            var bodyAngleX = angle.x / 3;
            var bodyAngleY = angle.y / 3;
            var bodyAngleZ = angle.z / 3;
            return new FaceModelData(angle.x, angle.y, angle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }
    }

}

