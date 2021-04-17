using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager build;

    void Start()
    {
        build = BuildManager.instance; 
    }

    public void PurchaseTurret()
    {
        build.SetTurretToBuild(build.stTurret);
    }

    public void PurchaseLauncher()
    {
        build.SetTurretToBuild(build.missileTower);
    }

}
