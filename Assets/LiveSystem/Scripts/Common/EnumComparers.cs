// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;

namespace LiveSystem
{
    public class ParamIdComparer : IEqualityComparer<ParamId>
    {
        private static readonly ParamIdComparer m_Instance = new ParamIdComparer();
        
        public static ParamIdComparer Instance { get { return m_Instance; } }

        public bool Equals(ParamId x, ParamId y)
        {
            return ((int)x) == ((int)y);
        }

        public int GetHashCode(ParamId obj)
        {
            return ((int)obj);
        }
    }

    public class DirectionComparer : IEqualityComparer<Direction>
    {
        private static readonly DirectionComparer m_Instance = new DirectionComparer();

        public static DirectionComparer Instance { get { return m_Instance; } }

        public bool Equals(Direction x, Direction y)
        {
            return ((int)x) == ((int)y);
        }

        public int GetHashCode(Direction obj)
        {
            return ((int)obj);
        }
    }
}