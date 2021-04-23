using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverPanel;

    public static bool gameEnded;

    void Start()
    {
        gameEnded = false;
    }

    void Update()
    {
        if (gameEnded)
            return;

        if(CurrentGame.Lives < 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
    }
}
