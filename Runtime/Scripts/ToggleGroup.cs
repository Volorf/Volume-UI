using System;
using System.Collections.Generic;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public class ToggleGroup : MonoBehaviour
    {
        [SerializeField] bool _allowSwitchOff;
        [SerializeField] List<Toggle> _toggles = new ();

        void Start()
        {
            SetGroupProcessing();
        }

        void SetGroupProcessing()
        {
            foreach (Toggle t in _toggles)
            {
                t.processInToggleGroup = ProcessToggle;
            }
        }

        public void RegisterToggle(Toggle toggle)
        {
            if (toggle != null && !_toggles.Contains(toggle))
            {
                toggle.processInToggleGroup = ProcessToggle;
                _toggles.Add(toggle);
            }
        }
        
        public void UnregisterToggle(Toggle toggle)
        {
            if (toggle != null && _toggles.Contains(toggle))
            {
                toggle.processInToggleGroup = null;
                _toggles.Remove(toggle);
            }
        }

        void ProcessToggle(Toggle toggle)
        {
            print("Processing toggle: " + toggle.name);
            foreach (Toggle t in _toggles)
            {
                if (t != toggle)
                {
                    t.IsOn(false, notify: false);
                    continue;
                }
                
                if (!_allowSwitchOff)
                {
                    if (!t.isOn)
                    {
                        t.IsOn(true, notify: true, processInGroup: false);
                    }
                }
            }
        }
    }
}

