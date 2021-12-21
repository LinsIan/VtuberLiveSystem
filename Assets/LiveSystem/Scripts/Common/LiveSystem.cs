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
        [SerializeField] protected ModelController modelController;//2D、3D控制
        [SerializeField] protected LiveMode liveMode;

        //protected const int LandmarkCount = 468;
        

        virtual protected void Start()
        {
            
        }

        virtual protected void Pause()
        {
            solution.Pause();
        }

        virtual protected void Stop()
        {

        }

    }
}