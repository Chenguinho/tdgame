using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [Header("Menú de pausa")]
    public GameObject pauseMenu;

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }

    }

    void Toggle()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
            
    }

    public void Restart()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        Toggle();
    }

    public void Quit()
    {
        SceneManager.LoadScene("LevelMenu");
    }

}
