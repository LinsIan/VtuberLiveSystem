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
            var landmark = data.Landmark;

            var leftEye = GetCentralPoint(LeftEyeKeyPointIds, data);
            var rightEye = GetCentralPoint(RightEyeKeyPointIds, data);
            var nose = landmark[NosePoint];

            var angle = GetFaceEulerAngles(landmark[FaceDirectionPointIds.mid], landmark[FaceDirectionPointIds.left], landmark[FaceDirectionPointIds.right]);
            var eyeLOpen = 1f + landmark[LeftEyePointIds.up].Y - landmark[LeftEyePointIds.down].Y;
            var eyeROpen = 1f + landmark[RightEyePointIds.up].Y - landmark[RightEyePointIds.down].Y;
            var eyeBallX = landmark[LeftPupilPoint].X - leftEye.X;
            var eyeBallY = landmark[LeftPupilPoint].Y - leftEye.Y;
            var mouthOpenY = landmark[InnerLipsPointIds.up].Y - landmark[InnerLipsPointIds.down].Y;
            var bodyAngleX = angle.x / 3;
            var bodyAngleY = angle.y / 3;
            var bodyAngleZ = angle.z / 3;
            return new FaceModelData(angle.x, angle.y, angle.z, eyeLOpen, eyeROpen, eyeBallX, eyeBallY, mouthOpenY, bodyAngleX, bodyAngleY, bodyAngleZ);
        }
    }

}

