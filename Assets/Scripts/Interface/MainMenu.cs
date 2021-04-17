using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    #region Variables del script

    //Paneles que tienen contenido y tenemos que manejar
    public GameObject pPrincipal, pNewGame,
        pHelp, pOnline, pLocal, pSelectGame,
        pPopupExists, pPopupNotFound, pPopUpConnect;

    //Inputs para introducir datos
    public TMP_InputField nameInput, userRegistration, passwordRegistration,
        signInUser, signInPwd, newLocalGame, loadLocalGame;

    //Botones
    public Button backButton, nextButton, backGameButton,
        registerButton, loginButton, onlineButton, newLocalGameButton,
        loadLocalGameButton;

    //Cuadros de texto
    public TextMeshProUGUI txt1, txt2, txt3, conErr;

    //Jugador
    GameObject player;
    public GameObject playerPrefs;

    #endregion

    void Start()
    {
        /*
         * Obtenemos el GameObject referente al jugador
         */
        player = GameObject.FindGameObjectWithTag("Player");
        player.AddComponent<Player>();

    }

    #region Metodos del menu de opciones

    //Funcion para salir de la aplicacion

    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("Saliendo...");

    }

    #endregion

    #region Metodos para nueva partida local

    //Funcion para crear una nueva partida en local con un
    //nombre de usuario

    public void SetPlayerName()
    {
        string path = Application.persistentDataPath + "/" +
            newLocalGame.text.ToLower() + ".fun";
        if (File.Exists(path))
        {
            pPopupExists.SetActive(true);
        } else
        {
            CreateNew(player);
            GoToLevelMenu();
        }
    }

    public void RestartLocalGame()
    {
        CreateNew(player);
        GoToLevelMenu();
    }

    //Inicializacion de los atributos del jugador

    public void CreateNew(GameObject player)
    {
        //Nombre del usuario
        player.GetComponent<Player>().setUsername(newLocalGame.text.ToLower());
        //Nivel record
        player.GetComponent<Player>().setLevel(1);
        //Estrellas record
        player.GetComponent<Player>().setStars(0);
        //Puntuacion record
        player.GetComponent<Player>().setPoints(0);
        //Para saber despues que el juego es en local le damos un id nulo
        player.GetComponent<Player>().setId(0);
        //Puntuacion de cada nivel
        player.GetComponent<Player>().InitStats();
        //Guardamos la informacion en el fichero
        player.GetComponent<Player>().SavePlayer();
    }

    public void VerifyInputsNewLocal()
    {
        newLocalGameButton.interactable = (
            newLocalGame.text.Length >= 4 &&
            newLocalGame.text.Length <= 15
        );
    }

    #endregion

    #region Metodos para cargar partida local

    //Funcion para cargar una partida en local con un
    //nombre de usuario

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/"
            + loadLocalGame.text.ToLower() + ".fun";
        if (File.Exists(path))
        {
            player.GetComponent<Player>().LoadPlayer(loadLocalGame.text.ToLower());
            GoToLevelMenu();
        } else
        {
            pPopupNotFound.SetActive(true);
        }
    }

    public void VerifyInputsLoadLocal()
    {
        loadLocalGameButton.interactable = (
            loadLocalGame.text.Length >= 4 &&
            loadLocalGame.text.Length <= 15
        );
    }

    #endregion

    #region Funciones para controlar el registro

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        string url = "http://chenguinho.zapto.org/tdgame/phpUnity/register.php";
        string username = userRegistration.text.ToLower();
        

        WWWForm form = new WWWForm();
        form.AddField("username", userRegistration.text);
        form.AddField("password", passwordRegistration.text);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Habria que hacer un pop-up
                Debug.Log(www.error);
            }
            else
            {
                if(int.Parse(www.downloadHandler.text) > 0)
                {
                    //Nombre del usuario
                    player.GetComponent<Player>().setUsername(username);
                    //Nivel record
                    player.GetComponent<Player>().setLevel(1);
                    //Estrellas record
                    player.GetComponent<Player>().setStars(0);
                    //Puntuacion record
                    player.GetComponent<Player>().setPoints(0);
                    //Para marcar que juega online le damos su id
                    player.GetComponent<Player>().setId(int.Parse(www.downloadHandler.text));
                    //Puntuacion de cada nivel
                    player.GetComponent<Player>().InitStats();

                    GoToLevelMenu();
                }
                else if(int.Parse(www.downloadHandler.text) == -1)
                {
                    //Habria que hacer un pop-up
                    Debug.Log("Usuario ya en el sistema");
                }
                else if (int.Parse(www.downloadHandler.text) == -2)
                {
                    //Habria que hacer un pop-up
                    Debug.Log("Se produjo un error inesperado");
                }
            }
        }
    }

    public void VerifyInputs()
    {
        registerButton.interactable = (
            userRegistration.text.Length >= 4 &&
            userRegistration.text.Length <= 12 &&
            passwordRegistration.text.Length >= 4 &&
            passwordRegistration.text.Length <= 12
        );
    }

    #endregion

    #region Funciones para controlar el log in

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        string url = "http://chenguinho.zapto.org/tdgame/phpUnity/login.php";

        WWWForm form = new WWWForm();
        string username = signInUser.text.ToLower();
        string password = signInPwd.text;

        form.AddField("username", signInUser.text);
        form.AddField("password", signInPwd.text);


        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Habria que poner pop up
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text != "-1")
                {
                    /*
                     * Invocamos a la otra corrutina pasandole como
                     * unica informacion el id del usuario obtenido
                     * a traves de la consulta anterior
                     */

                    player.GetComponent<Player>().InitStats();
                    player.GetComponent<Player>().setUsername(username);
                    StartCoroutine(GetUserData(int.Parse(www.downloadHandler.text)));
                } else if(int.Parse(www.downloadHandler.text) == -1)
                {
                    //Habria que hacer pop-up
                    Debug.Log("No se ha reconocido al usuario");
                }
                
            }
        }
    }

    IEnumerator GetUserData(int id)
    {
        string url = "http://chenguinho.zapto.org/tdgame/phpUnity/getUserData.php";

        WWWForm form = new WWWForm();
        form.AddField("id", id);

        using(UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                //Habria que poner pop up
                Debug.Log(www.error);
            } else
            {
                string var = www.downloadHandler.text;
                if(var.Length < 3)
                {
                    //Habria que poner pop-up
                    Debug.Log("Error en la obtencion del id " + www.downloadHandler.text);
                } else
                {
                    string[] values = var.Split(","[0]);
                    int level = int.Parse(values[0]);
                    int points = int.Parse(values[1]);
                    int stars = int.Parse(values[2]);
                    
                    player.GetComponent<Player>().setLevel(level);
                    player.GetComponent<Player>().setPoints(points);
                    player.GetComponent<Player>().setStars(stars);
                    player.GetComponent<Player>().setId(id);
                    //Estadisticas de cada nivel
                    AssignOnlineLevelStats(level, values, player.GetComponent<Player>());

                    GoToLevelMenu();
                }
            }
        }
    }

    public void VerifyInputsLogin()
    {
        loginButton.interactable = (
            signInUser.text.Length >= 4 &&
            signInUser.text.Length <= 12 &&
            signInPwd.text.Length >= 4 &&
            signInPwd.text.Length <= 12
        );
    }

    #endregion

    #region Funciones para la conexion

    public void CallConnect()
    {
        StartCoroutine(Connect());
    }

    IEnumerator Connect()
    {
        string url = "http://chenguinho.zapto.org/tdgame/phpUnity/connect.php";


        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                conErr.text = "No se pudo conectar con el servidor: " + www.error;
                pPopUpConnect.SetActive(true);
            }
            else
            {
                pSelectGame.SetActive(false);
                pOnline.SetActive(true);
                backGameButton.gameObject.SetActive(true);
            }
        }
    }

    #endregion

    #region Manejo grafico del menu de ayuda

    public void MoveTextNext()
    {
        if (txt1.isActiveAndEnabled)
        {

            txt1.gameObject.SetActive(false);
            txt2.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);

        }

        else if (txt2.isActiveAndEnabled)
        {

            txt2.gameObject.SetActive(false);
            txt3.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);

        }

    }

    public void MoveTextBack()
    {

        if (txt2.isActiveAndEnabled)
        {

            txt2.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            txt1.gameObject.SetActive(true);

        }
        if (txt3.isActiveAndEnabled)
        {

            txt3.gameObject.SetActive(false);
            txt2.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);

        }

    }

    public void Home()
    {

        backButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        txt1.gameObject.SetActive(true);
        txt2.gameObject.SetActive(false);
        txt3.gameObject.SetActive(false);

    }

    #endregion

    //Manejo grafico de la opcion de volver atras

    public void BackGameButton()
    {

        if (pOnline.activeSelf)
        {

            backGameButton.gameObject.SetActive(false);
            pOnline.SetActive(false);
            pSelectGame.SetActive(true);

        }

        if (pLocal.activeSelf)
        {

            backGameButton.gameObject.SetActive(false);
            pLocal.SetActive(false);
            pSelectGame.SetActive(true);

        }

    }

    public void GoToLevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void GoToOptionsMenu()
    {
        playerPrefs.GetComponent<PlayerPrefs>().setSource("MainMenu");
        SceneManager.LoadScene("ConfigMenu");
    }

    public void AssignOnlineLevelStats(int l, string[] values, Player p)
    {
        for(int i = 1; i <= l; i++)
        {
            p.setLevelStats(
                i,
                int.Parse(values[i + (i + 1)]),
                int.Parse(values[i + (i + 2)])
            );
        }
    }
}
