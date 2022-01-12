// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Diagnostics;

namespace LiveSystem
{
    public class Interpolator<T> where T : struct
    {
        public float InterpolationFactor { get; private set; }

        private readonly int MaxArrayNum = 2;

        private Func<T, T, float, T> Lerp;
        private Stopwatch stopwatch;
        private double[] lastUpdateTimes;
        private T[] lastData;
        private int newIndex;

        public Interpolator(Func<T, T, float, T> lerp)
        {
            Lerp = lerp;
            stopwatch = new Stopwatch();
            stopwatch.Start();
            lastUpdateTimes = new double[MaxArrayNum];
            lastData = new T[MaxArrayNum];
            newIndex = 0;
        }

        public void OnDataUpdate(T data)
        {
            lock(lastData) lock(lastUpdateTimes)
                {
                    newIndex = GetOldIndex();
                    lastData[newIndex] = data;
                    lastUpdateTimes[newIndex] = stopwatch.Elapsed.TotalSeconds;
                }
        }

        public T GetCurrentData()
        {
            double newTime, oldTime;
            T newData, oldData;

            lock (lastData) lock (lastUpdateTimes)
                {
                    newTime = lastUpdateTimes[newIndex];
                    oldTime = lastUpdateTimes[GetOldIndex()];
                    newData = lastData[newIndex];
                    oldData = lastData[GetOldIndex()];
                }
                    
            if (newTime != oldTime)
            {
                InterpolationFactor = (float)((stopwatch.Elapsed.TotalSeconds - newTime) / (newTime - oldTime));
            }
            else
            {
                InterpolationFactor = 1;
            }

            return Lerp(oldData, newData, InterpolationFactor);
        }

        private int GetOldIndex()
        {
            return (newIndex == 0 ? 1 : 0);
        }
    }
}

