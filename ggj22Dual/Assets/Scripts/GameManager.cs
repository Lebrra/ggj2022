using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // unique per level
    public static GameManager instance;

    bool firstSuccess = false;
    bool lost = false;

    public int nextLevelIndex;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    bool paused = false;

    void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    public void AddToSuccess()
    {
        if (!firstSuccess) firstSuccess = true;
        else
        {
            // yay win
            Debug.Log("YOU WIN");
            SceneLoader.instance?.LoadScene(nextLevelIndex);
        }
    }

    public void TriggerLose()
    {
        if (!lost)
        {
            lost = true;
            SceneLoader.instance?.ReloadScene();
        }
    }

    public void StartGame()
    {
        SceneLoader.instance?.LoadScene(nextLevelIndex);
    }

    public void ToTitle()
    {
        SceneLoader.instance?.LoadScene(nextLevelIndex);
        StartCoroutine(AudioManager.inst.FadeSongOut(2.2f, .5f, 1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneLoader.instance?.LoadScene(0);
        Debug.Log("Play credits here");
    }

    public void Pause()
    {
        paused = !paused;
        pauseMenu?.SetActive(paused);

        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        Pause();
        SceneLoader.instance?.ReloadScene();
    }
}
