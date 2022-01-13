// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.ModelData;
using Live2D.Cubism.Core;

namespace LiveSystem
{
    public class Live2DModelController : ModelController
    {
        //test
        [SerializeField] Transform test;

        private CubismModel cubismModel;
        private Dictionary<Live2DParamId, CubismParameter> parameters;
        private FaceModelData currentFaceData;
        private Interpolator<FaceModelData> interpolator;
        private bool isStartOutputData;

        public override void Start()
        {
            base.Start();
            interpolator = new Interpolator<FaceModelData>(FaceModelData.Lerp);
            cubismModel = modelObj.GetComponent<CubismModel>();
            parameters = new Dictionary<Live2DParamId, CubismParameter>(new Live2DParamIdComparer());
            var modelParamteters = cubismModel.Parameters;
            var values = (Live2DParamId[])Enum.GetValues(typeof(Live2DParamId));
            foreach (var item in values)
            {
                string id = Enum.GetName(typeof(Live2DParamId), item);
                parameters.Add(item, modelParamteters.FindById(id));
            }
            isStartOutputData = false;
        }   

        public override void UpdateModel()
        {
            if (!isStartOutputData) return;

            //TODO:套用敏感度數值
            currentFaceData = interpolator.GetCurrentData();

            parameters[Live2DParamId.ParamAngleX].Value = currentFaceData.AngleX;
            parameters[Live2DParamId.ParamAngleY].Value = currentFaceData.AngleY;
            parameters[Live2DParamId.ParamAngleZ].Value = currentFaceData.AngleZ;


            //Debug.Log(currentData.MouthOpenY);

            //parameters[Live2DParamId.ParamBodyAngleX].Value = currentData.BodyAngleX;
            //parameters[Live2DParamId.ParamBodyAngleY].Value = currentData.BodyAngleY;
            //parameters[Live2DParamId.ParamBodyAngleZ].Value = currentData.BodyAngleZ;

            //test obj
            test.transform.rotation = Quaternion.Euler(currentFaceData.AngleX, currentFaceData.AngleY, currentFaceData.AngleZ);
        }

        //called from thread
        public void OnFaceModelDataOutput(FaceModelData data)
        {
            isStartOutputData = true;
            interpolator.UpdateData(data);
        }
    }

}

