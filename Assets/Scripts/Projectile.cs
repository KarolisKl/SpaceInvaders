using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {

    }

    public virtual void Contact(EnemyBase enemy)
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("opa");
            Contact(other.transform.GetComponent<EnemyBase>());
        }
        Destroy(this.gameObject);
    }



}
