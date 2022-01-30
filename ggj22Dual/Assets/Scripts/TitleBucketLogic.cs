using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBucketLogic : MonoBehaviour
{
    public int option;
    public GameObject particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallLogic>())
        {
            AudioManager.inst.PlaySuccess(0);
            Instantiate(particles, other.transform.position, Quaternion.identity);
            other.GetComponent<BallLogic>().SetChoice(option);
        }
    }
}
