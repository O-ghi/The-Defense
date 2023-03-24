using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int currentLevel;
    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public GameObject warningUI;
    public GameObject backButton;

    public GameObject pauseButton;
    public GameObject resumeButton;

    public AudioSource audioSource;
    public static Wave[] waves;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene!");
            return;
        }
        instance = this;
    }
    void Start()
    {
        GameIsOver = false;
        audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        backButton.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
        backButton.SetActive(true);
        Debug.Log($"currentLevel " + currentLevel);

        if (PlayerData.level < currentLevel)
        {
            Debug.Log($"currentLevel " + currentLevel + PlayerData.level);
            PlayerData.level = currentLevel;
            SaveManager.SaveGame();
        }
    }

    public void Pause()
    {
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);

        Time.timeScale = 0f;

    }

    public void Resume()
    {
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);

        Time.timeScale = 1f;
    }

    /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveManager.SaveGame();
    }
}
