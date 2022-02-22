// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;
using Mediapipe.Unity;

namespace LiveSystem
{
    public abstract class LiveSystem : MonoBehaviour
    {
        [SerializeField] protected Solution solution;
        [SerializeField] protected ModelData modelData;
        [SerializeField] protected FaceLandmarkKeyPoints keyPoints;
        protected ModelController modelController;
        protected List<Calculater> calculaters = new List<Calculater>();

        protected virtual IEnumerator Start()
        {
            yield return InitSubSystem();
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

        protected virtual IEnumerator InitSubSystem()
        {
            yield return modelController.Init();
        }

        public IEnumerator SetModelData(ModelData newData, Action callback = null)
        {
            yield return modelController.SetModelData(newData);
            callback?.Invoke();
        }

        public IEnumerator ChangeModel(int index, Action callback = null)
        {
            yield return modelController.ChangeModel(index);
            callback?.Invoke();
        }

        public void SetLiveMode(LiveMode mode)
        {
            modelController.SetLiveMode(mode);
        }   
    }
}