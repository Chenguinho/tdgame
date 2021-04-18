using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGame : MonoBehaviour
{

    public static int Money;
    public int startMoney = 300;

    void Start()
    {
        Money = startMoney;
    }

}
