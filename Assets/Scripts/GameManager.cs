using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Variable of class
    public GameObject[] enemies;
    public float xSpawnRange = 6.5f;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button restartButton;

    public PlayerController playerController;

    private float ySpawn = 0.77f;
    private float zEnemySpawn = 24;
    private float startDelay = 1;
    private float enemySpawnTime = 1;
    private bool gameIsActive;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        gameIsActive = true;
        UpdatedScore(0);
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    // Method for enemies spawn
    void SpawnRandomEnemy()
    {
        if (gameIsActive)
        {
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            int randomIndex = Random.Range(0, enemies.Length);

            Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

            Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        if (playerController.gameOver)
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            gameIsActive = false;
        }
    }

    public void UpdatedScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + Time.deltaTime;
    }
}
