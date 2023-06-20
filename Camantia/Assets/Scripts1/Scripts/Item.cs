//Script made by Bonane :)
//Video explaining it: 
//This script mostly serves the purpose of creating something simple to understand and not to be particularly efficient, feel free to therefore improve it!
//Comments on how to improve it's performance would be likely apreciated by others!


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "newItem_scriptable")]
public class Item : ScriptableObject
{
    public string itemName;
    public int currentAmount;
    public int maxStackAmount;
    public Texture itemTexture;
}