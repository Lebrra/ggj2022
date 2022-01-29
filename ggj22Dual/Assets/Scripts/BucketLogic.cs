using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallLogic>())
        {
            other.GetComponent<BallLogic>().SetSuccess();
        }
    }
}
