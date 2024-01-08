using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCameraController : MonoBehaviour
{

    // varaibles for character like speed and the spring speed
    public Transform playerTransform;
    public float sensitivity = 2.0f;
    public float normalSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    private float currentSpeed;  
    private bool isSprinting = false;
    private CharacterController characterController;

    // shovel
    public GameObject rustyShovelPrefab;  
    public Transform shovelSpawnPoint;
    private bool isCarryingShovel = false;

    // attack vars
    public float attackRange = 5.0f; 
    public int damageAmount = 100;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = normalSpeed;  
    }

    void Update()
    {
        Look();
        Move();
        HandleSprint();
        HandleShovel();
        UpdateShovelPositionAndRotation();
        HandleAttack();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        Vector3 moveDirection = playerTransform.TransformDirection(movement);

        // Ensure the movement stays the same speed regardless of camera orientation
        moveDirection.y = 0.0f;

        // Move the character using CharacterController
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player around the Y-axis (left and right)
        playerTransform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the X-axis (up and down)
        Camera playerCamera = GetComponentInChildren<Camera>();
        playerCamera.transform.Rotate(Vector3.left * mouseY);
    }

    void HandleSprint()
    {
        // shift to spring
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartSprint();
        }
        else
        {
            StopSprint();
        }
    }

    void StartSprint()
    {
        isSprinting = true;
        currentSpeed = sprintSpeed;

        StartCoroutine(StopSprintAfterDuration(5.0f));
    }

    void StopSprint()
    {
        if (isSprinting)
        {
            isSprinting = false;
            currentSpeed = normalSpeed;
        }
    }

    IEnumerator StopSprintAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopSprint();
    }



    void HandleShovel()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isCarryingShovel)
            {
                DropShovel();
            }
            else
            {
                PickUpShovel();
            }
        }
    }

    void PickUpShovel()
    {
        GameObject shovelInstance = Instantiate(rustyShovelPrefab, shovelSpawnPoint.position, shovelSpawnPoint.rotation);

        // Set the player's transform as the parent of the shovel
        shovelInstance.transform.parent = playerTransform;

        // Set the local position to be slightly left of player
        Vector3 offset = new Vector3(-0.3f, 0.6f, 1.2f); 
        shovelInstance.transform.localPosition = offset;
        // Rotate the shovel 90 degrees along the Y-axis
        // this took ages to look proper
        shovelInstance.transform.localRotation = Quaternion.Euler(270.0f, 0.0f, 0.0f);

        ShovelScript shovelScript = shovelInstance.GetComponent<ShovelScript>();
        if (shovelScript != null)
        {
            // could add more stuff
        }
        isCarryingShovel = true;
    }


    void DropShovel()
    {
        if (isCarryingShovel)
        {
            ShovelScript shovelScript = rustyShovelPrefab.GetComponent<ShovelScript>();

            if (shovelScript != null)
            {
                shovelScript.Drop();
            }

            isCarryingShovel = false;
        }
    }

    bool IsCarryingShovel()
    {
        return isCarryingShovel;

    }


    // see if shovel rotates with mouse 

    void UpdateShovelPositionAndRotation()
    {
        if (isCarryingShovel)
        {
            // Get the mouse input
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Calculate the new rotation based on the mouse input
            // pretty sure this doesnt work eitherway
            Quaternion newRotation = Quaternion.Euler(-mouseY, mouseX, 0.0f); 
            rustyShovelPrefab.transform.localRotation = newRotation;

            // this took ages to get right, wanted the shovel to be in fps view
            Vector3 offset = new Vector3(-0.3f, 0.6f, 1.2f);  
            rustyShovelPrefab.transform.localPosition = offset;
        }
    }

    void HandleAttack()
    {
        // left mouse button for attack if holding shover
        if (Input.GetMouseButtonDown(0) && isCarryingShovel)
        {
   
            Attack();
        }
    }

    void Attack()
    {
        // just debug to sim attack
        Debug.Log("Attacking!");

        // Find the ghost and call its TakeDamage method
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, attackRange))
        {
            GhostController ghost = hit.collider.GetComponent<GhostController>();
            if (ghost != null)
            {
                ghost.TakeDamage(damageAmount);
            }
        }
    }



    // collision debug
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Print a message when a collision is detected
        Debug.Log("Collision detected");
    }
}
