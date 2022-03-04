using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

namespace LiveSystem
{
    public class Home3DModel : MonoBehaviour
    {
        [SerializeField] private Transform neck;
        [SerializeField] private Transform spine;
        [SerializeField] private VRMBlendShapeProxy blendShapeProxy;

        public void RotateNeck(Quaternion rotatoin)
        {
            neck.rotation = rotatoin;
        }

        public void RotateSpine(Quaternion rotation)
        {
            spine.rotation = rotation;
        }

        public void SetBlendShapeValue(BlendShapePreset blendShape, float value)
        {
            BlendShapeAvatar avatar = blendShapeProxy.BlendShapeAvatar;
            blendShapeProxy.ImmediatelySetValue(avatar.GetClip(blendShape).Key, value);
        }

        public void SetBlendShapeValueSmoothly(BlendShapePreset blendShape, float value)
        {
            //TODO:表情功能變化
        }
    }
}