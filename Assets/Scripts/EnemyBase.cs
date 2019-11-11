using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int health;
    public float speed;
    public Vector3[][] movementCycles;
    public GameObject[] weapons; //which gun to use on what projectile   Sadly no time to implement more weapons :(
    public Vector3 currentDirection;
    public float lootModifier;

    public Vector3[] currentCycle;
    bool canShoot = true;
    public GameObject currentWeapon;
    public float coolDown;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //base Movement function (override)
    void Movement()
    {
        // check if can go to the direction
        Vector3 translation = currentDirection * speed * Time.deltaTime;
        transform.Translate(translation);

        if (canShoot) Shoot();
    }

    public virtual void Shoot()
    {
        Instantiate(currentWeapon, transform.position + new Vector3(0, 0, -2), Quaternion.identity);
        canShoot = false;
        StartCoroutine("ShootTimer");
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

    public virtual void ChangeCycle()
    {
        List<int> availableCycleIndexes = new List<int>();
        List<Vector3[]> availableCycles = new List<Vector3[]>();
        for (int i = 0; i < movementCycles.Length; i++)
        {
            Vector3[] cycle = movementCycles[i];
            Vector3 finalPoint = transform.position;

            //add all directions to determine final position
            foreach (Vector3 direction in cycle)
            {
                finalPoint += direction;
            }
            // can perform cycle if enemy ends up in enemy area
            if (MapGeneration.instance.PointIsInMap(finalPoint)) // change?
            {
                availableCycles.Add(cycle);
                availableCycleIndexes.Add(i);
            }
        }

        // select random cycle
        int index = Random.Range(0, availableCycles.Count);
        Debug.Log(availableCycles.Count);
        currentCycle = availableCycles[index];
        currentWeapon = weapons[availableCycleIndexes[index]];

        IEnumerator coroutine = PerformCycle(currentCycle);
        StartCoroutine(coroutine);
    }

    public virtual IEnumerator PerformCycle(Vector3[] cycle)
    {
        int i = 0;
        do
        {
            currentDirection = cycle[i];
            yield return new WaitForSeconds(1f / speed);
            i++;
        } while (i < cycle.Length);
        ChangeCycle();
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        //check if died
        if (health >= 0)
        {
            Die();
        }
    }

    void Die()
    {
        int value = (int) (Random.Range(1, 101) * lootModifier);
        spawnLoot(Inventory.instance.DropItem(value));
        SpawnManager.instance.EnemyDied();
        Destroy(gameObject);
    }

    void spawnLoot(Item item)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = transform.position;
        go.transform.localScale = new Vector3(1.5f, 0.5f, 1.5f);
        go.AddComponent<Pickable>().SetItem(item);
    }
}
