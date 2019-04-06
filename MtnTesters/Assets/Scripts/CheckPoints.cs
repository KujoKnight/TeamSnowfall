using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {

    private GameController gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
    }

    //Sets the current checkpoint to the one the player has most recently collided with
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            gc.lastCheckPointPos = transform.position;
            Debug.Log("Checkpoint changed to " + gc.lastCheckPointPos);
        }
    }
}
