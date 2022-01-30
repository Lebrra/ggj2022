using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDeathCounter : MonoBehaviour
{
    public TMPro.TextMeshPro deathCountText;
    int deathCount = 0;

    void Start()
    {
        if (SaveDataManager.instance)
        {
            foreach(var data in SaveDataManager.instance.gameData.allLevelData)
            {
                deathCount += data.deathCounter;
            }
        }

        deathCountText.text = deathCount.ToString();
    }
}
