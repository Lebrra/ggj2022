using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
    }

    void Update()
    {
        
    }
}
