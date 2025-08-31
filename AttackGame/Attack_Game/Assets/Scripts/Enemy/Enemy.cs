using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public Animator animator;
    public PlayerHealthBar playerHealthBar;
    public AudioSource audioSource; 
    public AudioClip attackSound;

    private float speed = 53f;
    private float nextAttackTime = 0f;
    private float nextSoundTime = 0f;

    public float soundCooldown = 1f;
    public float damage;
    public float attackRate = 0.1f;

    private bool isAttacking = false;

    void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {

            player = playerObject.transform;

        }

        playerHealthBar = FindObjectOfType<PlayerHealthBar>();

 
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {

        if (!playerHealthBar.isDead)
        {

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > 1f)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                rb.velocity = new Vector3(direction.x * speed, 0, direction.z * speed);

                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            }

            else
            {
                rb.velocity = Vector3.zero;

                if (!isAttacking)
                {

                    StartCoroutine(AttackRoutine());

                }
            }

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {

            animator.SetTrigger("EnemyAttack");

            playAttackSound();

            if (!playerHealthBar.isDead)
            {

                playerHealthBar.takeDamage(damage);

            }

            nextAttackTime = Time.time + attackRate;

        }
    }

    void playAttackSound()
    {
        if (audioSource != null && attackSound != null && Time.time >= nextSoundTime)
        {

            audioSource.PlayOneShot(attackSound,0.95f);

            nextSoundTime = Time.time + soundCooldown; 

        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        animator.SetTrigger("EnemyAttack");

        yield return new WaitForSeconds(0.1f); 

        if (!playerHealthBar.isDead)
        {

            playerHealthBar.takeDamage(damage);

        }

        yield return new WaitForSeconds(0.1f);

        isAttacking = false;
    }
}