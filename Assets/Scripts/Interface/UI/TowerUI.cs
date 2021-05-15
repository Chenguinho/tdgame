using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [Header("Descripciones de las torres")]
    public TextMeshProUGUI towerName, towerDescription;

    [Header("Texto")]
    public Text costText, sellText;

    [Header("Botones")]
    public Button upgradeButton;

    [Header("Canvas")]
    public GameObject ui;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        sellText.text = "+" + target.towerBlueprint.sellCost.ToString() + "$";

        if (!target.isUpgraded)
        {
            costText.text = "-" + target.towerBlueprint.upgradeCost.ToString() + "$";
            upgradeButton.interactable = true;
        }
        else
        {
            costText.text = "NIVEL MÁXIMO";
            upgradeButton.interactable = false;
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }

}
