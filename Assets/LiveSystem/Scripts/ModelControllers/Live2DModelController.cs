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
        protected CubismModel cubismModel;
        protected CubismHarmonicMotionController motionController;
        protected Dictionary<ParamId, CubismParameter> parameters;
        protected FaceData defaultFaceData;
        protected FaceData calibrationFaceData;
        protected Interpolator<FaceData> interpolator;
        
        public Live2DModelController(ModelData data) : base(data)
        {
        }

        public override IEnumerator Init()
        {
            yield return base.Init();
            interpolator = new Interpolator<FaceData>(FaceData.Lerp);
            cubismModel = modelObj.GetComponent<CubismModel>();
            motionController = modelObj.GetComponent<CubismHarmonicMotionController>();
            InitParameters();
            SetMotionRate();
            isPause = false;
            defaultFaceData = new FaceData(
                parameters[ParamId.ParamAngleX].DefaultValue,
                parameters[ParamId.ParamAngleY].DefaultValue,
                parameters[ParamId.ParamAngleZ].DefaultValue,
                parameters[ParamId.ParamEyeLOpen].DefaultValue,
                parameters[ParamId.ParamEyeROpen].DefaultValue,
                parameters[ParamId.ParamEyeBallX].DefaultValue, 
                parameters[ParamId.ParamEyeBallY].DefaultValue,
                parameters[ParamId.ParamMouthOpenY].DefaultValue,
                parameters[ParamId.ParamBodyAngleX].DefaultValue,
                parameters[ParamId.ParamBodyAngleY].DefaultValue,
                parameters[ParamId.ParamBodyAngleZ].DefaultValue
           );
        }

        public override void UpdateModel()
        {
            if (isPause || !interpolator.HasInputData) return;

            FaceData currentFaceData = interpolator.GetCurrentData();

            UpdateParamter(ParamId.ParamAngleX, currentFaceData.AngleX, calibrationFaceData.AngleX);
            UpdateParamter(ParamId.ParamAngleY, currentFaceData.AngleY, calibrationFaceData.AngleY);
            UpdateParamter(ParamId.ParamAngleZ, currentFaceData.AngleZ, calibrationFaceData.AngleZ);
            UpdateParamter(ParamId.ParamEyeLOpen, currentFaceData.EyeLOpen, calibrationFaceData.EyeLOpen);
            UpdateParamter(ParamId.ParamEyeROpen, currentFaceData.EyeROpen, calibrationFaceData.EyeROpen);
            UpdateParamter(ParamId.ParamEyeBallX, currentFaceData.EyeBallX);
            UpdateParamter(ParamId.ParamEyeBallY, currentFaceData.EyeBallY);
            UpdateParamter(ParamId.ParamMouthOpenY, currentFaceData.MouthOpenY);
            UpdateParamter(ParamId.ParamBodyAngleX, currentFaceData.BodyAngleX, calibrationFaceData.BodyAngleX);
            UpdateParamter(ParamId.ParamBodyAngleY, currentFaceData.BodyAngleY, calibrationFaceData.BodyAngleY);
            UpdateParamter(ParamId.ParamBodyAngleZ, currentFaceData.BodyAngleZ, calibrationFaceData.BodyAngleZ);

            foreach (var sensitivity in modelData.Sensitivities)
            {
                foreach (var id in sensitivity.EffectedParamIds)
                {
                    if (parameters.ContainsKey(id))
                    {
                        ApplySensitivity(id, ref parameters[id].Value, sensitivity.Value);
                    }
                }
            }

            cubismModel.ForceUpdateNow();
        }

        public override void CalibrateModel()
        {
            if (!interpolator.HasInputData || isPause) return;
            FaceData currentFaceData = interpolator.GetCurrentData();
            calibrationFaceData = defaultFaceData - currentFaceData;
        }

        public override void SetLiveMode(LiveMode newMode)
        {
            liveMode = LiveMode.FaceOnly;
        }

        //called from thread
        public void OnFaceDataOutput(FaceData data)
        {
            interpolator.UpdateData(data);
        }

        public void SetMotionRate()
        {
            for (int i = 0; i < modelData.MotionRates.Count; i++)
            {
                motionController.ChannelTimescales[i] = modelData.MotionRates[i].Value;
            }
        }

        protected void InitParameters()
        {
            parameters = new Dictionary<ParamId, CubismParameter>(ParamIdComparer.Instance);
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

        protected void UpdateParamter(ParamId id, float currentValue, float calibrationValue = 0)
        {
            parameters[id].Value = currentValue + calibrationValue;
        }
    }

}

