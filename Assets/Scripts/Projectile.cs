using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;


    private void Start()
    {
        StartCoroutine("Destroy");
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(8f);
        Destroy(this.gameObject);
    }

    public virtual void Movement()
    {

    }

    public virtual void Contact(EnemyBase enemy)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Contact(other.transform.GetComponent<EnemyBase>());
        } else if (other.gameObject.tag == "Projectile") // collided with enemy projectile
        {
            
            Destroy(other.transform.parent.gameObject);
        }
        Destroy(this.gameObject);

        // add colliding with projectiles
        // add colliding with enemies
        // add colliding with blockade;
    }



}
