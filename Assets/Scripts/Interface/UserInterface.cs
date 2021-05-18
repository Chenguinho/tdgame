using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Networking;

public class UserInterface : MonoBehaviour
{
    //Variables para el menu
    //Resolution[] res;
    public TMP_Dropdown resDropdown, quaDropdown;
    public AudioMixer mixer;
    public GameObject pMenu;

    //Paneles de nivel bloqueado
    public GameObject pBlock2, pBlock3, pBlock4, pBlock5;

    //Paneles
    public GameObject pPopUpReloading;

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
    int recordLevel, recordStars, recordPoints, userId;

    void Start()
    {

        //Obtenemos la musica
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();

        //Obtenemos el GameObject del jugador con la info asociada
        player = GameObject.FindGameObjectWithTag("Player");
        playerPrefs = GameObject.FindGameObjectWithTag("Prefs");

        /*
         * Cada vez que entremos a esta escena tendremos que
         * volver a cargar la información del usuario
        */

        if (playerPrefs.GetComponent<PlayerPrefs>().getSource() != "MainMenu"
            &&
            playerPrefs.GetComponent<PlayerPrefs>().getSource() != "LevelMenu")
        {
            if (GetPlayer().getId() == 0)
            {
                pPopUpReloading.SetActive(true);
                GetPlayer().LoadPlayer(GetPlayer().getUsername());
                pPopUpReloading.SetActive(false);
            }
            else
            {
                pPopUpReloading.SetActive(true);
                ReloadData();
            }
        }

        usernameText.text = GetPlayer().getUsername();
        starsText.text = GetPlayer().getStars().ToString();
        pointsText.text = GetPlayer().getPoints().ToString();

        //Marcamos las estadisticas del nivel 1
        //Puntuacion record en el nivel
        pT1.text = GetPlayer().getLevelPoints(1).ToString();
        //Rellenamos estrellas segun el nivel
        int lStars = GetPlayer().getLevelStars(1);
        stars1 = GameObject.FindGameObjectsWithTag("StarsLevel1");
        SetActiveStars(lStars, stars1);

        //Hacemos visibles los niveles que el usuario tenga
        if (GetPlayer().getLevel() != 1)
        {
            VisibleLevels(GetPlayer().getLevel());
        }

    }

    /*
     * En Update tenemos que estar comprobando la pulsacion
     * de la tecla M para mostrar el menu al usuario
     */

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

    #region Recargar información del usuario online

    void ReloadData()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        string url = "http://chenguinho.zapto.org/tdgame/phpUnity/getUserData.php";

        WWWForm form = new WWWForm();

        form.AddField("id", GetPlayer().getId());

        using(UnityWebRequest www = UnityWebRequest.Post(url, form))
        {

            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            } else
            {
                string var = www.downloadHandler.text;
                if(var != "1")
                {
                    string[] values = var.Split(","[0]);

                    int level = int.Parse(values[0]);
                    int points = int.Parse(values[1]);
                    int stars = int.Parse(values[2]);

                    player.GetComponent<Player>().setLevel(level);
                    player.GetComponent<Player>().setPoints(points);
                    player.GetComponent<Player>().setStars(stars);

                    AssignOnlineLevelStats(level, values, player.GetComponent<Player>());

                    playerPrefs.GetComponent<PlayerPrefs>().setSource("MainMenu");
                    pPopUpReloading.SetActive(false);

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                 
                } else
                {
                    Debug.Log("ERROR AL RECUPERAR LA INFORMACIÓN");
                }
            }

        }
    }

    public void AssignOnlineLevelStats(int l, string[] values, Player p)
    {
        for (int i = 1; i <= l; i++)
        {
            p.setLevelStats(
                i,
                int.Parse(values[i + (i + 1)]),
                int.Parse(values[i + (i + 2)])
            );
        }
    }

    #endregion

    #region Funciones para ir a los niveles

    public void GoToLevel1()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("Level1");
        SceneManager.LoadScene("Level1");
    }

    public void GoToLevel2()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("Level2");
        SceneManager.LoadScene("Level2");
    }

    public void GoToLevel3()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("Level3");
        SceneManager.LoadScene("Level3");
    }

    public void GoToLevel4()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("Level4");
        SceneManager.LoadScene("Level4");
    }

    public void GoToLevel5()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("Level5");
        SceneManager.LoadScene("Level5");
    }

    public void GoToMenu()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("LevelMenu");
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
