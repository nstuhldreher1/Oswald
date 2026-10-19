using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Scriptable Objects/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f; // Time delay between each character
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoprogessDelay = 1.5f; // Delay before automatically progressing to the next line
}
