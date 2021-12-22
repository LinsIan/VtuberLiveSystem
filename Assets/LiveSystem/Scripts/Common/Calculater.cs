// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

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
