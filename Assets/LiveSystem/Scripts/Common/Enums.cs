// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file


namespace LiveSystem
{

    [System.Serializable]
    public enum LiveMode
    {
        Live2DFace,
        Home3DFace,
        Home3DHolistic
    }

    [System.Serializable]
    public enum Live2DParamId
    {
        ParamAngleX = 0,
        ParamAngleY,
        ParamAngleZ,
        ParamEyeLOpen,
        ParamEyeROpen,
        ParamEyeBallX,
        ParamEyeBallY,
        ParamMouthOpenY,
        ParamBodyAngleX,
        ParamBodyAngleY,
        ParamBodyAngleZ
    }
}