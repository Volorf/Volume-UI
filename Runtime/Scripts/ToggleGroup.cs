using System;
using System.Collections.Generic;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public class ToggleGroup : MonoBehaviour
    {
        [SerializeField] bool _allowSwitchOff;
        [SerializeField] List<ITogglable> _toggles = new List<ITogglable>();

        void Start()
        {
            AddTogglables();
        }

        public void AddToggle(ITogglable togglable)
        {
            if (togglable == null || _toggles.Contains(togglable)) return;
            _toggles.Add(togglable);
        }

        void AddTogglables()
        {
            _toggles.Clear();
            _toggles.AddRange(GetComponentsInChildren<ITogglable>());
        }
        
        void ProcessToggle(ITogglable togglable, bool isOn)
        {
            if (togglable == null) return;

            if (!isOn && !_allowSwitchOff)
            {
                togglable.IsOn(true);
                return;
            }

            foreach (var toggle in _toggles)
            {
                if (toggle == togglable) continue;
                toggle.IsOn(false);
            }

            togglable.IsOn(isOn);
        }
    }
}

