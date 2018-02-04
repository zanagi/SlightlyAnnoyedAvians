using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    [SerializeField]
    private int score;
    [SerializeField]
    private bool destroyParent = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var avian = collision.GetComponentInParent<Avian>();

        if(avian)
        {
            ScoreManager.score += score;
            Destroy(destroyParent ? transform.parent.gameObject : gameObject);
        }
    }
}
