using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public static GameCamera Instance { get; private set; }

    public Transform target;
    private Vector3 defaultPos;
    
	private void Start () {
        Instance = this;
        defaultPos = transform.localPosition;
	}

    private void Update()
    {
        if (!target)
            return;

        if (target.position.x >= transform.position.x)
            transform.position = new Vector3(target.position.x, 0.5f * (target.transform.position.y + transform.position.y), transform.position.z);
    }

    public void Reset()
    {
        transform.localPosition = defaultPos;
    }
}
