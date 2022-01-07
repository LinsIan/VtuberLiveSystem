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
        [SerializeField] protected GameObject modelObj; //這邊先用在場景拉關係，之後改成controller生成

        public virtual void Start()
        {
            //TODO:載入模型、設置初始位置等操作
        }

        public virtual void UpdateModel()
        {

        }

        public virtual void SetModel()
        {
            //TODO:切換模型的功能，可能要coroutine(載入模型資料然後生成等等)
        }

    }
}