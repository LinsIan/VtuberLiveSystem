// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;
using Mediapipe.Unity;

namespace LiveSystem
{
    /// <summary>
    /// 策略、橋接模式，把LiveSystem與其他工具之間的互動方法寫在父類別，子類別負責實作方法
    /// </summary>
    public abstract class LiveSystem : MonoBehaviour
    {
        [SerializeField] protected Solution solution; //抓graph、操作solution
        [SerializeField] protected ModelController modelController;//2D、3D控制器
        [SerializeField] protected ModelData modelData;
        [SerializeField] protected LiveMode liveMode;

        protected virtual void Start()
        {
            
        }

        protected virtual void Pause()
        {
            solution.Pause();
        }

        protected virtual void Stop()
        {
        }

        protected virtual void Update()
        {
            modelController.UpdateModel();
        }

        public void SetData(ModelData newData)
        {
        }

        public void SetLiveMode(LiveMode mode)
        {
        }

        public void BuildSubSystem()
        {
            // SubSystemBuilder or 分開build?
            //solution
            //modelController
        }
    }
}