using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator buttonAnim;
    public Animator gateAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonAnim.SetTrigger("buttPress");
            gateAnim.SetTrigger("GateOpen");

            foreach(var balls in FindObjectsOfType<BallLogic>())
            {
                balls.transform.SetParent(null);
            }
        }
    }
}
