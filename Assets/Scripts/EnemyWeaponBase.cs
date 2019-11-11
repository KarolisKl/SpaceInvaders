using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeaponBase : MonoBehaviour
{
    public GameObject go;
    public int dmg;
    public float speed;
    public float coolDown;
    public bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {

    }
}
