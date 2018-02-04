using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour {

    [SerializeField]
    private float offset;

	// Use this for initialization
	void Start () {
        var animator = GetComponent<Animator>();
        animator.Play("Idle", 0, offset);
    }
}
