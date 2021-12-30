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
        private enum ParameterIndex
        {
            AngleX = 0
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

            //TODO:平滑移動
            parameters[(int)ParameterIndex.AngleX].Value = currentData.AngleX * 400;

        }

        public void OnFaceModelDataOutput(FaceModelData data)
        {
            //parameters[((int)ParameterIndex.AngleX)].Value = data.AngleX * 300;
            lock(dataQue)
            {
                dataQue.Enqueue(data);
            }
        }


    }

}

