using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard_Door : MonoBehaviour
{

    [SerializeField] private Animator animDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    private bool isOpen = false;
    public Inventory inventory;
    public Item keycard;

    

    // Start is called before the first frame update
    void Start()
    {
    
 
        //transform.rotation = start.rotation;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.E) && !isOpen && inventory.GetAmountOfItem(keycard) >= 1)
        
        {
            animDoor.Play("Door_Open_Keycard", 0, 0.0f);
            isOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            animDoor.Play("Door_Close_Keycard", 0, 0.0f);
            isOpen = false;
        }


    }
}
