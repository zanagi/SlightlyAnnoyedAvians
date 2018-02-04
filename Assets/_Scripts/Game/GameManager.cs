using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle, Animation
}

public class GameManager : MonoBehaviour {

    [SerializeField]
    private float force = 5.0f, defaultArrowDist = 2.0f;
    private GameState state;
    private Avian selectedAvian;

    private Transform arrowTransform;

    private void Start()
    {
        arrowTransform = transform.Find("Arrow");
        arrowTransform.gameObject.SetActive(false);
    }

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
                selectedAvian = hit.collider.GetComponentInParent<Avian>();
                arrowTransform.position = selectedAvian.transform.position;
            }
        }
        else if(selectedAvian)
        {
            var delta = AvianMouseDelta();
            var length = delta.magnitude;
            arrowTransform.gameObject.SetActive(true);

            if (length > 0.0f)
            {
                var angle = Vector3.SignedAngle(Vector3.right, delta / length, Vector3.forward);

                if (Mathf.Abs(angle) <= 90.0f)
                {
                    arrowTransform.localRotation = Quaternion.Euler(0, 0, angle);
                    arrowTransform.localScale = new Vector3(length / defaultArrowDist, arrowTransform.localScale.y);

                    if (Input.GetMouseButtonUp(0))
                    {
                        var flightForce = delta * force;

                        if (flightForce.sqrMagnitude > 0.0f)
                        {
                            arrowTransform.gameObject.SetActive(false);
                            arrowTransform.localScale = new Vector3(0, arrowTransform.localScale.y);
                            selectedAvian.rBody.AddForce(flightForce);
                            selectedAvian = null;
                        }
                    }
                    return;
                }
            }
            arrowTransform.gameObject.SetActive(false);
        }
    }

    private Vector2 AvianMouseDelta()
    {
        return selectedAvian.transform.position
          - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
