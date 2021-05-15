using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    //Origen
    static public string source = "MainMenu";

    //Pantalla completa
    static public bool full; //Bool para activar o desactivar la pantalla completa

    //Resolución
    static public Resolution[] res; //Resoluciones de la pantalla
    static public List<string> resList; //Lista de resoluciones disponibles
    static public int rIndex; //Índice en la lista de resoluciones

    //Calidad
    static public int qIndex; //Índice en la lista de gráficos

    //Volumen
    static public float volume;

    //SET y GET
    
    public void setSource(string s)
    {
        source = s;
    }

    public void setFull(bool i)
    {
        full = i;
    }

    public void setRes(Resolution[] r)
    {
        res = r;
    }

    public void setResList(List<string> l)
    {
        resList = l;
    }

    public void setRIndex(int i)
    {
        rIndex = i;
    }

    public void setQIndex(int i)
    {
        qIndex = i;
    }

    public void setVolume(float v)
    {
        volume = v;
    }

    public string getSource()
    {
        return source;
    }

    public bool getFull()
    {
        return full;
    }

    public Resolution[] getRes()
    {
        return res;
    }

    public List<string> getResList()
    {
        return resList;
    }

    public int getRIndex()
    {
        return rIndex;
    }

    public int getQIndex()
    {
        return qIndex;
    }

    public float getVolume()
    {
        return volume;
    }
    
}
