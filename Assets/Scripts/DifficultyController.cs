using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public float scoreTimerIntervalMultiplier = 6f;
    private int scoreMultiplier = 2;
    public float speedMultiplier = 1.1f;
    public float speedPrefabs = 10;

    public GameManager gameManager;
    public EnemyController enemyController;

    // Start is called before the first frame update
    public void UpdatedDifficulty()
    {
            speedPrefabs *= 1.1f;
            gameManager.score *= scoreMultiplier;
            Debug.Log("La difficulté augmente");
    }
}
