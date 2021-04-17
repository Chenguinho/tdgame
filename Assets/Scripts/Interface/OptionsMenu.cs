using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    //Dropdowns (resoluciones y calidad)
    public TMP_Dropdown resDrop, qDrop;

    //Preferencias del jugador
    public GameObject playerPrefs;

    //Mezclador de audio para poder controlar el volumen en los ajustes
    public AudioMixer mixer;

    //Resoluciones
    Resolution[] res;
    public List<string> resList;

    public void Start()
    {
        int rIndex = 0, qIndex;
        res = Screen.resolutions;
        resList = new List<string>();

        for (int i = 0; i < res.Length; i++)
        {
            string option = res[i].width + "x" + res[i].height;
            resList.Add(option);

            if (res[i].width == Screen.currentResolution.width &&
                res[i].height == Screen.currentResolution.height)
            {
                rIndex = i;
            }
        }

        resDrop.ClearOptions();
        resDrop.AddOptions(resList);
        resDrop.value = rIndex;
        resDrop.RefreshShownValue();

        qIndex = QualitySettings.GetQualityLevel();
        qDrop.value = qIndex;
        qDrop.RefreshShownValue();

    }

    //Funcion para manejar el volumen general

    public void SetVolume(float volume)
    {

        mixer.SetFloat("MyVolume", volume);
        playerPrefs.GetComponent<PlayerPrefs>().setVolume(volume);

    }

    //Funcion para aplicar los cambios de los graficos

    public void SetGraphics(int gIndex)
    {

        QualitySettings.SetQualityLevel(gIndex);
        playerPrefs.GetComponent<PlayerPrefs>().setQIndex(gIndex);

    }

    //Funcion para aplicar los cambios de la resolucion

    public void SetResolution(int rIndex)
    {

        Resolution resolution = res[rIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        playerPrefs.GetComponent<PlayerPrefs>().setRIndex(rIndex);

    }

    //Funcion para cambiar el modo de pantalla completa

    public void SetFullscreen(bool isActive)
    {

        Screen.fullScreen = isActive;
        playerPrefs.GetComponent<PlayerPrefs>().setFull(isActive);

    }

    public void GoBack()
    {
        SceneManager.LoadScene(playerPrefs.GetComponent<PlayerPrefs>().getSource());
    }
}
