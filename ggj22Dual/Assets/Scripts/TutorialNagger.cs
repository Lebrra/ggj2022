using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNagger : MonoBehaviour
{
    public Animator imgAnim;
    RectTransform imgTransform;
    Vector3 offset = new Vector3(0F, 0F);

    private void Awake()
    {
        imgTransform = GetComponent<RectTransform>();
        StartCoroutine(DelayShow());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            imgAnim.SetBool("clicked", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            imgAnim.SetBool("clicked", false);
        }

        if (!Input.GetMouseButton(0))
        {
            imgTransform.position = Input.mousePosition + offset;
        }
    }

    IEnumerator DelayShow()
    {
        yield return new WaitForSeconds(4F);
        imgAnim.gameObject.SetActive(true);
    }
}
