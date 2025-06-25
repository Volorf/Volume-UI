using UnityEngine;

namespace Volorf.VolumeUI
{
    public class MouseInput : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
                    {
                        Toggle toggle = interactable as Toggle;
                        if (toggle) toggle.interactionMode = InteractionMode.Pointer;
                        interactable.Pressed();
                    }
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
                    {
                        interactable.Released();
                    }
                }
            }
        }
    }
}

