using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataStorer : MonoBehaviour
{

    private bool isOnline;

    public Player player;
    public GameManager gameManager;
    public LevelWon levelWon;

    int level;

    void Start()
    {

        level = gameManager.level;

        if (Player.id == 0)
            isOnline = false;
        else
            isOnline = true;

    }

    public void StoreData()
    {

        if (isOnline)
            StoreOnline();
        else
            StoreLocal();

    }

    void StoreLocal()
    {

        if(player.getLevel() == level)
        {

            player.setLevel(level + 1);
            player.setLevelStats(level, CurrentGame.Points, CurrentGame.Stars);
            player.setPoints(player.getPoints() + CurrentGame.Points);
            player.setStars(player.getStars() + CurrentGame.Stars);

        } else
        {

            if (player.getLevelPoints(level) < CurrentGame.Points)
                player.setLevelStats(level, CurrentGame.Points, player.getLevelStars(level));

            if(player.getLevelStars(level) < CurrentGame.Stars)
            {
                player.setLevelStats(level, player.getLevelPoints(level), CurrentGame.Stars);
                player.setStars(player.getStars() + CurrentGame.Stars);
            }

            player.setPoints(player.getPoints() + CurrentGame.Points);

        }

        SaveSystem.SavePlayer(player);

    }

    void StoreOnline()
    {
        StartCoroutine(Online());
    }

    IEnumerator Online()
    {

        string url = "http://chenguinho.zapto.org/tdgame/phpUnity/storeData.php";

        WWWForm form = new WWWForm();
        form.AddField("id", Player.id);
        form.AddField("stars", CurrentGame.Stars);
        form.AddField("points", CurrentGame.Points);
        form.AddField("level", level);

        using(UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            } else
            {
                Debug.Log("Datos almacenados correctamente");
            }

        }
    }

}
