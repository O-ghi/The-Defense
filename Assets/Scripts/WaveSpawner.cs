using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;
    private int TotalEnemy = 0;

    public Wave[] waves;

    public List<Transform> spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;

    float timer = 0;
    private Wave wave;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        waves = GameManager.waves;
    }
    void Update()
    {

        if (EnemiesAlive > 0)
        {
            waveCountdownText.transform.parent.gameObject.SetActive(false);
            return;
        }
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            GameManager.instance.warningUI.SetActive(false);

            PlayerStats.Rounds++;
            wave = waves[waveIndex];
            foreach (TypeEnemy enemy in wave.EnemyList)
            {
                TotalEnemy += enemy.count;
                EnemiesAlive = TotalEnemy;
            }
            countdown = timeBetweenWaves;
            waveIndex++;

            return;
        }
        if (PlayerStats.Rounds == 2)
        {
            GameManager.instance.warningUI.SetActive(true);
        }
        waveCountdownText.transform.parent.gameObject.SetActive(true);

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (TotalEnemy > 0)
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        timer += Time.fixedDeltaTime;
        // Debug.Log($"timer " + Math.Round(timer, 2));

        foreach (TypeEnemy enemy in wave.EnemyList)
        {
            if (Math.Round(timer, 2) % enemy.rate == 0)
            {
                if (enemy.count > 0)
                {
                    SpawnEnemy(enemy.enemy);
                    enemy.count--;
                    TotalEnemy--;
                }
            }
        }

    }

    void SpawnEnemy(GameObject enemy)
    {
        System.Random random = new System.Random();
        int point = random.Next(0, 3);
        GameObject gameObject = Instantiate(enemy, spawnPoint[point].position, spawnPoint[point].rotation);
        gameObject.GetComponent<Enemy>().target = Waypoints.points[point];
    }

    public void SetWave(Wave[] waves)
    {
        this.waves = waves;
    }
}
