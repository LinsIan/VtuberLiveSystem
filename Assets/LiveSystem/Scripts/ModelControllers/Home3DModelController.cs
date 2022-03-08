// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.Data;
using VRM;

namespace LiveSystem
{
    public class Home3DModelController : ModelController
    {
        protected Interpolator<FaceData> faceDataInterpolator;
        protected Home3DModel home3DModel;

        public Home3DModelController(ModelData data, LiveMode mode) : base(data, mode)
        {
        }

        public override IEnumerator Init()
        {
            yield return base.Init();
            faceDataInterpolator = new Interpolator<FaceData>(FaceData.Lerp);
            home3DModel = modelObj.GetComponent<Home3DModel>();
            isPause = false;
        }

        public override void UpdateModel()
        {
            if (isPause || !faceDataInterpolator.HasInputData) return;

            var currentFaceData = faceDataInterpolator.GetCurrentData();
            var faceAngle = new Vector3(currentFaceData.AngleX, currentFaceData.AngleY, currentFaceData.AngleZ);
            var bodyAngle = new Vector3(currentFaceData.BodyAngleX, currentFaceData.BodyAngleY, currentFaceData.BodyAngleZ);
            home3DModel.SetNeckRotation(Quaternion.Euler(faceAngle));
            home3DModel.SetSpineRotation(Quaternion.Euler(bodyAngle));
            home3DModel.SetBlendShapeValue(BlendShapePreset.Blink_L, currentFaceData.EyeLOpen);
            home3DModel.SetBlendShapeValue(BlendShapePreset.Blink_R, currentFaceData.EyeROpen);
            //vrm的瞳孔是骨架

        }

        public override void SetLiveMode(LiveMode newMode)
        {
            liveMode = LiveMode.FaceOnly;
        }

        public override void CalibrateModel()
        {
        }

        //called from thread
        public void OnFaceDataOutput(FaceData data)
        {
            faceDataInterpolator.UpdateData(data);
        }

    }
}