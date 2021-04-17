using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

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

    public GameObject stTurret;

    private GameObject turretToBuild;

    void Start()
    {
        turretToBuild = stTurret;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

}
