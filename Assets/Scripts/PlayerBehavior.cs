using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10f;
    int armor = 100;
    public int ammo = 9999;
    int points = 0;

    float coolDown = 0.8f;

    public GameObject currentWeapon;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = Resources.Load("Prefabs/DefaultProjectile") as GameObject;
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float translation = horizontal * speed * Time.deltaTime;
        if(transform.position.x + (horizontal / 2) > 0 && transform.position.x + (horizontal / 2) < MapGeneration.instance.mapX)
        {
            transform.Translate(translation * Time.deltaTime, 0, 0);
            // add rotation to the side
        }

        if (Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(currentWeapon, transform.position + new Vector3(0,0,3), transform.rotation);
        canShoot = false;
        StartCoroutine("ShootTimer");
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

    public void GetDamage(int value)
    {
        armor -= value;
        if(armor <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        MapGeneration.instance.ToggleRestart();
        Destroy(gameObject);
    }

    public void AddArmor(int value)
    {
        armor += value;
    }

    public void AddAmmo(int value)
    {
        ammo += value;
    }

    public void AddPoints(int value)
    {
        points += value;
    }

}
