using System;
using System.Collections;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public class VolumeUIBehaviour : MonoBehaviour, IInteractable
    {
        bool _cooledDown = false;
        float _cooldownTime = 0.25f;

        void OnTriggerEnter(Collider other)
        {
            if (_cooledDown) return;
            if (other.gameObject.TryGetComponent(out VolumeUIInteractor _))
            {
                Pressed();
                StopCoroutine(CoolDown());
            }
        }
        
        void OnTriggerExit(Collider other)
        {
            Released();
        }

        public virtual void Pressed()
        {
            print("Pressed on: " + gameObject.name);
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

