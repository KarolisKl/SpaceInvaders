using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : EnemyBase
{

    private void Start()
    {
        health = 20;
        movementCycles = new Vector3[][] { MovementCycles.Line(), MovementCycles.Line(true), new Vector3[] { Vector3.forward }, new Vector3[] { Vector3.back }};
        weapons = new GameObject[movementCycles.Length];
        coolDown = 2f;
        lootModifier = 1f;
        for(int i = 0; i < movementCycles.Length; i++)
        {
            weapons[i] = EnemyWeapons.Basic();
        }
        ChangeCycle();
    }
}