using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextLevel = "";

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        //If you collide with the player while there are no frutis left you load the next level
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(nextLevel);

        }
    }
}
