using System.Collections;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public GameObject interactablePopupText;
    public GameObject savePointStatusPrefab;
    public float savePointStatusDuration = 2f;
    public float savePointTimer = 0f;
     private Coroutine hideStatusCoroutine;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        DataPersistenceManager.instance.SaveGame();
        savePointStatusPrefab.SetActive(true);
        hideStatusCoroutine = StartCoroutine(HideSaveStatus());
    }

    private IEnumerator HideSaveStatus()
    {
        yield return new WaitForSeconds(savePointStatusDuration);
        savePointStatusPrefab.SetActive(false);
        hideStatusCoroutine = null;
    }
    public void toggleInteractablePopup(bool isActive)
    {
        interactablePopupText.GetComponent<TMPro.TMP_Text>().text = "Press E to save your progress";
        interactablePopupText.SetActive(isActive);
    }
}
