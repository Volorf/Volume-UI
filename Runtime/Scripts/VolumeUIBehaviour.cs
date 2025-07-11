using System;
using System.Collections;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public enum InteractionMode
    {
        Touch,
        Pointer
    }
    
    public class VolumeUIBehaviour : MonoBehaviour, IInteractable
    {
        public event Action OnPressed;
        public event Action OnReleased;
        
        [HideInInspector] public float pressFactor = 0f;
        [HideInInspector] public InteractionMode interactionMode = InteractionMode.Touch;
        [HideInInspector] public Vector3 startTapPosition;
        
        bool _cooledDown;
        float _cooldownTime = 0.25f;
        protected bool isPressed;
        float _startDot;

        void OnTriggerEnter(Collider other)
        {
            if (_cooledDown) return;
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                Pressed();
                startTapPosition = other.ClosestPoint(transform.position);
                Vector3 tapDirection = startTapPosition - transform.position;
                _startDot = Vector3.Dot(transform.forward, tapDirection);
                StopCoroutine(CoolDown());
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (!isPressed) return;
            
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                Vector3 tapPosition = other.ClosestPoint(transform.position);
                Vector3 tapDirection = tapPosition - transform.position;
                float dot = Vector3.Dot(transform.forward, tapDirection);
                pressFactor = isPressed ? 1f - Mathf.Clamp01(dot / _startDot) : 0f;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!isPressed) return;
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                Released();
            }
        }

        public virtual void Pressed()
        {
            isPressed = true;
            OnPressed?.Invoke();
        }

        public virtual void Released()
        {
            isPressed = false;
            OnReleased?.Invoke();
        }
        
        IEnumerator CoolDown()
        {
            _cooledDown = true;
            yield return new WaitForSeconds(_cooldownTime);
            _cooledDown = false;
        }
    }
}

