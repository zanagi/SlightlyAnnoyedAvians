using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var avian = collision.GetComponent<Avian>();

        if(avian)
        {
            // TODO:
        }
    }
}
