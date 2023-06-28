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
    public Transform lookingAt;
    public float doorOpenRange;
    private float timeTillClose = 0;

    

    // Start is called before the first frame update
    void Start()
    {
    
 
        //transform.rotation = start.rotation;
    }

    // Update is called once per frame
    void Update()
    {
       

        //Opens the door if you have a key card
        if (Input.GetKeyDown(KeyCode.E) && !isOpen && inventory.GetAmountOfItem(keycard) >= 1)
        
        {
            RaycastHit hit;
            if (Physics.Raycast(lookingAt.transform.position,lookingAt.transform.TransformDirection(Vector3.forward), out hit, doorOpenRange)){
                animDoor.Play("Door_Open_Keycard", 0, 0.0f);
                isOpen = true;
                timeTillClose += 5;
            }

            
        }
        else if(Input.GetKeyDown(KeyCode.E) && isOpen && inventory.GetAmountOfItem(keycard) >= 1)
        {
            animDoor.Play("Door_Close_Keycard", 0, 0.0f);
            isOpen = false;
            timeTillClose = 0;
        }

        if (isOpen)
        {
            timeTillClose -= Time.deltaTime;
            
            if(timeTillClose < 1)
            {
                animDoor.Play("Door_Close_Keycard", 0, 0.0f);
                isOpen = false;
            }
        }


    }
}
