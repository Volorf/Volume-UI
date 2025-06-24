using System.Collections;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public class VolumeUIBehaviour : MonoBehaviour, IInteractable
    {
        bool _cooledDown = false;
        float _cooldownTime = 0.25f;
        bool _isPressed = false;

        void OnTriggerEnter(Collider other)
        {
            if (_cooledDown) return;
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                _isPressed = true;
                Pressed();
                StopCoroutine(CoolDown());
            }
        }
        
        void OnTriggerExit(Collider other)
        {
            if (!_isPressed) return;
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                _isPressed = false;
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

