// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file


namespace LiveSystem
{
    public enum ModelType
    {
        Live2D,
        Home3D
    }

    public enum LiveMode
    {
        Live2DFace = 0,
        Home3DFace,
        Home3DHolistic
    }

    public enum ParamId
    {
        ParamAngleX = 0,
        ParamAngleY,
        ParamAngleZ,
        ParamEyeLOpen,
        ParamEyeROpen,
        ParamEyeOpen,
        ParamEyeBallX,
        ParamEyeBallY,
        ParamEyeBall,
        ParamMouthOpenY,
        ParamBodyAngleX,
        ParamBodyAngleY,
        ParamBodyAngleZ,
        ParamBreath
    }

    public enum Direction
    {
        Up = 0,
        Down,
        Left,
        Right,
        Mid
    }
}