// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.ModelData;
using Live2D.Cubism.Core;

namespace LiveSystem
{
    public class Live2DModelController : ModelController
    {
        [SerializeField] Transform test;

        private CubismModel cubismModel;
        private CubismParameter[] parameters;
        private Queue<FaceModelData> dataQue = new Queue<FaceModelData>();
        private FaceModelData currentData;

        public override void Start()
        {
            base.Start();
            cubismModel = modelObj.GetComponent<CubismModel>();
            parameters = cubismModel.Parameters;

            foreach (var par in parameters)
            {
                Debug.Log(par.Id);
            }
        }

        public override void UpdateModel()
        {

            if (dataQue.Count == 0) return;

            lock (dataQue)
            {
                currentData = dataQue.Dequeue();
            }

            //TODO:平滑移動、套用敏感度數值
            parameters.FindById(FaceModelData.AngleXParamID).Value = currentData.AngleX;
            parameters.FindById(FaceModelData.AngleYParamID).Value = currentData.AngleY;
            parameters.FindById(FaceModelData.AngleZParamID).Value = currentData.AngleZ;

            parameters.FindById(FaceModelData.BodyAngleXParamID).Value = currentData.BodyAngleX;    
            parameters.FindById(FaceModelData.BodyAngleYParamID).Value = currentData.BodyAngleY;
            parameters.FindById(FaceModelData.BodyAngleZParamID).Value = currentData.BodyAngleZ;

            //test obj
            test.transform.rotation = Quaternion.Euler(currentData.AngleX, currentData.AngleY, currentData.AngleZ);
        }

        //call form thread
        public void OnFaceModelDataOutput(FaceModelData data)
        {
            lock(dataQue)
            {
                dataQue.Enqueue(data);
            }
        }

    }

}

