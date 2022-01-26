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
        [SerializeField, Range(0,10)]
        private float breathingRate = 1;

        private CubismModel cubismModel;
        private CubismHarmonicMotionController breathingController;
        private Dictionary<Live2DParamId, CubismParameter> parameters;
        private FaceData currentFaceData;
        private Interpolator<FaceData> interpolator;
        private bool isStartOutputData;

        public override void Start()
        {
            base.Start();
            interpolator = new Interpolator<FaceData>(FaceData.Lerp);
            cubismModel = modelObj.GetComponent<CubismModel>();
            breathingController = modelObj.GetComponent<CubismHarmonicMotionController>();
            InitParameters();
            SetBreathingRate(breathingRate);
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

        public void SetBreathingRate(float rate)
        {
            breathingController.ChannelTimescales[0] = rate;
        }

        private void InitParameters()
        {
            parameters = new Dictionary<Live2DParamId, CubismParameter>(Live2DParamIdComparer.Instance);
            var modelParamteters = cubismModel.Parameters;
            var values = (Live2DParamId[])Enum.GetValues(typeof(Live2DParamId));
            foreach (var item in values)
            {
                string id = Enum.GetName(typeof(Live2DParamId), item);
                parameters.Add(item, modelParamteters.FindById(id));
            }
        }

        private void UpdateFaceData()
        {
            if (!isStartOutputData) return;

            currentFaceData = interpolator.GetCurrentData();

            parameters[Live2DParamId.ParamAngleX].Value = currentFaceData.AngleX;
            parameters[Live2DParamId.ParamAngleY].Value = currentFaceData.AngleY;
            parameters[Live2DParamId.ParamAngleZ].Value = currentFaceData.AngleZ;

            parameters[Live2DParamId.ParamBodyAngleX].Value = currentFaceData.BodyAngleX;
            parameters[Live2DParamId.ParamBodyAngleY].Value = currentFaceData.BodyAngleY;
            parameters[Live2DParamId.ParamBodyAngleZ].Value = currentFaceData.BodyAngleZ;

            parameters[Live2DParamId.ParamEyeBallX].Value = currentFaceData.EyeBallX;
            parameters[Live2DParamId.ParamEyeBallY].Value = currentFaceData.EyeBallY;

            parameters[Live2DParamId.ParamEyeROpen].Value = currentFaceData.EyeROpen;
            parameters[Live2DParamId.ParamEyeLOpen].Value = currentFaceData.EyeLOpen;

            parameters[Live2DParamId.ParamMouthOpenY].Value = currentFaceData.MouthOpenY;

        }

    }

}

