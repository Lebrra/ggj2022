using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLogic : MonoBehaviour
{
    int sequence = 0;

    public Animator fogAnim;
    public GameObject[] textsToCycle;

    private void Awake()
    {
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        if (sequence > 0) textsToCycle[sequence - 1].SetActive(false);
        textsToCycle[sequence].SetActive(true);
        fogAnim.SetTrigger("fogDown");

        yield return new WaitForSecondsRealtime(3.5F);

        if(sequence >= 2) SceneLoader.instance.LoadScene(1);
        else
        {
            fogAnim.SetTrigger("fogUp");
            yield return new WaitForSecondsRealtime(2.2F);

            sequence++;
            StartCoroutine(ChangeText());
        }
    }
}
