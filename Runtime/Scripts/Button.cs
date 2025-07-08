using UnityEngine;
using UnityEngine.Events;

namespace Volorf.VolumeUI
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider))]
    public class Button : VolumeUIBehaviour
    {
        [Tooltip("In millimeters")]
        [SerializeField] float _width = 120f;
        
        [Header("Animation")]
        [SerializeField] float _duration = 1.0f;
        [SerializeField] AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        [Header("Style")] 
        public Color bodyColor;
        public Color buttonDefaultColor;
        public Color buttonPressedColor;
        
        [Header("Elements")]
        [SerializeField] MeshRenderer _meshRenderer;

        public UnityEvent onTap;
        
        MaterialPropertyBlock _mpb;
        Coroutine _animateCoroutine;
        float _currentValue;
        bool _isOnOffAnimating;
    }
}

