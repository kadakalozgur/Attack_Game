using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public PlayerMove playerMove;
    public KnifeAttack knifeAttack;
    public PlayerHealthBar healthBar;
    public AudioSource audioSource;
    public AudioClip swordSwingSound;
    public AudioClip swordHitSound;

    private float attack1Duration = 0.75f;
    private float attack2Duration = 1f;
    private float animationSpeed = 1.5f;

    public float attack2Cooldown = 6.5f;
    public float lastAttack2Time = -Mathf.Infinity;

    public int potionNumber = 0;

    private bool isAttacking = false;

    public bool attack2 = false;

   
    void Start()
    {

        animator.speed = animationSpeed;

    }

    void Update()
    {

        meleeAttack();

    }

    void meleeAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && !healthBar.isDrink && !healthBar.isDead)
        {

            Attack();

        }

        if (Input.GetMouseButtonDown(1) && !isAttacking && !healthBar.isDrink && Time.time >= lastAttack2Time + attack2Cooldown && !healthBar.isDead)
        {

            Attack2();

        }
    }

    void Attack()
    {
        isAttacking = true;

        animator.SetTrigger("Attack1");

        knifeAttack.enableBoxColl();

        playSwingSound();

        StartCoroutine(EndAttack(attack1Duration));
    }

    void Attack2()
    {

        isAttacking = true;
        attack2 = true;

        animator.SetTrigger("Attack2");

        knifeAttack.enableBoxColl();

        lastAttack2Time = Time.time;

        StartCoroutine(PlayDoubleSwingSounds());

        StartCoroutine(EndAttack(attack2Duration));

    }

    void ResetAttack()
    {

        isAttacking = false;
        attack2 = false;

        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");

        knifeAttack.disableBoxColl();

    }

    public void playSwingSound()
    {

        if (healthBar.isDrink || healthBar.isDead)
            return;

        audioSource.PlayOneShot(swordSwingSound,1f);

    }

    public void playHitSound()
    {

        if (healthBar.isDrink || healthBar.isDead)
            return;

        audioSource.PlayOneShot(swordHitSound,0.45f);

    }

    public void playDoubleHitSound()
    {

        StartCoroutine(PlayDoubleHitSounds());

    }

    private IEnumerator EndAttack(float duration)
    {

        yield return new WaitForSeconds(duration);
            
        ResetAttack();

    }

    private IEnumerator PlayDoubleSwingSounds()
    {

        playSwingSound();

        yield return new WaitForSeconds(0.3f);

        if (isAttacking && attack2)
        {

            playSwingSound();

        }
    }

    private IEnumerator PlayDoubleHitSounds()
    {

        playHitSound();

        yield return new WaitForSeconds(0.2f);

        playHitSound();
    }
}