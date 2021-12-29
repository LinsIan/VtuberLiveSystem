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

        private CubismParameter[] parameters;

        public override IEnumerator Start()
        {
            yield return base.Start();
            var cubismModel = modelObj.GetComponent<CubismModel>();
            parameters = cubismModel.Parameters;
        }


        float speed = 10;
        public override void UpdateModel()
        {
            var par = parameters[0];
            par.Value += speed * Time.deltaTime;
            if (par.Value >= par.MaximumValue)
            {
                speed = -10;
            }
            else if (par.Value <= par.MinimumValue)
            {
                speed = 10;
            }
        }

        public void OnFaceModelDataOutput(FaceModelData data)
        {
            //Debug.Log(data.AngleX * 500);
            //parameters[((int)ParameterIndex.AngleX)].Value = data.AngleX * 300;
            
        }


    }

}

