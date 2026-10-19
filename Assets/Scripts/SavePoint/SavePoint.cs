using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public GameObject interactablePopupText;
    public GameObject savePointStatusPrefab;
    public float savePointStatusDuration = 2f;
    public float savePointTimer = 0f;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        DataPersistenceManager.instance.SaveGame();
        savePointStatusPrefab.SetActive(true);
    }

    public void toggleInteractablePopup(bool isActive)
    {
        interactablePopupText.GetComponent<TMPro.TMP_Text>().text = "Press E to save your progress";
        interactablePopupText.SetActive(isActive);
    }

    public void Update()
    {
        savePointTimer += Time.deltaTime;
        if(savePointStatusPrefab.activeSelf && savePointTimer >= savePointStatusDuration)
        {
            savePointStatusPrefab.SetActive(false);
            savePointTimer = 0f;

            savePointTimer -= savePointStatusDuration; 
        }

    }
}
