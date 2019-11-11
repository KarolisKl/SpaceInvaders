using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Projectile
{


    // Update is called once per frame
    public override void Movement()
    {
        Vector3 translation = Vector3.forward * speed * Time.deltaTime;
        transform.Translate(translation);
    }

    public override void Contact(EnemyBase enemy)
    {
        enemy.GetDamage(damage);
    }
}
