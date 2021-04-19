using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    #region Singleton (solo una instancia)

    public static BuildManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Hay más de una instancia");
            return;
        }
        instance = this;    
    }

    #endregion

    [Header("Efecto de construcción")]
    public GameObject placeTowerEffect;

    private TowerBlueprint towerToBuild;

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return CurrentGame.Money >= towerToBuild.cost; } }

    //SET

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
    }

    public void BuildTowerOn(Node n)
    {

        if(CurrentGame.Money < towerToBuild.cost)
        {
            //Mostrar info
            Debug.Log("No tienes pasta");
            return;
        }

        CurrentGame.Money -= towerToBuild.cost;

        GameObject tower = (GameObject) Instantiate(towerToBuild.prefab, n.GetBuildPosition(), Quaternion.identity);
        n.tower = tower;

        GameObject effect = (GameObject)Instantiate(placeTowerEffect, n.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

    }

}
