using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotions : MonoBehaviour
{

    public PlayerCombat combat;
    public HeatlhPotionCanvas healthPotionCanvas;
    public AudioSource audioSource; 
    public AudioClip collectSound;

    private float rotationSpeed = 120f;
    private float floatAmplitude = 2.5f;
    private float floatFrequency = 4f;   

    private Vector3 startPos;

    void Start()
    { 

        startPos = transform.position;

        combat = FindObjectOfType<PlayerCombat>();
        healthPotionCanvas = FindObjectOfType<HeatlhPotionCanvas>();

    }

    void Update()
    {
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            combat.potionNumber++;

            healthPotionCanvas.changeText(combat.potionNumber);

            if (other.TryGetComponent<AudioSource>(out AudioSource playerAudio))
            {

                playerAudio.PlayOneShot(collectSound, 1.6f);

            }

            Destroy(gameObject);

        }

    }
}
