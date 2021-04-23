using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI points;
    private int Stars;
    public GameObject[] stars;

    void OnEnable()
    {
        points.text = CurrentGame.Points.ToString();
        Stars = CurrentGame.Stars;
        switch (Stars)
        {
            case 0:
                return;
            case 1:
                stars[0].SetActive(true);
                break;
            case 2:
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                break;
            case 3:
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(true);
                break;
            default:
                return;
        }
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        SceneManager.LoadScene("LevelMenu");
    }

}
