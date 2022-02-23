using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.Data;

namespace LiveSystem
{
    public class Home3DModelController : ModelController
    {
        protected Interpolator<FaceData> faceDataInterpolator;

        public Home3DModelController(ModelData data, LiveMode mode) : base(data, mode)
        {
        }

        public override IEnumerator Init()
        {
            yield return base.Init();
            faceDataInterpolator = new Interpolator<FaceData>(FaceData.Lerp);
            isPause = false;
        }

        public override void UpdateModel()
        {
            //face only
            //upper body
            //lower body
        }

        public override void SetLiveMode(LiveMode newMode)
        {
            liveMode = LiveMode.FaceOnly;
        }

        public override void CalibrateModel()
        {
        }

        //called from thread
        public void OnFaceDataOutput(FaceData data)
        {
            faceDataInterpolator.UpdateData(data);
        }

    }
}