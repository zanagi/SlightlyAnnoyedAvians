using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle, Animation
}

public class GameManager : MonoBehaviour {

    [SerializeField]
    private float force = 5.0f;
    private GameState state;
    private Avian selectedAvian;
    
	// Update is called once per frame
	void Update ()
    {
        CheckTouch();
	}

    private void CheckTouch()
    {
        if (state != GameState.Idle)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(
                new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

            if (hit.collider)
            {
                Debug.Log("Selected.");
                selectedAvian = hit.collider.GetComponentInParent<Avian>();
            }
        } else if(Input.GetMouseButtonUp(0) && selectedAvian)
        {
            Debug.Log("Released.");
            var flightForce = (Input.mousePosition - selectedAvian.transform.position) * force;
            selectedAvian.rBody.AddForce(flightForce);
        }
    }
}
