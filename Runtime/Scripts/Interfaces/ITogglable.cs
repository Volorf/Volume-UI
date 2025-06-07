using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Volorf.VolumeUI
{
    public interface ITogglable
    {
        public void  IsOn(bool isOn);
        public event Action<(ITogglable, bool)> onToggleUpdated;
    }
}

