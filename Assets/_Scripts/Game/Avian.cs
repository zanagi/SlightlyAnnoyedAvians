using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avian : MonoBehaviour {

    public Rigidbody2D rBody { get; private set; }
    private bool touched;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
        rBody.Sleep();
	}
}
