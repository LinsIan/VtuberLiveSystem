using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiveSystem
{
    public abstract class Calculater<T>
    {

        public virtual void OnDataOutput(T data)
        {
        }

        public virtual void OnMultiDataOutput(List<T> data)
        {
        }

    }
}
