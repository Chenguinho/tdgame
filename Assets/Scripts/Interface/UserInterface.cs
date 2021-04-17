using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UserInterface : MonoBehaviour
{
    //Variables para el menu
    //Resolution[] res;
    public TMP_Dropdown resDropdown, quaDropdown;
    public AudioMixer mixer;
    public GameObject pMenu;

    //Paneles de nivel bloqueado
    public GameObject pBlock2, pBlock3, pBlock4, pBlock5;

    //Textos TextMeshPro de la barra superior
    public TextMeshProUGUI usernameText, starsText, pointsText;
    //Puntuaciones de cada nivel
    public TextMeshProUGUI pT1, pT2, pT3, pT4, pT5;

    //Imagenes para las estrellas de cada nivel
    GameObject[] stars1, stars2, stars3, stars4, stars5;

    //Datos del usuario
    GameObject player;
    public GameObject playerPrefs;
    string username;
    int recordLevel, recordStars, recordPoints;

    void Start()
    {

        //Obtenemos la musica
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();

        //Obtenemos el GameObject del jugador con la info asociada
        player = GameObject.FindGameObjectWithTag("Player");
        playerPrefs = GameObject.FindGameObjectWithTag("Prefs");

        //Recogemos los datos del usuario cada vez que entramos en la escena
        username = player.GetComponent<Player>().getUsername();
        recordLevel = player.GetComponent<Player>().getLevel();
        recordStars = player.GetComponent<Player>().getStars();
        recordPoints = player.GetComponent<Player>().getPoints();

        usernameText.text = username;
        starsText.text = recordStars.ToString();
        pointsText.text = recordPoints.ToString();

        //Marcamos las estadisticas del nivel 1
        //Puntuacion record en el nivel
        pT1.text = GetPlayer().getLevelPoints(1).ToString();
        //Rellenamos estrellas segun el nivel
        int lStars = GetPlayer().getLevelStars(1);
        stars1 = GameObject.FindGameObjectsWithTag("StarsLevel1");
        SetActiveStars(lStars, stars1);

        //Hacemos visibles los niveles que el usuario tenga
        if (recordLevel != 1)
        {
            VisibleLevels(recordLevel);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (pMenu.activeSelf)
            {
                pMenu.SetActive(false);
            } else
            {
                pMenu.SetActive(true);
            }
        }
        
    }

    #region Carga de niveles

    //Funcion para hacer visibles los niveles necesarios

    public void VisibleLevels(int nLevels)
    {
        for(int i = 2; i <= nLevels; i++)
        {
            switch (i)
            {
                case 2:
                    pBlock2.SetActive(false);
                    pT2.text = GetPlayer().getLevelPoints(2).ToString();
                    stars2 = GameObject.FindGameObjectsWithTag("StarsLevel2");
                    SetActiveStars(GetPlayer().getLevelStars(2), stars2);
                    break;
                case 3:
                    pBlock3.SetActive(false);
                    pT3.text = GetPlayer().getLevelPoints(3).ToString();
                    stars3 = GameObject.FindGameObjectsWithTag("StarsLevel3");
                    SetActiveStars(GetPlayer().getLevelStars(3), stars3);
                    break;
                case 4:
                    pBlock4.SetActive(false);
                    pT4.text = GetPlayer().getLevelPoints(4).ToString();
                    stars4 = GameObject.FindGameObjectsWithTag("StarsLevel4");
                    SetActiveStars(GetPlayer().getLevelStars(4), stars4);
                    break;
                case 5:
                    pBlock5.SetActive(false);
                    pT5.text = GetPlayer().getLevelPoints(5).ToString();
                    stars5 = GameObject.FindGameObjectsWithTag("StarsLevel5");
                    SetActiveStars(GetPlayer().getLevelStars(5), stars5);
                    break;
                default:
                    break;
            }
        }
    }

    //Función para asignar visualmente las estrellas necesarias

    public void SetActiveStars(int nStars, GameObject[] stars)
    {
        switch (nStars)
        {
            case 0:
                stars[0].SetActive(false);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                break;
            case 1:
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                break;
            case 2:
                stars[2].SetActive(false);
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    #endregion

    #region Funciones para ir a los niveles

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void GoToLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void GoToLevel5()
    {
        SceneManager.LoadScene("Level5");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToOptions()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("LevelMenu");
        SceneManager.LoadScene("ConfigMenu");
    }

    #endregion

    public void Exit()
    {
        Application.Quit();
    }

    public Player GetPlayer()
    {
        return player.GetComponent<Player>();
    }



}
