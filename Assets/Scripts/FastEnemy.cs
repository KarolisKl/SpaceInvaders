using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : EnemyBase
{
    // not enough time to implement :(
    private void Start()
    {
        health = 40;
        movementCycles = new Vector3[][] { MovementCycles.Line(), MovementCycles.Line(true), MovementCycles.Circle(),
            MovementCycles.Circle(true), new Vector3[] { Vector3.forward }, new Vector3[] { Vector3.back } };
        weapons = new GameObject[movementCycles.Length];
        speed = 2f;
        coolDown = 2f;
        lootModifier = 2f;
        for (int i = 0; i < movementCycles.Length; i++)
        {
            weapons[i] = EnemyWeapons.Basic();
        }
        ChangeCycle();
    }
}
