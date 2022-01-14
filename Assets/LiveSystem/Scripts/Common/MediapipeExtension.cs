using System;
using UnityEngine;
using Mediapipe;

namespace LiveSystem.ExtensionMethods
{
    public static class MediapipeExtensions
    {
        public static NormalizedLandmark Round(this NormalizedLandmark landmark, int digits)
        {
            landmark.X = (float)Math.Round(landmark.X, digits);
            landmark.Y = (float)Math.Round(landmark.Y, digits);
            landmark.Z = (float)Math.Round(landmark.Z, digits);
            return landmark;
        }
    }

}

