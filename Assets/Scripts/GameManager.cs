using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Variable of class
    public GameObject[] enemies;
    public float xSpawnRange = 6.5f;
    public TextMeshProUGUI gameOverText;

    private float ySpawn = 0.77f;
    private float zEnemySpawn = 24;
    private float startDelay = 1;
    private float enemySpawnTime = 1;
    private bool gameIsActive;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method for enemies spawn
    void SpawnRandomEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndex = Random.Range(0, enemies.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
    }
}
