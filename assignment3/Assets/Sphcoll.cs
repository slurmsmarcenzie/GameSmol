using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphcoll : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            print("Collision working");
        }
    }

}
