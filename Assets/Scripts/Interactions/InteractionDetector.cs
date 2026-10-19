using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnInteract()
    {
        interactableInRange?.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IInteractable>(out IInteractable interactable) && interactable.CanInteract())
        {
            Debug.Log("Interactable in range: " + interactable);
            interactableInRange = interactable;
            interactableInRange?.toggleInteractablePopup(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange?.toggleInteractablePopup(false);
            interactableInRange = null;
        }
    }
}
