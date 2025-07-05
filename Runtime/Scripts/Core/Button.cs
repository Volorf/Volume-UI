using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Volorf.VolumeUI
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider))]
    public class Button : VolumeUIBehaviour
    {
        [Header("Animation")]
        [SerializeField] float _duration = 1.0f;
        [SerializeField] AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        [Header("Style")] 
        public Color bodyColor;
        public Color buttonColor;
    }
}

