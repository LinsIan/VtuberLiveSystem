// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Diagnostics;

namespace LiveSystem
{
    public class Interpolator<TData> where TData : struct
    {
        public delegate TData LerpMethod(in TData a, in TData b, float t);
        public float InterpolationFactor { get; private set; }
        public bool HasInputData { get; private set; }


        private readonly int MaxArrayNum = 2;
        private LerpMethod Lerp;
        private Stopwatch stopwatch;
        private double[] lastUpdateTimes;
        private TData[] lastData;
        private int newIndex;

        public Interpolator(LerpMethod lerp)
        {
            Lerp = lerp;
            stopwatch = new Stopwatch();
            stopwatch.Start();
            lastUpdateTimes = new double[MaxArrayNum];
            lastData = new TData[MaxArrayNum];
            newIndex = 0;
            HasInputData = false;
        }

        public void UpdateData(in TData data)
        {
            lock(lastData) lock(lastUpdateTimes)
                {
                    newIndex = GetOldIndex();
                    lastData[newIndex] = data;
                    lastUpdateTimes[newIndex] = stopwatch.Elapsed.TotalSeconds;
                    HasInputData = true;
                }
        }

        public TData GetCurrentData()
        {
            double newTime, oldTime;

            lock (lastData) lock (lastUpdateTimes)
                {
                    newTime = lastUpdateTimes[newIndex];
                    oldTime = lastUpdateTimes[GetOldIndex()];

                    if (newTime != oldTime)
                    {
                        InterpolationFactor = (float)((stopwatch.Elapsed.TotalSeconds - newTime) / (newTime - oldTime));
                    }
                    else
                    {
                        InterpolationFactor = 1;
                    }
                    return Lerp(lastData[GetOldIndex()], lastData[newIndex], InterpolationFactor);
                }
        }

        private int GetOldIndex()
        {
            return (newIndex == 0 ? 1 : 0);
        }
    }
}

