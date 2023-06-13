using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : MonoBehaviour
{

    public GameObject glovePickUp;
    public bool hasGloves = false;

    private void OnTriggerEnter(Collider other)
    {
        //If you collide with the player while there are no frutis left you load the next level
        if (other.tag == "Player")
        {
            hasGloves = true;
            glovePickUp.SetActive(false);
        }
    }
}
