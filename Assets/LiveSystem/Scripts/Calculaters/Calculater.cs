// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections.Generic;
using Mediapipe;

namespace LiveSystem
{
    public abstract class Calculater
    {

        public virtual void OnLandmarkDataOutput(NormalizedLandmarkList data)
        {
        }

        public virtual void OnMultiLandmarkDataOutput(List<NormalizedLandmarkList> data)
        {
        }

    }
}
