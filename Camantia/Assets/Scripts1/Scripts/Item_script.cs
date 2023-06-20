//Script made by Bonane :)
//Video explaining it: 
//This script mostly serves the purpose of creating something simple to understand and not to be particularly efficient, feel free to therefore improve it!
//Comments on how to improve it's performance would be likely apreciated by others!


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item_script : MonoBehaviour
{
    public Item object_item; //This is the main Object we copy
    public Item currentHeldItemProperties; //This is the copy with our edited changes to it

    public TMP_Text item_name_text;
    public TMP_Text item_amount;
    public RawImage item_texture;

    //This sets the Items Text, Texture and Name ss
    public void SetItem(Item item)
    {
        if (currentHeldItemProperties == null) //Has the Item been modified?
        {
            //No? Okay, then lets create a copy of our main Object!
            SetCurrenHeldItemProperties(item);
        }

        //This just goes through the UI stuff and Updates it
        item_name_text.text = currentHeldItemProperties.itemName;
        item_amount.text = currentHeldItemProperties.currentAmount.ToString();
        item_texture.texture = currentHeldItemProperties.itemTexture;
    }

    public void SetCurrenHeldItemProperties(Item item)
    {
        //This just creates a copy of the Item we put in it

        currentHeldItemProperties = ScriptableObject.CreateInstance<Item>(); //This creates a new Item scriptable object

        //This goes through our copied Item values and sets them to the basic Object values
        currentHeldItemProperties.itemName = item.itemName;
        currentHeldItemProperties.currentAmount = item.currentAmount;
        currentHeldItemProperties.maxStackAmount = item.maxStackAmount;
        currentHeldItemProperties.itemTexture = item.itemTexture;
    }
}