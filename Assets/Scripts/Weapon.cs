using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    // Not enought time :(
    GameObject projectile;
    float coolDown;

    public override void Use()
    {
        //make it current weapon. 
        base.Use(); // returns the actual boolean
    }
}
