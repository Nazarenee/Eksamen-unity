using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnimator;
    public Rigidbody playerRigidbody;
    public float walkSpeed, walkBackwardSpeed, defaultWalkSpeed, runSpeedBoost, rotationSpeed;
    public bool isWalking;
    public bool isAttacking = false;
    public Transform playerTransform;

    public void Start()
    {
        defaultWalkSpeed = walkSpeed;
    }
	
    void FixedUpdate()
    {
        if (isAttacking) return; // Prevent movement while attacking

        if (Input.GetKey(KeyCode.W))
        {
            playerRigidbody.linearVelocity = transform.forward * walkSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerRigidbody.linearVelocity = -transform.forward * walkBackwardSpeed * Time.deltaTime;
        }
        else
        {
            playerRigidbody.linearVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (isAttacking) return; // Prevent actions while attacking

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnimator.SetTrigger("walk");
            playerAnimator.ResetTrigger("idle");
            isWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.ResetTrigger("walk");
            playerAnimator.SetTrigger("idle");
            isWalking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnimator.SetTrigger("walkback");
            playerAnimator.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.ResetTrigger("walkback");
            playerAnimator.SetTrigger("idle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTransform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTransform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        // ATTACK INPUT
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            playerRigidbody.linearVelocity = Vector3.zero; // Stop movement

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

            // Wait for attack animation to end
            StartCoroutine(ResetAttack());
        }

        if (isWalking)
        {				
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed += runSpeedBoost;
                playerAnimator.SetTrigger("run");
                playerAnimator.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walkSpeed = defaultWalkSpeed;
                playerAnimator.ResetTrigger("run");
                playerAnimator.SetTrigger("walk");
            }
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f); // Adjust duration to match attack animation
        isAttacking = false;

        // Restart movement animation if the player is still pressing a key
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("run");
            }
            else
            {
                playerAnimator.SetTrigger("walk");
            }
            playerAnimator.ResetTrigger("idle");
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnimator.SetTrigger("walkback");
            playerAnimator.ResetTrigger("idle");
        }
        else
        {
            playerAnimator.SetTrigger("idle"); // Ensure character goes to idle if no movement key is pressed
        }
    }
}
