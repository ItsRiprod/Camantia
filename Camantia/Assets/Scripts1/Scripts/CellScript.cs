//Script made by Bonane :)
//Video explaining it: 
//This script mostly serves the purpose of creating something simple to understand and not to be particularly efficient, feel free to therefore improve it!
//Comments on how to improve it's performance would be likely apreciated by others!


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public int Cell_Index; //This is just used to identify it with the Inventory array;
    public InventoryManager inventoryManager;
    public GameObject BasicItemPrefab; //Your prefab for a Item, make it cool!
    public GameObject currentHeldItem;

    public void CreateItemInCell(Item item)
    {
        //First of all, let's delete all child objects. (In theory you don't need this step HOWEVER you can't ever be to sure)
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        currentHeldItem = Instantiate(BasicItemPrefab, transform.position, transform.rotation, transform); //Let's set the current Item we hold in this cell to the Item we create with our Basic Item prefab
        currentHeldItem.GetComponent<Item_script>().SetItem(item); //Let's also update the Items values and text's to the new values
    }

    //This is the main function of the cell and checks if the item in the cell should be moved, stacked or changed
    public void OnClick()
    {
        bool IHaveAddedAItem = false; //This checks if the Item in the cell has been filled


        //Here we look at if we want to stack the current amount of the item in the cell
        if (currentHeldItem != null && inventoryManager.currentItemInHand != null) //Is the Item in the cell and the item in the current hand even there?
        {
            //Both have a Item!
            Debug.Log("[*] Both parties have a Item in hand!");

            Item inventoryManagerItem = inventoryManager.currentItemInHand.GetComponent<Item_script>().currentHeldItemProperties; //Making the long lines a lot smaller!
            Item cellItem = currentHeldItem.GetComponent<Item_script>().currentHeldItemProperties;

            if (cellItem.currentAmount < cellItem.maxStackAmount && cellItem.itemName == inventoryManagerItem.itemName) //Is there even space for squeezing in extra Items and is it the same Item?
            {
                //Yup!
                Debug.Log("[*] The cell has less amount than the max stack amount can hold!");


                int diff = cellItem.maxStackAmount - cellItem.currentAmount; //Let's see how much we can squeeze in by calculating the difference
                if (diff >= inventoryManagerItem.currentAmount) //If the difference is bigger or euqal to what we want to squeeze in, we know there is enough space!
                {
                    cellItem.currentAmount += inventoryManagerItem.currentAmount; //Let's add up the amount of the item in this cell
                    Destroy(inventoryManager.currentItemInHand); //Let's destroy the item in our hand
                    inventoryManager.currentItemInHand = null; //And let's set it to null
                }
                else //There isn't enough space to squeeze all of it
                {
                    cellItem.currentAmount += diff; //So then let's just add up as much as we can 
                    inventoryManagerItem.currentAmount -= diff; //And deduct the rest from our amount in our hand
                }

                if (inventoryManager.currentItemInHand != null) //If there still is a Item in our hand, lets update it's values visually
                {
                    inventoryManager.currentItemInHand.GetComponent<Item_script>().SetItem(null); //Updates the values of the item in our hand visually
                }
                currentHeldItem.GetComponent<Item_script>().SetItem(null); //Updates the values of the item in our cell visually
                IHaveAddedAItem = true; //It has added a amount to the item in this cell
            }
        }

        if (!IHaveAddedAItem) //So we can't stack? Well then let's see what we can do!
        {
            GameObject provisionalCellItemHolder = currentHeldItem; //First of all let's save our current held item in a different variable
            if (inventoryManager.currentItemInHand != null) //Do we currently have a item in our hand?
            {
                currentHeldItem = inventoryManager.currentItemInHand.gameObject; //We have so let's switch them!
            }
            else
            {
                currentHeldItem = null; //If we don't hold a Item in our hand, we can't asign one to this cell
            }
            inventoryManager.currentItemInHand = provisionalCellItemHolder; //We can now set the Item in our hand to the one that WAS in the Cell
        }

        if (currentHeldItem != null) //We don't want to try and modify a GameObject that isn't there, Unity doesn't like that
        {
            //Theres a object here!

            currentHeldItem.transform.parent = transform; //Let's first parent it to this Cell
            currentHeldItem.transform.position = transform.position; //Let's also move it to the middle of this cell
            currentHeldItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); //This sets the Size of the Item in the cell, modify it to whatever you like! You can also set it to the Cells Size (as an example)

            Inventory.instance_.inventoryItemArray[Cell_Index] = currentHeldItem; //Update the main Inventory Array with the Cell Index so that we know where every Item is!
        }
        else
        {
            Inventory.instance_.inventoryItemArray[Cell_Index] = null; //Theres no Item so lets Update the Main Inventory Array!
        }
    }

}