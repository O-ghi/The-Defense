using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelPanel : MonoBehaviour
{
    int CompleteLevel;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        PlayerData.level = SaveManager.LoadGame();
        CompleteLevel = PlayerData.level;
        Debug.Log($"CompleteLevel " + CompleteLevel);
        for (int i = 0; i < CompleteLevel; i++)
        {
            Transform child = transform.GetChild(i);
            child.Find("Image").gameObject.SetActive(true);
        }
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.GameIsOver = false;
        WaveData waveData = transform.GetComponent<WaveData>();
        Debug.Log($"");
        GameManager.waves = waveData.Level1;
        GameManager.currentLevel = 1;
        PlayerStats.SetStats(80, 10);
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.GameIsOver = false;
        WaveData waveData = transform.GetComponent<WaveData>();
        GameManager.waves = waveData.Level2;
        GameManager.currentLevel = 2;

        PlayerStats.SetStats(50, 12);
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.GameIsOver = false;
        WaveData waveData = transform.GetComponent<WaveData>();
        GameManager.waves = waveData.Level3;
        GameManager.currentLevel = 3;

        PlayerStats.SetStats(80, 15);
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.GameIsOver = false;
        WaveData waveData = transform.GetComponent<WaveData>();
        GameManager.waves = waveData.Level4;
        GameManager.currentLevel = 4;

        PlayerStats.SetStats(80, 15);
    }

}
