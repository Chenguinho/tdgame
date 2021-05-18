using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Blueprint de torretas")]
    public TowerBlueprint stTurret, missileLauncher, laserBeamer;

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

    public void SelectLaser()
    {
        build.SelectTowerToBuild(laserBeamer);
    }

    public TowerBlueprint GetTower(int i)
    {
        switch (i)
        {
            case 0:
                return stTurret;
            case 1:
                return missileLauncher;
            case 2:
                return laserBeamer;
            default:
                return null;
        }
    }

}