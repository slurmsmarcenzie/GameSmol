using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShovelScript : MonoBehaviour
{
    private bool isPickedUp = false;

    public void PickUp()
    {
        isPickedUp = true;

    }

    public void Drop()
    {
        isPickedUp = false;

    }

}
