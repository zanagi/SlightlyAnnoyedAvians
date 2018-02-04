using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle, Animation
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private float force = 5.0f, defaultArrowDist = 2.0f;
    private GameState state;

    public int avianCount = 3;
    [SerializeField]
    private Avian avianPrefab;

    private Avian selectedAvian;
    private Transform arrowTransform;

    private float launchTimer;

    private void Start()
    {
        Instance = this;
        arrowTransform = transform.Find("Arrow");
        arrowTransform.gameObject.SetActive(false);
        CreateAvian();
    }

    // Update is called once per frame
    void Update ()
    {
        if(state == GameState.Animation)
        {
            CheckAnimationEnd();
            return;
        }
        CheckTouch();
        CheckCameraMove();
	}

    private void CheckTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckAvianTouch();
        }
        else if(selectedAvian)
        {
            CheckAvianDrag();
        }
    }

    private void CheckCameraMove()
    {
        GameCamera.Instance.CheckMove();
    }

    private void CheckAnimationEnd()
    {
        launchTimer += Time.deltaTime;

        if (launchTimer <= 2.0f)
            return;

        if(selectedAvian.Stopped())
        {
            GameCamera.Instance.Reset();
            Destroy(selectedAvian.gameObject);
            selectedAvian = null;
            state = GameState.Idle;
            launchTimer = 0;
            avianCount--;

            if(avianCount == 0)
            {
                // TODO: end;
                Debug.Log("End...");
            } else
            {
                CreateAvian();
            }
        }
    }

    private void CreateAvian()
    {
        Instantiate(avianPrefab);
    }

    private void CheckAvianTouch()
    {
        var hit = Physics2D.Raycast(
            new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

        if (hit.collider)
        {
            selectedAvian = hit.collider.GetComponentInParent<Avian>();
            GameCamera.Instance.target = selectedAvian.transform;
            arrowTransform.position = selectedAvian.transform.position;
        }
    }

    private void CheckAvianDrag()
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
                        state = GameState.Animation;
                    }
                }
                return;
            }
        }
        arrowTransform.gameObject.SetActive(false);
    }

    private Vector2 AvianMouseDelta()
    {
        return selectedAvian.transform.position
          - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
