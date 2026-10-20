using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private GameObject interactionDetectorPrefab;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movementInput;
    private string currentAnimationState;
    private Vector2 lastPosition;
    private float distanceMovedThisFrame;
    private Vector2 currentPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.Play("IdleFront");
        if (PlayerManager.instance.GetCurrentPlayerPosition() != null)
        {
            transform.position = PlayerManager.instance.GetCurrentPlayerPosition();
        }

        lastPosition = transform.position;
    }

    public void ChangeAnimationState(string newState, float tranitionDuration = 0.3f)
    {
        if (currentAnimationState == newState) return;
        animator.CrossFade(newState, tranitionDuration);
        currentAnimationState = newState;
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E)){
            interactionDetectorPrefab.GetComponent<InteractionDetector>().OnInteract();
            Debug.Log("Interact pressed");
        }
        if(PauseController.instance != null && PauseController.instance.getIsGamePaused())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        if(movementInput.x > 0){
            ChangeAnimationState("Right");
        } else if (movementInput.x < 0){
            ChangeAnimationState("Left");
        } else if (movementInput.y > 0){
            ChangeAnimationState("Backward");
        } else if (movementInput.y < 0){
            ChangeAnimationState("Forward");
        } else {
            if(currentAnimationState == "Right"){
                ChangeAnimationState("IdleRight");
            } else if (currentAnimationState == "Left"){
                ChangeAnimationState("IdleLeft");
            } else if (currentAnimationState == "Backward"){
                ChangeAnimationState("IdleBack");
            } else if (currentAnimationState == "Forward"){
                ChangeAnimationState("IdleFront");
            }
        }
       
        if (EncounterZoneManager.instance != null &&
            EncounterZoneManager.instance.GetIsPlayerInEncounterZone())
        {
            currentPosition = transform.position;
            distanceMovedThisFrame = Vector2.Distance(currentPosition, lastPosition);
            lastPosition = currentPosition;
            EncounterZoneManager.instance.AddPlayerDistanceMoved(distanceMovedThisFrame);
        }

       
        rb.linearVelocity = movementInput * movementSpeed;
        PlayerManager.instance.SetCurrentPlayerPosition(transform.position);
    }
}
