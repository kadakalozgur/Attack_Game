using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerCombat combat;
    public HeatlhPotionCanvas healthPotionCanvas;
    public GameObject healthPotionBottle;
    public GameOverManager gameOverManager;
    public Slider healthSlider;
    public Animator animator;
    public AudioSource audioSource;      
    public AudioClip drinkSound;
    public AudioClip deadSound;
    public BoxCollider playerCollider;

    private float maxHealth = 100f;
    private float health;

    public float invulnerabilityTime = 1f;

    private bool isInvulnerable = false;

    public bool isDead = false;
    public bool isDrink = false;

    void Start()
    {

        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        healthPotionBottle.SetActive(false);

    }

    private void Update()
    {

        drinkHeal();

    }

    public void takeDamage(float damage)
    {

        if (isDead || isInvulnerable)
            return;

        health -= damage;

        healthSlider.value = health;

        StartCoroutine(InvulnerabilityCoroutine());

        if (health <= 0)
        {

            health = 0;

            Dead();

        }

    }

    private void Dead()
    {

        if (isDead)
            return;

        isDead = true;

        playerCollider.enabled = false;

        audioSource.PlayOneShot(deadSound);

        animator.SetTrigger("deadplayer");

        StartCoroutine(GameOverAfterAnimation(1.4f));

    }

    private void drinkHeal()
    {

        if(Input.GetKeyDown(KeyCode.E) && !isDead && !isDrink && combat.potionNumber > 0 && health != 100)
        {

            animator.SetTrigger("drink");

            audioSource.PlayOneShot(drinkSound,1.2f);

            isDrink = true;

            healthPotionBottle.SetActive(true);

            health += 50;

            if(health > maxHealth)
            {

                health = maxHealth;

            }

            healthSlider.value = health;

            combat.potionNumber--;

            healthPotionCanvas.changeText(combat.potionNumber);

            StartCoroutine(DrinkRoutine(3f));

        }

    }

    IEnumerator DrinkRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        healthPotionBottle.SetActive(false);

        isDrink = false;
    }

    IEnumerator InvulnerabilityCoroutine()
    {

        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerabilityTime);

        isInvulnerable = false;

    }

    IEnumerator GameOverAfterAnimation(float delay)
    {

        yield return new WaitForSeconds(delay);

        gameOverManager.setup();

    }
}
