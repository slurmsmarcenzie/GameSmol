using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // death sound 
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;

    
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on GhostController GameObject.");
        }
        else
        {
            // debug
            Debug.Log("AudioSource component found.");
        }
    }

    void Update()
    {
        // Check if the hp has reached zero / dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // when the ghost is attacked
    public void TakeDamage(int damageAmount)
    {
      
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // death zero hp
    void Die()
    {

        // Play death sound
        if (deathSound != null && audioSource != null && audioSource.enabled)
        {
            audioSource.PlayOneShot(deathSound);
            Debug.Log("Death sound played.");

            // Wait for the sound to finish playing before destroying the GameObject
            // if we don't do this the sound will never come out as the ghost just destroy what i found
            StartCoroutine(DestroyAfterSound());
        }
        else
        {
           
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterSound()
    {
        // Wait for the length of the death sound
        yield return new WaitForSeconds(deathSound.length);

        // Now destroy 
        Destroy(gameObject);
    }

}
