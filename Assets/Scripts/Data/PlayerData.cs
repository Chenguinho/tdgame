using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public string username; //Nombre de usuario
    public int level;       //Nivel record alcanzado
    public int stars;       //Estrellas record conseguidas
    public int points;      //Puntos record conseguidos
    public int id;          //Id del jugador (0 -> Local)
    //Estadisticas de cada nivel
    public int points1, stars1;
    public int points2, stars2;
    public int points3, stars3;
    public int points4, stars4;
    public int points5, stars5;

    public PlayerData(Player p)
    {
        level = p.getLevel();
        username = p.getUsername();
        stars = p.getStars();
        points = p.getPoints();
        id = p.getId();
        for(int i = 1; i < 6; i++)
        {
            if (i == 1)
            {
                points1 = p.getLevelPoints(i);
                stars1 = p.getLevelStars(i);
            } else if (i == 2)
            {
                points2 = p.getLevelPoints(i);
                stars2 = p.getLevelStars(i);
            } else if (i == 3)
            {
                points3 = p.getLevelPoints(i);
                stars3 = p.getLevelStars(i);
            } else if (i == 4)
            {
                points4 = p.getLevelPoints(i);
                stars4 = p.getLevelStars(i);
            } else if (i == 5)
            {
                points5 = p.getLevelPoints(i);
                stars5 = p.getLevelStars(i);
            }
        }
    }

}
