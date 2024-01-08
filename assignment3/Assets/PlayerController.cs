using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-horizontal, 0f, -vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Ensure the player stays on the XZ plane (ground)
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player around the Y-axis (left and right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the X-axis (up and down)
        Camera playerCamera = GetComponentInChildren<Camera>();
        playerCamera.transform.Rotate(Vector3.left * mouseY);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            // Handle the collision with the obstacle (e.g., stop player movement)
            print("collision detected!");
        }
    }


}
