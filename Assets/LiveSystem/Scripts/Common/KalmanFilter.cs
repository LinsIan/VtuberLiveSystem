using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LiveSystem
{
    public class KalmanFilter
    {
        //TODO: 做一個純量卡爾曼濾波器，然後支援多個Vector，A和H都是1，然後把這個存起來．
        struct KalmanInfo
        {
            public float filterValue;  // 系統的狀態量
            public float gain;//卡爾曼增益
            public float A;  // x(n)=A*x(n-1)+u(n),u(n)~N(0,q)
            public float H;  // z(n)=H*x(n)+w(n),w(n)~N(0,r)
            public float q;  // 預測過程噪聲協方差
            public float r;  // 測量過程噪聲協方差
            public float p;  // 估計誤差協方差
        }

        private KalmanInfo kalmanInfo;

        public KalmanFilter(float predict_q, float newMeasured_q)
        {
            kalmanInfo = new KalmanInfo();
            kalmanInfo.filterValue = 0;//待測量的初始值，如有中值一般設成中值
            kalmanInfo.A = 1;
            kalmanInfo.H = 1;
            kalmanInfo.q = predict_q;//預測（過程）噪聲方差 影響收斂速率，可以根據實際需求給出
            kalmanInfo.r = newMeasured_q;//測量（觀測）噪聲方差 可以通過實驗手段獲得
            kalmanInfo.p = 2;//後驗狀態估計值誤差的方差的初始值（不要爲0問題不大）
        }

        public float Filt(float lastMeasurement)
        {
            //預測下一時刻的值
            float predictValue = kalmanInfo.A * kalmanInfo.filterValue;

            //求協方差
            kalmanInfo.p = kalmanInfo.A * kalmanInfo.A * kalmanInfo.p + kalmanInfo.q;

            //記錄上次實際座標的值
            float preValue = kalmanInfo.filterValue;

            //計算增益
            kalmanInfo.gain = kalmanInfo.p * kalmanInfo.H / (kalmanInfo.p * kalmanInfo.H * kalmanInfo.H + kalmanInfo.r);

            //修正結果，計算濾波值
            kalmanInfo.filterValue = predictValue + (lastMeasurement - predictValue) * kalmanInfo.gain;

            //更新估計誤差協方差
            kalmanInfo.p = (1 - kalmanInfo.gain * kalmanInfo.H) * kalmanInfo.p;

            return kalmanInfo.filterValue;
        }


    }


}

