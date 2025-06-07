using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Volorf.VolumeUI
{
    public class ToggleGroup : MonoBehaviour
    {
        public bool allowSwitchOff;
        [SerializeField] List<Toggle> _toggles = new ();

        void Start()
        {
            SetGroupProcessing();
        }

        void SetGroupProcessing()
        {
            foreach (Toggle t in _toggles)
            {
                t.SetToggleGroup(this);
            }
        }

        public void RegisterToggle(Toggle toggle)
        {
            if (toggle != null && !_toggles.Contains(toggle))
            {
                toggle.SetToggleGroup(this);
                _toggles.Add(toggle);
            }
        }
        
        public void UnregisterToggle(Toggle toggle)
        {
            if (toggle != null && _toggles.Contains(toggle))
            {
                toggle.SetToggleGroup(null);
                _toggles.Remove(toggle);
            }
        }

        public void ProcessToggle(Toggle toggle)
        {
            print("Processing toggle: " + toggle.name);
            foreach (Toggle t in _toggles)
            {
                if (t != toggle)
                {
                    t.IsOn(false, notify: true, processInGroup: false);
                    continue;
                }
                
                if (!allowSwitchOff && !toggle.isOn)
                {
                    toggle.IsOn(true, notify: true, processInGroup: false);
                }
            }
        }
    }
}

