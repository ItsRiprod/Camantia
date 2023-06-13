using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{

    private string currentScene;
    private void Start()
    {
         currentScene = SceneManager.GetActiveScene().name;
    }
    // Start is called before the first frame update
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            

            SceneManager.LoadScene(currentScene);
        }
    }
}
