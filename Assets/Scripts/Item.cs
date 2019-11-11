using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";    // Name of the item
    public Sprite icon = null;              // Item icon

    //returns true if item is completely used
    public virtual void Use()
    {
        // Override how to use in actual items
        Inventory.instance.Remove(this);
    }

    public virtual void PickUp()
    {

    }
}