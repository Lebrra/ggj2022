using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    // if ball falls too far, check for death/win
    bool success = false;

    float yCheckVal = -10F;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yCheckVal)
        {
            gameObject.SetActive(false);
            if (success)
            {
                GameManager.instance?.AddToSuccess();
            }
            else
            {
                // reset the level
                Debug.Log("YOU LOSE");
                GameManager.instance?.TriggerLose();
            }
        }
    }
}
