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
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //test

        private CubismModel cubismModel;
        private Dictionary<Live2DParamId, CubismParameter> parameters = new Dictionary<Live2DParamId, CubismParameter>(new Live2DParamIdComparer());
        private Queue<FaceModelData> dataQue = new Queue<FaceModelData>();
        private FaceModelData currentFaceData = default;

        public override void Start()
        {
            base.Start();
            cubismModel = modelObj.GetComponent<CubismModel>(); 
            var modelParamteters = cubismModel.Parameters;

            foreach (var live2DParamID in (Live2DParamId[])Enum.GetValues(typeof(Live2DParamId)))
            {
                string id = Enum.GetName(typeof(Live2DParamId), live2DParamID);
                parameters.Add(live2DParamID, modelParamteters.FindById(id));
            }

            sw.Reset();
            sw.Start();
        }

        public override void UpdateModel()
        {
            if (dataQue.Count > 0)
            {
                UpdateCurrentFaceData();
            }

            //TODO:平滑移動、套用敏感度數值
            //TODO:角度要變成能套在模型的數值，像AngleY要套在live2d的anglex
            //Debug.Log((currentData.AngleX, currentData.AngleY, currentData.AngleZ));
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
            lock(dataQue)
            {
                dataQue.Enqueue(data);
            }

            sw.Stop();
            Debug.Log(sw.Elapsed.TotalSeconds.ToString());
            sw.Reset();
            sw.Start();
        }

        private void UpdateCurrentFaceData()
        {
            FaceModelData newData;
            lock (dataQue)
            {
                newData = dataQue.Dequeue();
            }

            //Convers to Live2D Face Data

            currentFaceData = newData;
        }

    }

}

