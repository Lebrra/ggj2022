using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketLogic : MonoBehaviour
{
    public GameObject particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallLogic>())
        {
            other.GetComponent<BallLogic>().SetSuccess();
            AudioManager.inst.PlaySuccess(0);
            Instantiate(particles, other.transform.position, Quaternion.identity);
        }
    }
}
