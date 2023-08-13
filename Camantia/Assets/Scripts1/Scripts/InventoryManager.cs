//Script made by Bonane :)
//Video explaining it: 
//This script mostly serves the purpose of creating something simple to understand and not to be particularly efficient, feel free to therefore improve it!
//Comments on how to improve it's performance would be likely apreciated by others!


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform cellParent;
    public GameObject currentItemInHand;
    public Vector3 offset;
    bool currentlyOpen = false;
    public GameObject InventoryMenu;

    public Item itemToRemove;
    public int amountToRemove;
    public GameObject itemToAdd;

    private void Start()
    {
        //Loading all the items into the cells at the beginning
        SetCells();
    }
    private void Update()
    {
        //Just checking these two boys every update :D
        MoveItemInHand();
        OpenClose();



        ////////////////////THIS IS ONLY FOR TESTING PURPOSES AND CAN BE MOVED IF WANTED TO////////////////////
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Just looking at the current amount of a item and checking if I have more in the inventory than needed for building (so that we don't get negative values)
            Debug.Log("Total amount: " + Inventory.instance_.GetAmountOfItem(itemToRemove));
            if ((Inventory.instance_.GetAmountOfItem(itemToRemove) - amountToRemove) >= 0)
            {
                //We know that we have enough to build, so lets build and remove the needed items!
                Inventory.instance_.RemoveItemAmount(itemToRemove, amountToRemove);
            }
        }
/*        if (Input.GetKeyDown(KeyCode.A))
        {
            Inventory.instance_.AddItem(itemToAdd, 5);
        }*/
        ////////////////////THIS IS ONLY FOR TESTING PURPOSES AND CAN BE MOVED IF WANTED TO////////////////////
        ///
    }
    void MoveItemInHand()
    {
        //If the Item is null, we don't need to move it :)
        if (currentItemInHand == null)
        {
            return;
        }

        //Actually setting the object in hand to the desired position
        currentItemInHand.transform.parent = transform;
        currentItemInHand.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);  //You can change these values to modify the size of the Item when in hand (ie. moving it)
        Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f) + offset; //<- this offset is important as without it you can't click on the buttons
        currentItemInHand.transform.position = pos; //Setting the item to the actual position
    }

    public void SetCells()
    {
        int index = 0;

        //Looping through every child object under <cellParent>
        foreach (Transform cell in cellParent)
        {

            //If it already has a Item as a child, let's delete if so to not have 2 items in one cell
            if (cell.childCount > 0)
            {
                foreach (Transform child in cell)
                {
                    Destroy(child.gameObject);
                }
            }



            if (Inventory.instance_.inventoryItemArray[index] != null) //Checking if there should be a Item in the current Inventory slot
            {
                if (Inventory.instance_.inventoryItemArray[index].GetComponent<Item_script>().currentHeldItemProperties != null) //Does this Item already have a modified Item value?
                {
                    //Yes it does? Ok, then let's set the Item in the cell to those modified values!
                    cell.GetComponent<CellScript>().CreateItemInCell(Inventory.instance_.inventoryItemArray[index].GetComponent<Item_script>().currentHeldItemProperties);
                }
                else
                {
                    //No it doesn't? Ok, then let's set the Item in the cell to the basic values!
                    cell.GetComponent<CellScript>().CreateItemInCell(Inventory.instance_.inventoryItemArray[index].GetComponent<Item_script>().object_item);
                }
                //Lets update the inventory Array so that we can save it later
                Inventory.instance_.inventoryItemArray[index] = cell.GetComponent<CellScript>().currentHeldItem;
            }
            //Let's give the cell a index to make it easier when looking at the array and the cell (Array[index] <---->  cell.index)
            cell.GetComponent<CellScript>().Cell_Index = index;
            index++;
        }
    }

    //This is a VERY basic way of just opening and closing a inventory, feel free to change it and make some cool animations! 
    void OpenClose()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            currentlyOpen = !currentlyOpen; //If it's (False), it turns (True) and vise versa
            InventoryMenu.SetActive(currentlyOpen);
        }
    }
}