using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LiveSystem
{
    public class Interpolator<T> where T : struct
    {
        public float InterpolationFactor { get; private set; }

        private readonly int MaxArrayNum = 2;

        private Stopwatch stopwatch;
        private double[] lastUpdateTimes;
        private T[] lastData;
        private int newIndex;

        public Interpolator()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            lastUpdateTimes = new double[MaxArrayNum];
            lastData = new T[MaxArrayNum];
            newIndex = 0;
        }

        public void OnDataUpdate(T data)
        {
            newIndex = GetOldIndex();
            lastData[newIndex] = data;
            lastUpdateTimes[newIndex] = stopwatch.Elapsed.TotalSeconds;
        }

        public void Update()
        {
            var newTime = lastUpdateTimes[newIndex];
            var oldTime = lastUpdateTimes[GetOldIndex()];

            if (newTime != oldTime)
            {
                InterpolationFactor = (float)((stopwatch.Elapsed.TotalSeconds - newTime) / (newTime - oldTime));
            }
            else
            {
                InterpolationFactor = 1;
            }
        }

        private int GetOldIndex()
        {
            return (newIndex == 0 ? 1 : 0);
        }
    }
}

