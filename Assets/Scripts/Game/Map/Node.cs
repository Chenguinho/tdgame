using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    [Header("Personalización")]
    public Color hoverColor, notMoneyColor;

    [Header("Dónde colocar la torre (no tocar)")]
    public Vector3 offset;

    [Header("Opcional")]
    public GameObject tower;

    private Renderer rend;
    private Color startColor;

    BuildManager build;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        build = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!build.CanBuild)
            return;

        if(tower != null)
        {
            //Info en algun lado
            Debug.Log("Ahi no");
            return;
        }

        build.BuildTowerOn(this);

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

    public Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }

}
