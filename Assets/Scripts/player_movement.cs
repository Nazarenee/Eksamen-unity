using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Required for Input System


public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isAttacking = false;
    private bool isSprinting = false;
    public AudioSource audioSource; 
    public AudioClip bowDrawClip; 
    public AudioClip bowReleaseClip; 


    public Animator playerAnimator;
    public Rigidbody playerRigidbody;
    public float walkSpeed = 5f, walkBackwardSpeed = 2f, defaultWalkSpeed = 5f, rotationSpeed = 300f; 
    public Transform playerTransform;

    private void Awake()
    {
        controls = new PlayerControls();

        // Bind input actions for movement
        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        // Bind sprint action (Left Shift)
        controls.Player.Sprint.performed += ctx => isSprinting = true;
        controls.Player.Sprint.canceled += ctx => isSprinting = false;

        // Bind attack action (Left Mouse Button)
        controls.Player.Attack.performed += ctx => Attack(); 
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
      
        if (isAttacking)
        {
            playerRigidbody.linearVelocity = Vector3.zero; 
            return; 
        }

        Vector3 forward = playerTransform.forward;
        forward.y = 0; 

        Vector3 right = playerTransform.right;
        right.y = 0; 

        Vector3 movement = Vector3.zero;

        float currentSpeed = isSprinting ? walkSpeed * 2f : walkSpeed;

        if (moveInput.y != 0)
        {
            movement += forward * moveInput.y * currentSpeed;
        }

        if (moveInput.x != 0)
        {
            playerTransform.Rotate(0, moveInput.x * rotationSpeed * Time.deltaTime, 0);
        }

        playerRigidbody.linearVelocity = new Vector3(movement.x, playerRigidbody.linearVelocity.y, movement.z);
    }

    private void Update()
    {
        if (isAttacking) return;

        if (moveInput.y < 0)
        {
            playerAnimator.SetTrigger("walkback");
            playerAnimator.ResetTrigger("idle");
        }
        else if (moveInput.y > 0)
        {
            playerAnimator.SetTrigger("walk");
            playerAnimator.ResetTrigger("idle");
        }
        else
        {
            playerAnimator.ResetTrigger("walk");
            playerAnimator.ResetTrigger("walkback");
            playerAnimator.SetTrigger("idle");
        }

        if (isSprinting)
        {
            playerAnimator.SetTrigger("run");
            playerAnimator.ResetTrigger("walk");
            playerAnimator.ResetTrigger("walkback");
            playerAnimator.ResetTrigger("idle");
        }
        else if (!isSprinting && moveInput.y > 0)
        {
            playerAnimator.SetTrigger("walk");
            playerAnimator.ResetTrigger("run");
            playerAnimator.ResetTrigger("walkback");
            playerAnimator.ResetTrigger("idle");
        }
    }

    private void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;
        Vector2 tempMoveInput = moveInput; 
        moveInput = Vector2.zero; 
        playerRigidbody.linearVelocity = Vector3.zero;
        
        audioSource.PlayOneShot(bowDrawClip);

        playerAnimator.ResetTrigger("walk");
        playerAnimator.ResetTrigger("walkback");
        playerAnimator.ResetTrigger("run");

        if (gameObject.CompareTag("Warrior"))
        {
            playerAnimator.SetTrigger("melee");
        }
        else if (gameObject.CompareTag("Hunter"))
        {
            playerAnimator.SetTrigger("arrow"); 
        }
        else
        {
            playerAnimator.SetTrigger("magic");
        }
        StartCoroutine(PlayBowReleaseSound());

        StartCoroutine(ResetAttack(tempMoveInput));
    }
    
    IEnumerator PlayBowReleaseSound()
    {
        yield return new WaitForSeconds(0.5f); 
        audioSource.PlayOneShot(bowReleaseClip); 
    }

    IEnumerator ResetAttack(Vector2 savedMoveInput)
    {
        yield return new WaitForSeconds(1f); 
        isAttacking = false;

        playerAnimator.SetTrigger("idle");
        playerAnimator.ResetTrigger("melee");
        playerAnimator.ResetTrigger("arrow");
        playerAnimator.ResetTrigger("magic");

        moveInput = savedMoveInput; 
    }
}
