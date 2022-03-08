// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using LiveSystem.Data;

namespace LiveSystem
{
    [CreateAssetMenu(fileName = "FaceLandmarkKeyPoints", menuName = "ScriptableObjects/FaceLandmarkKeyPoints", order = 1)]
    public class FaceLandmarkKeyPoints : ScriptableObject
    {
        //protected readonly List<int> FaceOvalPoints = new List<int> { 10, 338, 297, 332, 284, 251, 389, 356, 454, 323, 361, 288, 397, 365, 379, 378, 400, 377, 152, 148, 176, 149, 150, 136, 172, 58, 132, 93, 234, 127, 162, 21, 54, 103, 67, 109 };
        //protected readonly List<int> LeftEyePoints = new List<int> { 33, 7, 163, 144, 145, 153, 154, 155, 133, 33, 246, 161, 160, 159, 158, 157, 173 };
        //protected readonly List<int> LeftEyebrowPoints = new List<int> { 46, 53, 52, 65, 55, 70, 63, 105, 66, 107 };
        //protected readonly List<int> RightEyePoints = new List<int> { 263, 249, 390, 373, 374, 380, 381, 382, 362, 466, 388, 387, 386, 385, 384, 398 };
        //protected readonly List<int> RightEyebrowPoints = new List<int> { 276, 283, 282, 295, 285, 300, 293, 334, 296, 336 };
        //protected readonly List<int> InnerLipsPoints = new List<int> { 78, 95, 88, 178, 87, 14, 317, 402, 318, 324, 308, 191, 80, 81, 82, 13, 312, 311, 310, 415 };
        //protected readonly List<int> OuterLipsPoints = new List<int> { 61, 146, 91, 181, 84, 17, 314, 405, 321, 375, 291, 185, 40, 39, 37, 0, 267, 269, 270, 409 };

        //protected readonly (int mid, int left, int right) FaceDirectionPointIds = (6, 127, 356);
        //protected readonly (int up, int down) OuterLipsPointIds = (0, 17);
        //protected readonly (int up, int down) InnerLipsPointIds = (13, 14);
        //protected readonly (int left, int right) HorizonMouthPointIds = (61, 291);
        //protected readonly (int left, int right, int down, int up) LeftEyePointIds = (33, 133, 145, 159);
        //protected readonly (int left, int right, int down, int up) RightEyePointIds = (362, 263, 373, 386);

        //protected readonly int NosePoint = 1;
        //protected readonly int ChinPoint = 152;
        //protected readonly int LeftPupilPoint = 468;
        //protected readonly int RightPupilPoint = 473;

        public readonly int FaceMeshCount = 468;
        public readonly int IrisCount = 5;

        [SerializeField]
        private List<LandmarkPoint> faceDirectionPoints;

        [SerializeField]
        private List<LandmarkPoint> outerLipsPoints;

        [SerializeField]
        private List<LandmarkPoint> innerLipsPoints;

        [SerializeField]
        private List<LandmarkPoint> horizonMouthPoints;

        [SerializeField]
        private List<LandmarkPoint> leftEyePoints;

        [SerializeField]
        private List<LandmarkPoint> rightEyePoints;

        [field: SerializeField]
        public int NosePoint { get; private set; }

        [field: SerializeField]
        public int ChinPoint { get; private set; }

        [field: SerializeField]
        public int LeftIrisPoint { get; private set; }

        [field: SerializeField]
        public int RightIrisPoint { get; private set; }

        public Dictionary<Direction, int> FaceDirectionPoints { get; private set; }

        public Dictionary<Direction, int> OuterLipsPoints { get; private set; }

        public Dictionary<Direction, int> InnerLipsPoints { get; private set; }

        public Dictionary<Direction, int> HorizonMouthPoints { get; private set; }

        public Dictionary<Direction, int> LeftEyePoints { get; private set; }

        public Dictionary<Direction, int> RightEyePoints { get; private set; }

        public List<int> AllPoints { get; private set; }

        private void OnEnable()
        {
            FaceDirectionPoints = faceDirectionPoints.ToDictionary(point => point.Direction, point => point.Index);
            OuterLipsPoints = outerLipsPoints.ToDictionary(point => point.Direction, point => point.Index);
            InnerLipsPoints = innerLipsPoints.ToDictionary(point => point.Direction, point => point.Index);
            HorizonMouthPoints = horizonMouthPoints.ToDictionary(point => point.Direction, point => point.Index);
            LeftEyePoints = leftEyePoints.ToDictionary(point => point.Direction, point => point.Index);
            RightEyePoints = rightEyePoints.ToDictionary(point => point.Direction, point => point.Index);

            var points = new List<LandmarkPoint>();
            points.AddRange(faceDirectionPoints);
            points.AddRange(outerLipsPoints);
            points.AddRange(innerLipsPoints);
            points.AddRange(horizonMouthPoints);
            points.AddRange(leftEyePoints);
            points.AddRange(rightEyePoints);
            AllPoints = points.Select(point => point.Index).ToList();
            AllPoints.Add(NosePoint);
            AllPoints.Add(ChinPoint);
            AllPoints.Add(LeftIrisPoint);
            AllPoints.Add(RightIrisPoint);
        }
    }
}

