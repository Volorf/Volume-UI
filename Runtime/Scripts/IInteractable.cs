using System;

interface IInteractable
{
    void Pressed();
    void Released();
    
    event Action OnPressed;
    event Action OnReleased;
}
