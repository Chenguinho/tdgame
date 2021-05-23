using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverPanel, winPanel;

    public static bool gameEnded, gameWon;

    public DataStorer dataStorer;

    public int level;

    public Music music;

    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();
        music.StopAll();
        music.PlayMusic(music.GetAudioSource(level));

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
        music.GetAudioSource(level).Stop();
        gameEnded = true;
        gameOverPanel.SetActive(true);
    }

    public void WinGame()
    {
        music.GetAudioSource(level).Stop();
        gameEnded = true;
        winPanel.SetActive(true);
        dataStorer.StoreData();
    }

    public void PauseMusic()
    {
        music.GetAudioSource(level).Pause();
    }

    public void ResumeMusic()
    {
        music.GetAudioSource(level).Play();
    }

}
