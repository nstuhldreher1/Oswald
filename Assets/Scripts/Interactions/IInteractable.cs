using UnityEngine;

public interface IInteractable
{
    void Interact();
    bool CanInteract();

    void toggleInteractablePopup(bool isActive);
}
