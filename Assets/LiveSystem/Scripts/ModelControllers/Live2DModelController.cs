// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.Data;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.HarmonicMotion;

namespace LiveSystem
{
    public class Live2DModelController : ModelController
    {
        private CubismModel cubismModel;
        private CubismHarmonicMotionController breathingController;
        private Dictionary<ParamId, CubismParameter> parameters;
        private FaceData currentFaceData;
        private Interpolator<FaceData> interpolator;
        private bool isStartOutputData;

        public Live2DModelController(ModelData data) : base(data)
        {
        }

        public override IEnumerator Init()
        {
            yield return base.Init();
            interpolator = new Interpolator<FaceData>(FaceData.Lerp);
            cubismModel = modelObj.GetComponent<CubismModel>();
            breathingController = modelObj.GetComponent<CubismHarmonicMotionController>();
            InitParameters();
            isStartOutputData = false;
        }   

        public override void UpdateModel()
        {
            UpdateFaceData();
            
        }

        //called from thread
        public void OnFaceModelDataOutput(FaceData data)
        {
            isStartOutputData = true;
            interpolator.UpdateData(data);
        }

        protected void SetBreathingRate(float rate)
        {
            breathingController.ChannelTimescales[0] = rate;
        }

        protected void InitParameters()
        {
            parameters = new Dictionary<ParamId, CubismParameter>(Live2DParamIdComparer.Instance);
            var modelParamteters = cubismModel.Parameters;
            var values = (ParamId[])Enum.GetValues(typeof(ParamId));
            foreach (ParamId item in values)
            {
                string id = Enum.GetName(typeof(ParamId), item);
                var param = modelParamteters.FindById(id);
                if (param != null)
                {
                    parameters.Add(item, modelParamteters.FindById(id));
                }
            }
        }

        

        protected void UpdateFaceData()
        {
            if (!isStartOutputData) return;

            currentFaceData = interpolator.GetCurrentData();

            parameters[ParamId.ParamAngleX].Value = currentFaceData.AngleX;
            parameters[ParamId.ParamAngleY].Value = currentFaceData.AngleY;
            parameters[ParamId.ParamAngleZ].Value = currentFaceData.AngleZ;

            parameters[ParamId.ParamBodyAngleX].Value = currentFaceData.BodyAngleX;
            parameters[ParamId.ParamBodyAngleY].Value = currentFaceData.BodyAngleY;
            parameters[ParamId.ParamBodyAngleZ].Value = currentFaceData.BodyAngleZ;

            parameters[ParamId.ParamEyeBallX].Value = currentFaceData.EyeBallX;
            parameters[ParamId.ParamEyeBallY].Value = currentFaceData.EyeBallY;

            parameters[ParamId.ParamEyeROpen].Value = currentFaceData.EyeROpen;
            parameters[ParamId.ParamEyeLOpen].Value = currentFaceData.EyeLOpen;

            parameters[ParamId.ParamMouthOpenY].Value = currentFaceData.MouthOpenY;

        }

    }

}

