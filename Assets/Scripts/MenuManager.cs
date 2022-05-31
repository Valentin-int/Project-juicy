using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject mainMenu;
    public GameObject leaderBoard;

    public void LeaderBoard()
    {
        mainMenu.gameObject.SetActive(false);
        leaderBoard.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenu.gameObject.SetActive(true);
        leaderBoard.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
