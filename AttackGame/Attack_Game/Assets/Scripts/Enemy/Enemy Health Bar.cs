using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Animator animator;
    public GameObject healthPotion;
    public PlayerScore playerScoreCanvas;
    public AudioSource audioSource;
    public AudioClip deadSound;

    private float maxHealth = 100f;
    private float health;

    private int randomNumber;

    private bool isDead = false;

    void Start()
    {
        health = maxHealth;

        healthSlider.maxValue = maxHealth;

        healthSlider.value = health;

        if (playerScoreCanvas == null)
        {

            playerScoreCanvas = FindObjectOfType<PlayerScore>();

        }
    }

    public void takeDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (health <= 0)
        {

            health = 0;

            Dead();

        }

        healthSlider.value = health;
    }

    private void Dead()
    {

        playerScoreCanvas.addScore(10);

        if (isDead) return;

        isDead = true;

        animator.SetTrigger("deadenemy");

        audioSource.PlayOneShot(deadSound,0.5f);

        Enemy enemyScript = GetComponent<Enemy>();
        if (enemyScript != null)
            enemyScript.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;

        BoxCollider boxCol = GetComponent<BoxCollider>();
        if (boxCol != null)
            boxCol.enabled = false;

        Canvas healthbarCanvas = GetComponentInChildren<Canvas>();
        if (healthbarCanvas != null)
            healthbarCanvas.gameObject.SetActive(false);

        randomNumber = Random.Range(1, 101);

        if (randomNumber <= 15)
        {

            Vector3 spawnPosition = transform.position + new Vector3(0, 9f, 0);
            Vector3 spawnRotation = new Vector3(-90f, 0f, 0f);

            Instantiate(healthPotion, spawnPosition, Quaternion.Euler(spawnRotation));

        }

        StartCoroutine(waitDeadAnimation());
    }

    private IEnumerator waitDeadAnimation()
    {

        yield return new WaitForSeconds(1f);

        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x, -64f, currentPos.z);

        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(13f, currentRotation.y, currentRotation.z);

        Destroy(gameObject, 2f);
    }
}
