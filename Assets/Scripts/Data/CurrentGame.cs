using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGame : MonoBehaviour
{

    public static int Money;
    public int startMoney = 300;

    public static int Lives;
    public int startLives = 20;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

}
