//Script made by Bonane :)
//Video explaining it: 
//This script mostly serves the purpose of creating something simple to understand and not to be particularly efficient, feel free to therefore improve it!
//Comments on how to improve it's performance would be likely apreciated by others!


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //This part is needed to make it a Static class so that we can interact with it with all scripts. Very nice 
    public static Inventory instance_ { get; set; }
    public GameObject[] inventoryItemArray;
    public InventoryManager inventoryManager;
    private void Awake()
    {
        instance_ = this;
    }


    public int GetAmountOfItem(Item item)
    {
        //Lets see how much of that Item is in the inventory by looping throuhg our Item array
        int totalAmountOfItemInInventory = 0;
        for (int i = 0; i < inventoryItemArray.Length; i++)
        {
            //Lets see if this space has a item in it
            if (inventoryItemArray[i] != null)
            {
                //It does! Lets see if it is the same Item we are looking for.
                if (inventoryItemArray[i].GetComponent<Item_script>().currentHeldItemProperties.itemName == item.itemName)
                {
                    //It is! Noice. Lets add its amount to our total Amount
                    totalAmountOfItemInInventory += inventoryItemArray[i].GetComponent<Item_script>().currentHeldItemProperties.currentAmount;
                }
            }
        }
        //Good. Now that we have the total amount, lets return that 
        return totalAmountOfItemInInventory;
    }

    public void RemoveItemAmount(Item item, int amount)
    {
        //Lets loop through our Inventory Array!
        for (int i = 0; i < inventoryItemArray.Length; i++)
        {
            //Is there something here?
            if (inventoryItemArray[i] != null)
            {
                //Yeah, theres a Item here! Noice.
                Item_script currentItemScript = inventoryItemArray[i].GetComponent<Item_script>(); //This just makes the long line smaller and easier to read :)

                //Are they the same items? Lets check their Item-names!
                if (currentItemScript.currentHeldItemProperties.itemName == item.itemName)
                {
                    //They are the same Item!
                    if (amount <= currentItemScript.currentHeldItemProperties.currentAmount) //If our amount is smaller or equal to the amount of the Item in the inventory, we can just take away that amount
                    {
                        currentItemScript.currentHeldItemProperties.currentAmount -= amount; //Let's take away that amount
                        if (currentItemScript.currentHeldItemProperties.currentAmount == 0) //Is the inventory amount down to 0?
                        {
                            //Yes! Well then let's delete it!
                            inventoryItemArray[i] = null;
                        }
                        inventoryManager.SetCells(); //Now let's update the inventory do display the changes.
                        return;
                    }
                    else
                    {
                        //The amount we want to deduct is bigger than the current amount of the stack!
                        Debug.Log("[*] The Remaining amount is bigger than the current stack");
                        for (int x = 0; i < inventoryItemArray.Length; x++) //You know the drill, let's loop through the Items to find all items we need
                        {
                            if (inventoryItemArray[x] != null)
                            {
                                if (inventoryItemArray[x].GetComponent<Item_script>().currentHeldItemProperties.itemName == item.itemName) //Lets check if the Item is the one we need
                                {
                                    Debug.Log("[*] Found the Item I was searching for!");

                                    //It is the one we need, ok. Lets look how much that Item has and save it in a variable
                                    int currentAmountOfInvObj = inventoryItemArray[x].GetComponent<Item_script>().currentHeldItemProperties.currentAmount;
                                    Debug.Log("[*] The current amount of the Item is: " + currentAmountOfInvObj.ToString());

                                    if (amount <= currentAmountOfInvObj) //If  the amount we wan't to deduct is smaller than the amount of the item, we can just deduct without worrying about negative numbers
                                    {
                                        Debug.Log("[*] The amount to deduct is smaller than the total amount!");
                                        inventoryItemArray[x].GetComponent<Item_script>().currentHeldItemProperties.currentAmount -= amount;
                                        if (inventoryItemArray[x].GetComponent<Item_script>().currentHeldItemProperties.currentAmount == 0)
                                        {
                                            inventoryItemArray[x] = null;
                                        }
                                        inventoryManager.SetCells();
                                        return;
                                    }
                                    else if (amount > currentAmountOfInvObj)
                                    {
                                        //It is bigger than the stack!
                                        //When this happens we can do some big-brain stuff! 

                                        Debug.Log("[*] The amount to deduct is bigger than the total amount!");
                                        inventoryItemArray[x] = null; //Let's first set the Item to null as we wan't to deduct more than it currently holds
                                        amount -= currentAmountOfInvObj; //The amount we wan't to deduct can be decreased as we just took some of that
                                        Debug.Log("Amount we still want to deduct: " + amount);
                                        if (amount <= 0) //Is the amount 0 now?
                                        {
                                            return;
                                        }
                                    }
                                    inventoryManager.SetCells(); //Let's update the Inventory system to display the changes!
                                }
                            }
                        }
                    }//Thats a lot of brackets :O
                }//Most of the time this is a sign of bad code :3
            }//But eh, who cares?
        }
    }

    public void AddItem(GameObject item_obj, int amount)
    {

        //Let's first check if you can stack the Item in the inventory
        if (GetAmountOfItem(item_obj.GetComponent<Item_script>().object_item) > 0) //Is the item present in the inventory?
        {
            //It is! You know what time it is then! LOOP TIME!!!
            for (int i = 0; i < inventoryItemArray.Length; i++)
            {
                //Is there a item here?
                if (inventoryItemArray[i] != null)
                {
                    //Yes there is!

                    //Just shortening these long lines down to two words
                    Item inventoryItem = inventoryItemArray[i].GetComponent<Item_script>().currentHeldItemProperties;
                    Item itemObjItem = item_obj.GetComponent<Item_script>().object_item;


                    if (inventoryItem.itemName == itemObjItem.itemName) //Is it the same Item though?
                    {
                        //Yeah!
                        Debug.Log("Found a Item with the same name!");

                        //Let's check if it is already at  the maxStackAmount or if we can squeeze a few items in there
                        if (inventoryItem.currentAmount < inventoryItem.maxStackAmount)
                        {
                            int diff = inventoryItem.maxStackAmount - inventoryItem.currentAmount; //With the difference we know how much place there is for us to squeeze stuff in.
                            if (diff >= amount) //If out amount is smaller, then there is place for us to squeeze the Items in there
                            {
                                inventoryItem.currentAmount += amount; //Set the Item in inventory amount up!
                                inventoryManager.SetCells(); //Update the Inventory visually
                                return;
                            }
                            else //It can't hold all of it :(
                            {
                                inventoryItem.currentAmount = inventoryItem.maxStackAmount; //Well then let's just set it to the max amount and deduct from our amount we just added
                                amount -= diff;
                            }
                        }
                    }
                }
            }

        }

        if (amount > 0)
        {
            //Just set Item normally as there is either no item that can stack or there is no similar item
            //LOOOOOOOOOOOOOOP time
            for (int i = 0; i < inventoryItemArray.Length; i++)
            {
                if (inventoryItemArray[i] == null) //Is there no item here?
                {
                    //Space is empty, we can put our item here!

                    Debug.Log("Found a emtpy inventory slot!");
                    GameObject itemObjectToSet = item_obj; //Let's create a new item_object to inherit from our basic Item prefab
                    itemObjectToSet.GetComponent<Item_script>().SetCurrenHeldItemProperties(item_obj.GetComponent<Item_script>().object_item); //Let's set it's properties so that it isn't empty
                    itemObjectToSet.GetComponent<Item_script>().currentHeldItemProperties.currentAmount = amount; //And let's change it's amout to the desired amount
                    inventoryItemArray[i] = itemObjectToSet; //Let's update the Inventory Array so that we stay up-to-date there
                    inventoryManager.SetCells(); //Update the Inventory visually!
                    return;
                }
            }
        }

        //Theres no space for the Item in the inventory!
        //Depending on the game you might want to do something with the rest of the item here!
        Debug.Log("No space for Item!");
    }
    //Hello there. :)
}