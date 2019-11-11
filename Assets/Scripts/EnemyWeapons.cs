using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyWeapons
{
   public static GameObject Basic()
    {
        return Resources.Load("Prefabs/BasicEnemyProjectile") as GameObject;
    }
}