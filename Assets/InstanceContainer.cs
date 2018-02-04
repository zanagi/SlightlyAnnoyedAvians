using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceContainer : MonoBehaviour {

    private static InstanceContainer Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
