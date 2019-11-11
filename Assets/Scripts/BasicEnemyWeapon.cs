using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyWeapon : EnemyWeaponBase
{

    public void Start()
    {
        dmg = 20;
        speed = 10;
        coolDown = 2f;
    }

    public override void Movement()
    {
        Vector3 translation = Vector3.back * speed * Time.deltaTime;
        transform.Translate(translation);
    }

    private void OnTriggerEnter(Collider other)
    {
        // collided with player
        if(other.gameObject.layer == 9)
        {
            other.GetComponent<PlayerBehavior>().GetDamage(dmg);
            Destroy(this.gameObject);
        }
    }
}
