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
    public abstract class LiveSystem : MonoBehaviour
    {
        [SerializeField] protected Solution solution; //抓graph、操作solution
        [SerializeField] protected ModelController modelController;//2D、3D控制器
        [SerializeField] protected LiveMode liveMode;

        protected virtual void Start()
        {
            //可能要考慮等solution、modelController都初始化完
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

        public void SetMode(LiveMode mode, ModelController controller)
        {
            liveMode = mode;
            modelController = controller;
        }
    }
}