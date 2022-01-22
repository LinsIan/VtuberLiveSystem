using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LiveSystem
{

    public class ScalarKalmanFilter
    {
        private const float DefaultQ = 0.0001f;
        private const float DefaultR = 0.002f;
        private const float DefaultP = 1;

        public Vector3 value;  // 系統的狀態量
        public float k; // 卡爾曼增益
        public float q; // 預測過程噪聲協方差
        public float r; // 測量過程噪聲協方差
        public float p; // 估計誤差協方差
 
        public ScalarKalmanFilter(float predict_q = DefaultQ, float measured_r = DefaultR)
        {
            value = Vector3.zero;
            q = predict_q;
            r = measured_r;
            p = DefaultP;
        }

        public Vector3 Filt(Vector3 lastMeasurement)
        {
            var predictValue = value;

            var pminus = p + q;

            k = pminus / (pminus +r);

            value = predictValue + k * (lastMeasurement - predictValue);

            p = (1 - k) *pminus;

            return value;
        }

        public void Reset()
        {
            p = DefaultP;
            value = Vector3.zero;
            k = 0;
        }
    }
}

