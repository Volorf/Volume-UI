using System;
using System.Collections;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public class VolumeUIBehaviour : MonoBehaviour, IInteractable
    {
        protected float pressFactor = 0f;
        
        bool _cooledDown = false;
        float _cooldownTime = 0.25f;
        protected bool isPressed = false;
        
        Vector3 _startTapPosition;
        float _startDot;

        void OnTriggerEnter(Collider other)
        {
            if (_cooledDown) return;
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                isPressed = true;
                _startTapPosition = other.transform.position;
                Vector3 tapDirection = _startTapPosition - transform.position;
                _startDot = Vector3.Dot(transform.forward, tapDirection);
                
                Pressed();
                StopCoroutine(CoolDown());
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (!isPressed) return;
            
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                Vector3 tapDirection = other.transform.position - transform.position;
                float dot = Vector3.Dot(transform.forward, tapDirection);
                pressFactor = isPressed ? 1f - Mathf.Clamp01(dot / _startDot) : 0f;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!isPressed) return;
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                isPressed = false;
                Released();
            }
        }

        public virtual void Pressed()
        {
        }

        public virtual void Released()
        {
            
        }
        
        IEnumerator CoolDown()
        {
            _cooledDown = true;
            yield return new WaitForSeconds(_cooldownTime);
            _cooledDown = false;
        }
    }
}

