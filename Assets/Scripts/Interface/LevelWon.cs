using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWon : MonoBehaviour
{

    private int sum = 100;

    public string nextLevel = "Level2";
    public TextMeshProUGUI points;
    public GameObject[] stars;
    public CurrentGame current;
    public Timer timer;

    void OnEnable()
    {
        CurrentGame.Points = CalculatePoints();
        StartCoroutine(AnimateText());

        CurrentGame.Stars = GetStars();
        StartCoroutine(AnimateStars());

    }

    IEnumerator AnimateText()
    {
        points.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);

        while (round < CurrentGame.Points)
        {
            if(round + sum < CurrentGame.Points)
            {
                round += sum;
                points.text = round.ToString();

                yield return new WaitForSeconds(.005f);
            } else if(round + 5 < CurrentGame.Points)
            {
                round += 5;
                points.text = round.ToString();

                yield return new WaitForSeconds(.001f);
            }
            else
            {
                round++;
                points.text = round.ToString();

                yield return new WaitForSeconds(.4f);
            }
            
        }
    }

    IEnumerator AnimateStars()
    {
        int count = 0;
        yield return new WaitForSeconds(.7f);

        while(count < CurrentGame.Stars)
        {

            stars[count].SetActive(true);
            count++;

            yield return new WaitForSeconds(.5f);
        }
    }

    int GetStars()
    {
        if (CurrentGame.Lives >= current.startLives - 1)
        {
            return 3;
        } else if (CurrentGame.Lives >= current.startLives - 5)
        {
            return 2;
        } else
        {
            return 1;
        }
    }

    int CalculatePoints()
    {
        int returnValue = CurrentGame.Points;

        float timeMul = 2 - (1 / timer.finalTime);
        int moneyMul = Mathf.FloorToInt(CurrentGame.Money * .01f);

        if (moneyMul != 0)
            return Mathf.FloorToInt(returnValue * moneyMul * timeMul);
        else return Mathf.FloorToInt(returnValue * timeMul);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void Next()
    {
        SceneManager.LoadScene(nextLevel);
    }

}
