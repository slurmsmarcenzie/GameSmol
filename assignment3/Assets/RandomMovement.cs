using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float changeDirectionInterval = 2.0f;

    private float timer;
    private Vector3 randomDirection;

    void Start()
    {
        // Start with a random direction
        GenerateRandomDirection();
    }

    void Update()
    {
        // Move the object in the current direction
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        // Update the timer
        timer -= Time.deltaTime;

        // If the timer reaches zero, generate a new random direction
        if (timer <= 0.0f)
        {
            GenerateRandomDirection();
            timer = changeDirectionInterval;
        }
    }

    void GenerateRandomDirection()
    {
        // Generate a random direction in the XZ plane
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
