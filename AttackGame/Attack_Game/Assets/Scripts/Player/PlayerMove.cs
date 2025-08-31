using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip walkSound;
    public AudioClip runSound;

    private float walkSpeed = 40f;

    private bool isRunning = false;
    private bool isWalking = false;

    void Update()
    {

        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveZ += 1f;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveZ -= 1f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveX += 1f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveX -= 1f;

        Vector3 moveDir = (transform.forward * moveZ + transform.right * moveX).normalized;

        isWalking = moveDir != Vector3.zero;
        isRunning = (moveZ > 0) && Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            animator.ResetTrigger("idle");
            animator.ResetTrigger("walk");
            animator.ResetTrigger("walkbackward");
            animator.ResetTrigger("walkleft");
            animator.ResetTrigger("walkright");
            animator.SetTrigger("run");

            rb.velocity = new Vector3(moveDir.x * walkSpeed * 2f, rb.velocity.y, moveDir.z * walkSpeed * 2f);

            HandleRunningSound();
        }

        else if (isWalking)
        {

            animator.ResetTrigger("idle");
            animator.ResetTrigger("run");

            if (moveX > 0)
            {

                animator.SetTrigger("walkright");
                animator.ResetTrigger("walk");
                animator.ResetTrigger("walkbackward");
                animator.ResetTrigger("walkleft");

            }

            else if (moveX < 0)
            {

                animator.SetTrigger("walkleft");
                animator.ResetTrigger("walk");
                animator.ResetTrigger("walkbackward");
                animator.ResetTrigger("walkright");

            }

            else if (moveZ > 0)
            {

                animator.SetTrigger("walk");
                animator.ResetTrigger("walkbackward");
                animator.ResetTrigger("walkleft");
                animator.ResetTrigger("walkright");

            }

            else if (moveZ < 0)
            {

                animator.SetTrigger("walkbackward");
                animator.ResetTrigger("walk");
                animator.ResetTrigger("walkleft");
                animator.ResetTrigger("walkright");

            }

            rb.velocity = new Vector3(moveDir.x * walkSpeed, rb.velocity.y, moveDir.z * walkSpeed);

            HandleWalkingSound();
        }

        else
        {
            animator.ResetTrigger("walkright");
            animator.ResetTrigger("run");
            animator.ResetTrigger("walk");
            animator.ResetTrigger("walkbackward");
            animator.ResetTrigger("walkleft");
            animator.SetTrigger("idle");

            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            HandleIdleSound();
        }
    }

    void HandleWalkingSound()
    {

        if (audioSource.isPlaying)
        {

            audioSource.Stop();

        }

        if (audioSource2.clip != walkSound || !audioSource2.isPlaying)
        {
            if (walkSound != null)
            {

                audioSource2.clip = walkSound;
                audioSource2.loop = true;
                audioSource2.volume = 1f;
                audioSource2.Play();

            }
        }
    }

    void HandleRunningSound()
    {

        if (audioSource.clip != runSound || !audioSource.isPlaying)
        {
            if (runSound != null)
            {

                audioSource.clip = runSound;
                audioSource.loop = true;
                audioSource.volume = 0.35f;
                audioSource.Play();

            }
        }
    }

    void HandleIdleSound()
    {

        if (audioSource.isPlaying)
        {

            audioSource.Stop();

        }

        if (audioSource2.isPlaying)
        {

            audioSource2.Stop();

        }
    }
}
