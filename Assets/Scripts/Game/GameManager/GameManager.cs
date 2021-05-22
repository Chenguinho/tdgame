using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverPanel, winPanel;

    public static bool gameEnded, gameWon;

    public DataStorer dataStorer;

    public int level;

    void Start()
    {
        gameEnded = false;
    }

    void Update()
    {
        if (gameEnded)
            return;

        if(CurrentGame.Lives <= 0)
        {

            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
    }

    public void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        dataStorer.StoreData();
    }

}
