using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    public Vector3 offset;

    private GameObject turret;

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

        if (build.GetTurretToBuild() == null)
            return;

        if(turret != null)
        {
            //Info en algun lado
            Debug.Log("Ahi no");
            return;
        }

        GameObject turretToBuild = build.GetTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position + offset, transform.rotation);

    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (build.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
