using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Volorf.VolumeUI
{
    // [RequireComponent(typeof(BoxCollider))]
    public class Toggle : VolumeUIBehaviour
    {
        [SerializeField] bool _isOn;
        
        [Header("Animation")]
        [SerializeField] float duration;
        [SerializeField] AnimationCurve curve;
        
        [Header("Style")] 
        public Color bodyColor;
        public Color onColor;
        public Color offColor;
        
        [Header("Elements")]
        [SerializeField] MeshRenderer _meshRenderer;
        
        [Header("Events")]
        public UnityEvent<bool> onValueChanged;
        
        MaterialPropertyBlock _mpb;
        Coroutine _animateCoroutine;
        float _currentValue;
        
        private ToggleGroup _toggleGroup;

        public bool IsOn
        {
            get => _isOn;
            set => SetState(value);
        }

        void Init()
        {
            _mpb ??= new MaterialPropertyBlock();
            SetColorsToMpb(_mpb);
        }
        
        void SetColorsToMpb(MaterialPropertyBlock mpb) 
        {
            mpb.SetColor("_Base", bodyColor);
            mpb.SetColor("_On", onColor);
            mpb.SetColor("_Off", offColor);
        }

        void OnValidate()
        {   
            if (!enabled) return;
            Init();
            IsOn = _isOn;
        }
        
        void Start()
        {
            _currentValue = _isOn ? 1f : 0f;
            Init();
        }

        public override void Pressed()
        {
            base.Pressed();
            SetState(!_isOn);
        }

        public void SetState(bool value, bool notify = true, bool processInGroup = true)
        {
            _isOn = value;
            
            if (_animateCoroutine != null)
            {
                StopCoroutine(_animateCoroutine);
            }
            
            if (Application.isPlaying) 
                _animateCoroutine = StartCoroutine(Animate(value ? 1f : 0f));
            else
                SetToggleValue(value ? 1f : 0f);

            if (notify)
                onValueChanged?.Invoke(value);

            if (_toggleGroup != null && processInGroup)
            {
                _toggleGroup.ProcessToggle(this);
            }
        }
        
        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggleGroup = toggleGroup;
        }
        
        IEnumerator Animate(float target)
        {
            float time = 0f;
            float capturedValue = _currentValue;
            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);
                t = curve.Evaluate(t);
                _currentValue = Mathf.Lerp(capturedValue, target, t);
                SetToggleValue(_currentValue);
                yield return null;
            }
        }
        
        void SetToggleValue(float value)
        {
            _mpb ??= new MaterialPropertyBlock();
            _mpb.SetFloat("_Value", value);
            SetColorsToMpb(_mpb);
            _meshRenderer.SetPropertyBlock(_mpb);
        }
    }
}

