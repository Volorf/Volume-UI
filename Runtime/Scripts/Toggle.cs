using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace VolumeUI
{
    [ExecuteAlways]
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(BoxCollider))]
    public class Toggle : MonoBehaviour
    {
        public bool isOn;
        
        [Header("Animation")]
        public float duration;
        public AnimationCurve curve;
        
        [Header("Events")]
        public UnityEvent<bool> onValueChanged;

        MeshRenderer _meshRenderer;
        MaterialPropertyBlock _mpb;
        Coroutine _animateCoroutine;
        
        float _currentValue;

        void OnValidate()
        {   
            if (!enabled) return;
            _meshRenderer ??= GetComponent<MeshRenderer>();
            _mpb ??= new MaterialPropertyBlock();
            IsOn(isOn);
        }
        
        void IsOn(bool value)
        {
            if (_animateCoroutine != null)
            {
                StopCoroutine(_animateCoroutine);
            }

            _animateCoroutine = StartCoroutine(Animate(value ? 1f : 0f));
            onValueChanged?.Invoke(value);
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

