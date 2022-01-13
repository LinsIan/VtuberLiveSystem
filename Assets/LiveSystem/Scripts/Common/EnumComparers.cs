// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;

namespace LiveSystem
{
    public class Live2DParamIdComparer : IEqualityComparer<Live2DParamId>
    {
        public bool Equals(Live2DParamId x, Live2DParamId y)
        {
            return ((int)x) == ((int)y);
        }

        public int GetHashCode(Live2DParamId obj)
        {
            return ((int)obj);
        }
    }
}