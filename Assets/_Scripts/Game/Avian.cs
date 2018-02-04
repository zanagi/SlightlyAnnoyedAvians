using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avian : MonoBehaviour {

    public Rigidbody2D rBody { get; private set; }
    public bool outOfBounds;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
        rBody.Sleep();
	}
    
    public bool Stopped()
    {
        return rBody.velocity.sqrMagnitude <= 0.04f || outOfBounds;
    }
}
