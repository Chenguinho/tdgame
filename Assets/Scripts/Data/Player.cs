using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    static public string username;
    static public int level;
    static public int stars;
    static public int points;
    static public int id;
    //Estadisticas de cada nivel
    static public int points1, stars1;
    static public int points2, stars2;
    static public int points3, stars3;
    static public int points4, stars4;
    static public int points5, stars5;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer(string n)
    {
        PlayerData data = SaveSystem.LoadPlayer(n.ToLower());

        username = data.username;
        level = data.level;
        stars = data.stars;
        points = data.points;
        id = data.id;
        points1 = data.points1;
        points2 = data.points2;
        points3 = data.points3;
        points4 = data.points4;
        points5 = data.points5;

        stars1 = data.stars1;
        stars2 = data.stars2;
        stars3 = data.stars3;
        stars4 = data.stars4;
        stars5 = data.stars5;
    }

    public void InitStats()
    {
        for(int i = 1; i < 6; i++)
        {
            setLevelStats(i, 0, 0);
        }
    }

    #region Metodos constructor

    //Modificar informacion (SET)

    public void setUsername(string u)
    {
        username = u;
    }

    public void setLevel(int l)
    {
        level = l;
    }

    public void setStars(int s)
    {
        stars = s;
    }

    public void setPoints(int p)
    {
        points = p;
    }

    public void setId(int i)
    {
        id = i;
    }

    public void setLevelStats(int l, int p, int s)
    {
        switch (l)
        {
            case 1:
                points1 = p;
                stars1 = s;
                break;
            case 2:
                points2 = p;
                stars2 = s;
                break;
            case 3:
                points3 = p;
                stars3 = s;
                break;
            case 4:
                points4 = p;
                stars4 = s;
                break;
            case 5:
                points5 = p;
                stars5 = s;
                break;
            default:
                break;
        }
    }

    //Recuperar informacion (GET)

    public string getUsername()
    {
        return username;
    }

    public int getLevel()
    {
        return level;
    }

    public int getStars()
    {
        return stars;
    }

    public int getPoints()
    {
        return points;
    }

    public int getId()
    {
        return id;
    }

    public int getLevelPoints(int l)
    {
        switch (l)
        {
            case 1:
                return points1;
            case 2:
                return points2;
            case 3:
                return points3;
            case 4:
                return points4;
            case 5:
                return points5;
            default:
                return 0;
        }
    }

    public int getLevelStars(int l)
    {
        switch (l)
        {
            case 1:
                return stars1;
            case 2:
                return stars2;
            case 3:
                return stars3;
            case 4:
                return stars4;
            case 5:
                return stars5;
            default:
                return 0;
        }
    }

    #endregion
}
