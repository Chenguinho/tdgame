using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public GameManager gameManager;

    [HideInInspector]
    public static int enemiesAlive = 0;
    public static int enemiesCount = 0;

    [Header("Oleadas")]
    public Wave[] waves;


    [Header("Tiempo entre oleadas")]
    public float time = 5f;

    [Header("Unity")]
    public Transform spawnPoint;
    public TextMeshProUGUI countdownText, progressText;

    private float countdown = 5f;
    [HideInInspector]
    public int waveIndex = 0;
    private Wave w;
    private bool finished = true;


    void Start()
    {
        enemiesAlive = 0;
        enemiesCount = 0;
        w = waves[waveIndex];
        progressText.text = waveIndex + "/" + waves.Length;
        countdownText.text = string.Format("{0:00.00}", countdown);
    }

    void Update()
    { 

        if (enemiesCount == w.count)
        {
            finished = true;
        }

        if (enemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length && CurrentGame.Lives > 0)
        {
            gameManager.WinGame();
            this.enabled = false;
            return;
        }

        if (countdown <= 0f && !GameManager.gameEnded)
        {
            progressText.text = waveIndex + 1 + "/" + waves.Length;
            enemiesCount = 0;
            StartCoroutine(SpawnWave());
            countdown = time;
            finished = false;
            return;
        }

        if (!finished)
        {
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        countdownText.text = string.Format("{0:00.00}", countdown);

    }

    IEnumerator SpawnWave()
    {

        w = waves[waveIndex];

        for (int i = 0; i < w.count; i++)
        {
            SpawnEnemy(w.enemy);
            yield return new WaitForSeconds(1f / w.rate);
        }

        waveIndex++;

    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
        enemiesCount++;
    }

}
