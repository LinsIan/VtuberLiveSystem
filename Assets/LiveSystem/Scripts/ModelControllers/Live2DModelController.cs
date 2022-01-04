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

        //TODO:改成用Parameters.FindById
        private enum ParameterIndex
        {
            AngleX = 0,
            AngleY = 1,
            AngleZ = 2
        }

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
            lock (dataQue)
            {
                if (dataQue.Count != 0)
                {
                    currentData = dataQue.Dequeue();
                }
            }

            //TODO:平滑移動、敏感度數值
            //parameters[(int)ParameterIndex.AngleX].Value = currentData.AngleX.Value * 400;
            test.transform.rotation = Quaternion.Euler(currentData.AngleX.Value, currentData.AngleY.Value, currentData.AngleZ.Value);


        }

        public void OnFaceModelDataOutput(FaceModelData data)
        {
            //call form thread
            lock(dataQue)
            {
                dataQue.Enqueue(data);
            }
        }

    }

}

