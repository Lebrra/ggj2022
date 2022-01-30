using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // unique per level
    public static GameManager instance;

    bool firstSuccess = false;
    bool lost = false;

    public int myLevelIndex;
    public int nextLevelIndex;
    LevelStats myLevelStats;

    [Header("Scene Elements")]
    public GameObject pauseMenu;
    bool paused = false;

    public TMPro.TextMeshPro bestTimeText;
    [SerializeField]
    float gameTime = 0F;
    bool timerRunning = false;

    void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        if (SaveDataManager.instance)
        {
            myLevelStats = SaveDataManager.instance.GetLevelData(myLevelIndex);
            if (myLevelStats.time > 0 && bestTimeText) bestTimeText.text = FormatTime(myLevelStats.time);
            else bestTimeText?.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Save data disabled");
            bestTimeText?.gameObject.SetActive(false);
            myLevelStats = new LevelStats();
            myLevelStats.time = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (timerRunning) gameTime += Time.deltaTime;
    }

    public void AddToSuccess()
    {
        if (!firstSuccess) firstSuccess = true;
        else
        {
            // yay win
            Debug.Log("YOU WIN");

            SetTime();
            SceneLoader.instance?.LoadScene(nextLevelIndex);
        }
    }

    public void TriggerLose()
    {
        if (!lost)
        {
            lost = true;
            AddToDeathCounter();
            SceneLoader.instance?.ReloadScene();
        }
    }

    public void StartGame()
    {
        SetTime();
        SceneLoader.instance?.LoadScene(nextLevelIndex);
    }

    public void ToTitle()
    {
        SetTime();

        SceneLoader.instance?.LoadScene(nextLevelIndex);
        StartCoroutine(AudioManager.inst.FadeSongOut(2.2f, .5f, 1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SetTime();
        StartCoroutine(AudioManager.inst.FadeSongOut(2.2f, 0, 0));
        SceneLoader.instance?.LoadScene(0);
        Debug.Log("Play credits here");
    }

    public void Pause()
    {
        paused = !paused;
        pauseMenu?.SetActive(paused);

        if (paused) Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        Pause();
        AddToDeathCounter();
        SceneLoader.instance?.ReloadScene();
    }

    public void ToMain()
    {
        Time.timeScale = 1;
        Pause();
        SceneLoader.instance?.LoadScene(2);
    }

    public void SetTimerRunning(bool enable)
    {
        timerRunning = enable;
    }

    public void SetTime()
    {
        SetTimerRunning(false);
        if (myLevelStats.time < 0 || myLevelStats.time > gameTime)
        {
            Debug.Log("NEW HIGH SCORE: " + gameTime);
            bestTimeText?.gameObject.SetActive(true);
            if (bestTimeText) bestTimeText.text = FormatTime(gameTime);

            myLevelStats.time = gameTime;
            SaveDataManager.instance?.SaveLevelData(myLevelIndex, myLevelStats);
        }
    }

    public void AddToDeathCounter()
    {
        myLevelStats.deathCounter++;
        SaveDataManager.instance?.SaveLevelData(myLevelIndex, myLevelStats);
    }

    string FormatTime(float time)   // in seconds
    {
        int minutes = Mathf.FloorToInt(time / 60);
        string minutesStr = "";
        if (minutes > 0) minutesStr = minutes.ToString() + ":";
        float remaining = time - (minutes * 60F);

        return minutesStr + (Mathf.Round(remaining * 100F) / 100F).ToString();
    }
}
