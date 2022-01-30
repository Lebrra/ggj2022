using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    public SaveData gameData;

    [Header("Cursors")]
    public Texture2D cursorDefault;
    public Texture2D cursorClicked;

    private void Awake()
    {
        if (instance) Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            gameData = JSONEditor.JSONToSaveData();
        }
    }

    public LevelStats GetLevelData(int sceneIndex)
    {
        if (gameData.allLevelData.Count > sceneIndex) return gameData.allLevelData[sceneIndex];
        else
        {
            // level data not saved yet
            while(gameData.allLevelData.Count <= sceneIndex)
            {
                gameData.allLevelData.Add(new LevelStats());
            }
            return gameData.allLevelData[sceneIndex];
        }
    }

    public void SaveLevelData(int sceneIndex, LevelStats data)
    {
        // if time not set or a better time, save 
        if (gameData.allLevelData[sceneIndex].time < 0 || gameData.allLevelData[sceneIndex].time > data.time)
        {
            gameData.allLevelData[sceneIndex].time = data.time;
        }
        // if out of date death counter, update it
        if (gameData.allLevelData[sceneIndex].deathCounter < data.deathCounter)
        {
            gameData.allLevelData[sceneIndex].deathCounter = data.deathCounter;
        }
        JSONEditor.SaveDataToJSON(gameData);
    }

    private void Update()
    {
        // cursor change
        if (Input.GetMouseButtonDown(0)) Cursor.SetCursor(cursorClicked, Vector2.zero, CursorMode.Auto);
        if (Input.GetMouseButtonUp(0)) Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
    }
}
