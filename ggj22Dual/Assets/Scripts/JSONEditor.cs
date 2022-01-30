using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JSONEditor
{ 
    /// <summary>
    /// If JSON save data exists, pull and load it. Else make new save data
    /// </summary>
    public static SaveData JSONToSaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/gamedata.json"))
        {
            // get from file
            var file = File.ReadAllText(Application.persistentDataPath + "/gamedata.json");
            SaveData data = JsonUtility.FromJson<SaveData>(file);
            return data;
        }
        else
        {
            // form default data
            return new SaveData();
        }
    }

    /// <summary>
    /// Convert SaveData to JSON and save to persistentDataPath
    /// </summary>
    public static string SaveDataToJSON(SaveData data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/gamedata.json", json);

        return json;
    }

    public static SaveData DeleteSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamedata.json"))
        {
            // get from file
            File.Delete(Application.persistentDataPath + "/gamedata.json");
        }
        // form default data
        return new SaveData();
    }
}

[System.Serializable]
public class SaveData
{
    public List<LevelStats> allLevelData;

    public SaveData()
    {
        allLevelData = new List<LevelStats>();
    }
}

[System.Serializable]
public class LevelStats
{
    public float time = -1F;
    public int deathCounter = 0;
}