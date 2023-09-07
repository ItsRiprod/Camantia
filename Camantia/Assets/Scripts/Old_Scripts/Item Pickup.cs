using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Inventory inventory;
    public GameObject objToAdd;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Debug.Log(objToAdd.name);
            inventory.AddItem(objToAdd, 1);

        }
    }
}
