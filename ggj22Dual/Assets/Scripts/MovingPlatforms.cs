using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public GameObject startPath;
    public GameObject endPath;
    public float speed = 1f;

    private Vector3 startPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = startPath.transform.localPosition;
        endPos = endPath.transform.localPosition;
        StartCoroutine(LerpPlatforms(gameObject, endPos, speed));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.localPosition == endPos)
            StartCoroutine(LerpPlatforms(gameObject, startPos, speed));

        if (transform.localPosition == startPos)
            StartCoroutine(LerpPlatforms(gameObject, endPos, speed));
    }

    IEnumerator LerpPlatforms(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPos = obj.transform.localPosition;
        float time = 0f;

        while(obj.transform.localPosition != target)
        {
            obj.transform.localPosition = Vector3.Lerp(startPos, target, (time / Vector3.Distance(startPos, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
