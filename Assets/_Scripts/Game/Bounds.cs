using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var avian = collision.GetComponentInParent<Avian>();

        if(avian)
        {
            avian.outOfBounds = true;
        }
    }
}
