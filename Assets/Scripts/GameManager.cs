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
    public GameObject player;
    public GameObject mainMenu;
    public GameObject gameScene;
    public float xSpawnRange = 6.5f;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button mainMenuButton;
    public Button restartButton;
    public int moreScore = 50;
    public int score = 0;

    public PlayerController playerController;

    private float xSpawn = 0;
    private float ySpawn = 0.77f;
    private float zSpawn = -7.96f;
    private float zEnemySpawn = 24;
    private float startDelay = 1;
    private float enemySpawnTime = 1;
    private bool gameIsActive;

    // Variable for timer score
    private int time;
    public float scoreTimerInterval = 5f;
    private float tick;

    private void Awake()
    {
        time = (int)Time.timeSinceLevelLoad;
        tick = scoreTimerInterval;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = (int)Time.timeSinceLevelLoad;
        UpdatedScore();
        GameOver();
        scoreText.text = "Score: " + score;
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
        playerController.gameOver = false;
        gameIsActive = true;
        score = 0;
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(xSpawn, ySpawn, zSpawn);
        resetScore();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        if (playerController.gameOver)
        {
            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            gameIsActive = false;
        }
    }

    public void UpdatedScore()
    {
        if (time == tick && gameIsActive)
        {
            tick = time + scoreTimerInterval;
            score += moreScore;
            resetScore();
        }
    }

    public void StartGame()
    {
        gameIsActive = true;
        resetScore();
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);

        mainMenu.gameObject.SetActive(false);
        gameScene.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }

    void resetScore()
    {
        scoreText.text = "Score: " + score;
    }
}
