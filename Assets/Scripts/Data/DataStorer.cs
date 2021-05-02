using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorer : MonoBehaviour
{

    private bool isOnline;

    Player player;

    void Start()
    {

        if (Player.id == 0)
            isOnline = false;
        else
            isOnline = true;

    }

    public void StoreData()
    {

        if (isOnline)
            StoreOnline();
        else
            StoreLocal();

    }

    void StoreLocal()
    {
        
    }

    void StoreOnline()
    {
        StartCoroutine(Online());
    }

    IEnumerator Online()
    {
        yield return new WaitForSeconds(1f);
    }

}
