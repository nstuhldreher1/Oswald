using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Image portraitImage;
    public GameObject interactablePopupText;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;


   public bool CanInteract(){
        return !isDialogueActive;
   }

   public void Interact(){
        if(dialogueData == null){
            return;
        }
        if(isDialogueActive){
            NextLine();
        } else {
            StartDialogue();
        }
   }

   public void toggleInteractablePopup(bool isActive){
        interactablePopupText.GetComponent<TMP_Text>().text = "Press E to talk with " + dialogueData.npcName;
        interactablePopupText.SetActive(isActive);
    }

   public void StartDialogue()
    {
        PauseController.instance.setIsGamePaused(true);
        isDialogueActive = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());

    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.npcName + ": " + dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else
        {
            dialogueIndex++;
            if (dialogueIndex < dialogueData.dialogueLines.Length)
            {
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    IEnumerator TypeLine(){
        isTyping = true;
        dialogueText.SetText(dialogueData.npcName + ": ");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        { 
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoprogessDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.instance.setIsGamePaused(false);
    }

}

