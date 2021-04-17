using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float time = 5.3f;
    private float countdown = 2f;

    public TextMeshProUGUI countdownText;

    private int waveIndex = 0;

    void Update()
    {

        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = time;
        }

        countdown -= Time.deltaTime;
        countdownText.text = Mathf.Round(countdown + 1).ToString();

    }

    IEnumerator SpawnWave()
    {

        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

    }

    void SpawnEnemy()
    {

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

    }

}
