using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerPrice : MonoBehaviour
{

    public TextMeshProUGUI price;

    public int towerIndex;
    public Shop shop;

    void Start()
    {
        price.text = shop.GetTower(towerIndex).cost + "$";
    }
}
