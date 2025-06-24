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
        private bool _prevIsOn;

        public bool IsOn
        {
            get => _isOn;
            set => SetState(value);
        }

        void Init()
        {
            _mpb ??= new MaterialPropertyBlock();
            SetColorsToMpb(_mpb);
            _prevIsOn = _isOn;
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
            print(gameObject.name + ", isOS: " + _isOn);
        }

        public void SetState(bool value, bool notify = true, bool processInGroup = true)
        {
            _isOn = value;
            
            if (_animateCoroutine != null)
            {
                if (_isOn != _prevIsOn)
                    StopCoroutine(_animateCoroutine);
            }
            
            if (Application.isPlaying) 
                _animateCoroutine = StartCoroutine(Animate(value ? 1f : 0f, _prevIsOn == _isOn ? duration / 2f : 0f));
            else
                SetToggleValue(_isOn ? 1f : 0f);

            if (notify && _prevIsOn != _isOn)
                onValueChanged?.Invoke(_isOn);

            if (_toggleGroup != null && processInGroup)
            {
                _toggleGroup.ProcessToggle(this);
            }
            
            _prevIsOn = _isOn;
        }
        
        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggleGroup = toggleGroup;
        }
        
        IEnumerator Animate(float target, float delay = 0f)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            
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

