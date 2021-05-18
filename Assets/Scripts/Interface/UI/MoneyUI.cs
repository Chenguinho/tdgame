using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{

    public TextMeshProUGUI text;

    void Update()
    {
        text.text = CurrentGame.Money.ToString() + "$";
    }

}
