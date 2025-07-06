using System.Collections.Generic;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public class ToggleGroup : MonoBehaviour
    {
        public bool allowSwitchOff;
        [SerializeField] List<Toggle> _toggles = new ();
        Toggle _currentToggle;

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
        
        public void CollectAllTogglesInChildren()
        {
            _toggles.Clear();
            Toggle[] toggles = GetComponentsInChildren<Toggle>(true);
            foreach (Toggle toggle in toggles)
            {
                _toggles.Add(toggle);
                toggle.SetToggleGroup(this);
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
            _currentToggle = toggle;
            // print("Processing toggle: " + toggle.name);
            foreach (Toggle t in _toggles)
            {
                if (!allowSwitchOff && !toggle.IsOn)
                {
                    toggle.SetState(true, notify: true, processInGroup: false);
                    return;
                }
                
                if (t != toggle)
                {
                    t.SetState(false, notify: true, processInGroup: false);
                }
                
                
            }
        }

        public bool IsCurrent(Toggle toggle)
        {
            return _currentToggle == toggle;
        }
    }
}

