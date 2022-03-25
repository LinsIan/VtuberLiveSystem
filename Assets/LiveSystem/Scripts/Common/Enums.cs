// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file


namespace LiveSystem
{
    public enum ModelType
    {
        Live2D = 0,
        Home3D
    }

    public enum LiveMode
    {
        FaceOnly = 0,
        UpperBody,
        Holistic
    }

    public enum ParamId
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
        ParamBodyAngleZ,
        ParamBreath,
        ParamNeck,
        ParamSpine,
        ParamRightEye,
        ParamLeftEye
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