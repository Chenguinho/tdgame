using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Blueprint de torretas")]
    public TowerBlueprint stTurret, missileLauncher;

    BuildManager build;

    void Start()
    {
        build = BuildManager.instance; 
    }

    public void SelectTurret()
    {
        build.SelectTowerToBuild(stTurret);
    }

    public void SelectLauncher()
    {
        build.SelectTowerToBuild(missileLauncher);
    }

    public TowerBlueprint GetTower(int i)
    {
        switch (i)
        {
            case 0:
                return stTurret;
            case 1:
                return missileLauncher;
            default:
                return null;
        }
    }

}