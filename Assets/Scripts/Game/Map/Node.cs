using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{

    [Header("Personalización")]
    public Color hoverColor, notMoneyColor;

    [Header("Dónde colocar la torre (no tocar)")]
    public Vector3 offset;

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager build;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        build = BuildManager.instance;
    }

    #region Metodos OnMouse[Action]()

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(tower != null)
        {
            build.SelectNode(this);
            return;
        }

        if (!build.CanBuild)
            return;

        BuildTower(build.GetTowerToBuild());

    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!build.CanBuild)
            return;
        if (build.HasMoney)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = notMoneyColor;
        }

        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    #endregion

    public Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }

    #region Acciones con las diferentes torres

    void BuildTower(TowerBlueprint blueprint)
    {
        if (CurrentGame.Money < blueprint.cost)
        {
            //Mostrar info
            Debug.Log("No tienes pasta");
            return;
        }

        CurrentGame.Money -= blueprint.cost;

        GameObject t = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = t;

        towerBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(build.placeTowerEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }

    public void UpgradeTower()
    {
        if (CurrentGame.Money < towerBlueprint.upgradeCost)
        {
            //Mostrar info
            Debug.Log("No tienes pasta");
            return;
        }

        CurrentGame.Money -= towerBlueprint.upgradeCost;

        Destroy(tower);

        GameObject t = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = t;

        GameObject effect = (GameObject)Instantiate(build.placeTowerEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
    }

    public void SellTower()
    {

        CurrentGame.Money += towerBlueprint.sellCost;

        Destroy(tower);
        towerBlueprint = null;

        GameObject effect = (GameObject)Instantiate(build.sellTowerEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2.5f);

    }

    #endregion

}
