using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Volorf.VolumeUI.Demo
{
    public class Oracul : MonoBehaviour
    {
        public List<Toggle> toggles;
        void OnEnable()
        {
            foreach (Toggle toggle in toggles)
            {
                toggle.OnToggleChanged += UpdateToggles;
            }
        }
    
        void OnDisable()
        {
            foreach (Toggle toggle in toggles)
            {
                toggle.OnToggleChanged -= UpdateToggles;
            }
        }

        void UpdateToggles(Toggle t)
        {
            bool isOn = Random.Range(0f, 1f) > 0.5f;
        
            int numOn = toggles.Count(toggle => toggle.IsOn);
            if (numOn < 3) return;
        
            foreach (Toggle toggle in toggles)
            {
                if (t == toggle) continue;
            
                if (t.IsOn)
                {
                    isOn = !isOn;
                    toggle.SetState(!isOn, notify: false);
                }
            }
        }
    }
}

