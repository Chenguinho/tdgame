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

    [Header("Tipos de torres")]

    public GameObject stTurret, missileTower;

    private GameObject turretToBuild;

    //GET Y SET

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject t)
    {
        turretToBuild = t;
    }

}
