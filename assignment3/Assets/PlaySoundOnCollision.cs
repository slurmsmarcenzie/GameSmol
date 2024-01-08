using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioClip collisionSound;  
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();

 
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
        audioSource.clip = collisionSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision 
        if (collision.gameObject.CompareTag("Player"))
        {
            // Play then
            audioSource.Play();
        }
    }
}
