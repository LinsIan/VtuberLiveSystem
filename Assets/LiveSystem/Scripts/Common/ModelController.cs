// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiveSystem
{

    public class ModelController : MonoBehaviour
    {
        [SerializeField] protected GameObject modelPrefab;

        public virtual IEnumerator Start()
        {
            //TODO:載入模型等操作

            yield return new WaitForEndOfFrame();
        }

        public virtual void UpdateModel()
        {
        }

        public virtual void SetModel()
        {
            //TODO:切換模型的功能
        }

    }
}