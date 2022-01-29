using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBucketLogic : MonoBehaviour
{
    public int option;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallLogic>())
        {
            other.GetComponent<BallLogic>().SetChoice(option);
        }
    }
}
