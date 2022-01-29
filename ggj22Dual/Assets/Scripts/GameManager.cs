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

    void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
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
}
