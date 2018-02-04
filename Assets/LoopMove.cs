using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that moves its gameobject along x-axis and teleports it back to its starting point once its passed the declared maximum
/// </summary>
public class LoopMove : MonoBehaviour {

    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float endX = 15;
    [SerializeField]
    private float startX = -15;
    
	void FixedUpdate ()
    {
        transform.localPosition += Vector3.right * speed * Time.fixedDeltaTime;

        if(transform.localPosition.x >= endX)
        {
            transform.localPosition = new Vector3(startX, transform.localPosition.y);
        }
	}
}
