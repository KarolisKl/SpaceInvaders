using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public MapGeneration.SpawnWave[] waves;
    int enemyCount = 0;
    public GameObject[] enemies; // enemies prefabs
    bool spawning;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void StartSpawning()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        spawning = true;
        foreach(MapGeneration.SpawnWave wave in waves)
        {
            List<GameObject> enemiesList = new List<GameObject>();
            for (int i = 0; i < enemies.Length; i++)
            {
                for (int t = 0; t < wave.enemies[i]; t++)
                {
                    enemiesList.Add(enemies[i]);
                }
            }
            var coroutine = SpawnEnemies(enemiesList.ToArray(), wave.timer);
            yield return StartCoroutine(coroutine);
        }
        spawning = false;
    }

    IEnumerator SpawnEnemies(GameObject[] enemiesToSpawn, float timer)
    {
        for(int i = 0; i < enemiesToSpawn.Length; i++)
        {
            yield return new WaitForSeconds(timer);
            Instantiate(enemiesToSpawn[i]);
            enemyCount++;
        }
    }

    public void EnemyDied() {
        enemyCount--;
     }

    // Update is called once per frame
    void Update()
    {
        if(!spawning && enemyCount == 0) // level is done, load another
        {
            MapGeneration.instance.StartLevel(MapGeneration.instance.currentLevel + 1);
        }
    }

    public class SpawnWave
    {
        public int timer; // timer when each enemy will be released
        public int count; // how many spawn
        //how many each type of enemies spawn per whole wave
        public int[] enemies; // 0 - simple, 1 - advanced, 2 - crazy, 3 - bossy <-- create these enemies
    }
}
