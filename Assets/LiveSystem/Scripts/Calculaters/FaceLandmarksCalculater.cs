using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;

namespace LiveSystem
{
    public class FaceLandmarksCalculater : Calculater<NormalizedLandmarkList>
    {
        public override void OnDataOutput(NormalizedLandmarkList data)
        {
        }
    }
}