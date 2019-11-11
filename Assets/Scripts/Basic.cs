﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Basic")]
public class Basic : Item
{
    public int value;
    public Stat stat;

    public override void PickUp(){
        Use();
     }

    public override void Use()
    {
        // add points armor or ammo according to the item type
        PlayerBehavior player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        switch (stat)
        {
            case Stat.Ammo:
                {
                    player.AddAmmo(value);
                    break;
                }
            case Stat.Points:
                {
                    player.AddPoints(value);
                    break;
                }
            case Stat.Armor:
                {
                    player.AddArmor(value);
                    break;
                }
        }
    }
}

public enum Stat { Armor, Points, Ammo }
