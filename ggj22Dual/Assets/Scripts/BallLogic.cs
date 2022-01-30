using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    // if ball falls too far, check for death/win
    bool success = false;
    int titleOption = -1;

    public bool hasTouchedGround = false;
    public bool fixForLevel2 = false;

    float yCheckVal = -10F;

    private void Awake()
    {
        if (fixForLevel2)
        {
            hasTouchedGround = false;
            GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(DelayGravity());
        }
    }

    IEnumerator DelayGravity()
    {
        yield return new WaitForSeconds(2.2F);
        transform.SetParent(null);
        GetComponent<Rigidbody>().useGravity = true;
    }

    void Update()
    {
        if (transform.position.y < yCheckVal)
        {
            gameObject.SetActive(false);
            switch (titleOption)
            {
                case 0:
                    GameManager.instance?.StartGame();
                    return;
                case 1:
                    GameManager.instance?.Credits();
                    return;
                case 2:
                    GameManager.instance?.QuitGame();
                    return;
                case 3:
                    GameManager.instance?.ToTitle();
                    return;
                default:
                    break;
            }

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

    public void SetSuccess()
    {
        success = true;
    }

    public void SetChoice(int choice)
    {
        titleOption = choice;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasTouchedGround)
        {
            AudioManager.inst.PlayBallSound(1);
            GameManager.instance?.SetTimerRunning(true);
            hasTouchedGround = true;
        }
    }
}
