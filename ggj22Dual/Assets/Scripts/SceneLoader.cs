using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public Animator fogAnim;

    void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;

        if (!fogAnim) Debug.LogWarning("Fog Animator not set", gameObject);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(RollInFog(() => SceneManager.LoadScene(sceneIndex)));
    }

    public void ReloadScene()
    {
        StartCoroutine(RollInFog(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)));
    }

    IEnumerator RollInFog(Action next)
    {
        fogAnim?.SetTrigger("fogUp");
        yield return new WaitForSecondsRealtime(2.2F);
        next();
    }
}
