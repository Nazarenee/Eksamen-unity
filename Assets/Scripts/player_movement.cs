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
    public AudioSource audioSource; // Assign this in the inspector
    public AudioClip bowDrawClip; // Assign the Bow Draw audio clip in the inspector
    public AudioClip bowReleaseClip; // Assign the Bow Release audio clip in the inspector


    public Animator playerAnimator;
    public Rigidbody playerRigidbody;
    public float walkSpeed = 5f, walkBackwardSpeed = 2f, defaultWalkSpeed = 5f, rotationSpeed = 300f; // Sprint boost of 1.1f for subtle speed increase
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
        controls.Player.Attack.performed += ctx => Attack(); // Calls Attack when left mouse button is clicked
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
        // If the player is attacking, we should not apply movement logic, but keep the movement input active
        if (isAttacking)
        {
            // Stop any movement or physics updates during the attack animation, but still respect moveInput
            playerRigidbody.linearVelocity = Vector3.zero; // Stop player immediately during the attack
            return; // Don't process any movement if attacking
        }

        // Get the player's current forward direction (taking into account rotation)
        Vector3 forward = playerTransform.forward;
        forward.y = 0; // Flatten the vector to prevent any movement along the y-axis (up/down)

        Vector3 right = playerTransform.right;
        right.y = 0; // Flatten the vector to prevent any movement along the y-axis (up/down)

        // Movement vector: based on input and current facing direction
        Vector3 movement = Vector3.zero;

        // Determine current speed (sprint if holding shift)
        float currentSpeed = isSprinting ? walkSpeed * 2f : walkSpeed;

        // Only move forward/backward when there is input on the y-axis
        if (moveInput.y != 0)
        {
            movement += forward * moveInput.y * currentSpeed;
        }

        // Only move sideways when there is input on the x-axis and we are not idle
        if (moveInput.x != 0)
        {
            // Rotate left or right based on input without moving sideways when idle
            playerTransform.Rotate(0, moveInput.x * rotationSpeed * Time.deltaTime, 0);
        }

        // Apply the movement to the rigidbody velocity (keep existing y-velocity for gravity)
        playerRigidbody.linearVelocity = new Vector3(movement.x, playerRigidbody.linearVelocity.y, movement.z);
    }

    private void Update()
    {
        if (isAttacking) return;

        // Handle player animation based on movement input
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

        // Handle sprint animation just like the walk
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
        Vector2 tempMoveInput = moveInput; // Save the current movement input
        moveInput = Vector2.zero; // Immediately stop movement when attacking
        playerRigidbody.linearVelocity = Vector3.zero; // Stop any movement during the attack animation
        
        audioSource.PlayOneShot(bowDrawClip);

        // Reset movement animation
        playerAnimator.ResetTrigger("walk");
        playerAnimator.ResetTrigger("walkback");
        playerAnimator.ResetTrigger("run");

        // Check the tag of the player and trigger the appropriate attack animation
        if (gameObject.CompareTag("Warrior"))
        {
            playerAnimator.SetTrigger("melee");
        }
        else if (gameObject.CompareTag("Hunter"))
        {
            playerAnimator.SetTrigger("arrow");  // For Hunter, trigger the arrow animation
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
        yield return new WaitForSeconds(1f); // Attack cooldown, adjust as needed
        isAttacking = false;

        // Ensure the character stays in idle state after attack
        playerAnimator.SetTrigger("idle");
        playerAnimator.ResetTrigger("melee");
        playerAnimator.ResetTrigger("arrow");
        playerAnimator.ResetTrigger("magic");

        // Restore movement input and continue moving after the attack animation
        moveInput = savedMoveInput; // Restore the original movement input
    }
}
