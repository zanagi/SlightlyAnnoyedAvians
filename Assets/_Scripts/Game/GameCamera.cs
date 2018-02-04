using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public static GameCamera Instance { get; private set; }
    public float minX, maxX = 15, minY = 1, maxY = 8, speed = 3;

    public Transform target;
    private Vector3 defaultPos;
    
	private void Start () {
        Instance = this;
        defaultPos = transform.localPosition;
	}

    private void Update()
    {
        if (!target)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
            return;
        }
        
        if (target.position.x >= transform.position.x)
            transform.position = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), 0.5f * (target.transform.position.y + transform.position.y), transform.position.z);
    }
    
    public void CheckMove()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime;
    }

    public void Reset()
    {
        transform.localPosition = defaultPos;
    }
}
