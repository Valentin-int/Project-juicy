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
    public GameObject leaderBoard;
    public GameObject gameScene;
    public float xSpawnRange = 6.5f;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button mainMenuButton;
    public Button restartButton;
    public int moreScore = 50;
    public int score = 0;

    public PlayerController playerController;
    public DifficultyController difficultyController;

    private float xSpawn = 0;
    private float ySpawn = 0.77f;
    private float zSpawn = -7.96f;
    private float zEnemySpawn = 24;
    public float startDelay = 1;
    public float enemySpawnTime = 1;
    public bool gameIsActive;

    // Variable for timer score
    public int time;
    public float scoreTimerInterval = 5f;
    public float tick;

    // Variable for leader board
    public TextMeshProUGUI highScoreText;
    private string scoreKey = "Score";
    private int highScore = 0;
    private int[] scoreArray = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        ZPlayerPrefs.Initialize("banana", "sgrfesg6gs5634ze67ze87reythgf3d4fs354687rg");
        ShowSave();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsActive)
        {
            time = (int)Time.timeSinceLevelLoad;
            UpdatedScore();
            GameOver();
            scoreText.text = "Score: " + score;
        }
    }

    // Method for enemies spawn
    void SpawnRandomEnemy()
    {
        if (gameIsActive)
        {
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            int randomIndex = Random.Range(0, enemies.Length);

            Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

            GameObject enemiesPrefabs = Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
            enemiesPrefabs.GetComponent<EnemyController>().speed = difficultyController.speedPrefabs;
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

    public void GameOver()
    {
        if (playerController.gameOver)
        {
            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            gameIsActive = false;
            SaveScores();
        }
    }

    public void UpdatedScore()
    {
        if (time == tick && gameIsActive)
        {
            Debug.Log("foila");
            tick = time + scoreTimerInterval;
            score += moreScore;
            difficultyController.UpdatedDifficulty();
            resetScore();
        }
    }

    public void StartGame()
    {
        time = (int)Time.timeSinceLevelLoad;
        tick = scoreTimerInterval;
        gameIsActive = true;
        resetScore();
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);

        mainMenu.gameObject.SetActive(false);
        gameScene.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }

    public void resetScore()
    {
        scoreText.text = "Score: " + score;
    }

    void SaveScores()
    {
        for (int i = 0; i < scoreArray.Length; i++)
        {
            if (score > scoreArray[i])
            {
                scoreKey = "Score" + (i + 1).ToString();
                ZPlayerPrefs.GetInt(scoreKey, score);
                break;
            }
        }
    }

    void ShowSave()
    {
        for (int i = 0; i < scoreArray.Length; i++)
        {
            scoreKey = "Score" + (i + 1).ToString();
            highScore = ZPlayerPrefs.GetInt(scoreKey, 0);
            scoreArray[i] = highScore;
            if (highScore > 0)
            {
                highScoreText.text = highScoreText.text + (i + 1).ToString() + ": " + highScore + "\n";
            }
        }
    }
}
