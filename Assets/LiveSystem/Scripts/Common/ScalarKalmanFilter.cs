using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LiveSystem
{

    public class ScalarKalmanFilter
    {
        private  const float DefaultQ = 0.00001f;
        private const float DefaultR = 0.01f;
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

        //TODO:是否要有is first判斷
        public Vector3 Filt(Vector3 lastMeasurement , float minDist)
        {
            var dist = Vector3.Distance(lastMeasurement, value);
            if (dist > minDist)
            {
                UnityEngine.Debug.Log(dist);
                Reset();
                value = lastMeasurement;
                //return value;
            }

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

    public class NoiseFilter
    {
        private const int maxHist = 10;
        private Vector3 ptLast;
        private float ptMaxTol;
        private Vector3[] hist = new Vector3[maxHist];
        private int histHead, histSize;

        public NoiseFilter(float maxTol = 0.0005f)
        {
            histHead = 0;
            histSize = 0;
            ptMaxTol = maxTol * maxTol;
            ptLast = Vector3.zero;
        }

        public Vector3 Update(Vector3 ptNew)
        {
            float dist = Vector3.Distance(ptLast, ptNew);
            
            if (dist > ptMaxTol)
            {
                Debug.Log(dist);
                histSize = 0;
                histHead = 0;
            }

            hist[histHead] = ptNew; 
            histHead = (histHead + 1) % maxHist;
            if (histSize < maxHist) histSize++;
            return getResult();
        }

        public Vector3 getResult()
        {
            Vector3 sum = Vector3.zero;
            for (int i = 0; i < histSize; i++)
            {
                sum += hist[i];
            }

            ptLast = sum / histSize;

            return ptLast;
        }

    }

}

