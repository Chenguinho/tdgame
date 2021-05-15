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

    [Header("Efectos")]
    public GameObject placeTowerEffect;
    public GameObject sellTowerEffect;

    private TowerBlueprint towerToBuild;
    private Node selectedNode;

    public TowerUI towerUI;

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return CurrentGame.Money >= towerToBuild.cost; } }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
        selectedNode = null;

        towerUI.Hide();
    }

    public void SelectNode(Node node)
    {

        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;

        towerUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        towerUI.Hide();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }

}
