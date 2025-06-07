using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Volorf.VolumeUI
{
    // [RequireComponent(typeof(BoxCollider))]
    public class Toggle : MonoBehaviour
    {
        public bool isOn;
        
        [Header("Animation")]
        [SerializeField] float duration;
        [SerializeField] AnimationCurve curve;
        
        [Header("Elements")]
        [SerializeField] MeshRenderer _meshRenderer;
        
        [Header("Events")]
        public UnityEvent<bool> onValueChanged;
        
        MaterialPropertyBlock _mpb;
        Coroutine _animateCoroutine;
        float _currentValue;
        
        public Action<Toggle> processInToggleGroup; 

        void Init()
        {
            _mpb ??= new MaterialPropertyBlock();
        }

        void OnValidate()
        {   
            if (!enabled) return;
            Init();
            IsOn(isOn);
        }
        
        void Start()
        {
            _currentValue = isOn ? 1f : 0f;
            Init();
        }
        
        public void IsOn(bool value, bool notify = true, bool processInGroup = true)
        {
            if (value != isOn)
                isOn = value;
            
            
            if (_animateCoroutine != null)
            {
                StopCoroutine(_animateCoroutine);
            }

            _animateCoroutine = StartCoroutine(Animate(value ? 1f : 0f));

            if (notify)
                onValueChanged?.Invoke(value);
            
            if (processInGroup)
                processInToggleGroup?.Invoke(this);
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
                _mpb.SetFloat("_Value", _currentValue);
                _meshRenderer.SetPropertyBlock(_mpb);
                yield return null;
            }
        }
    }
}

