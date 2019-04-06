using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour {

    private GameController gc;

	// Use this for initialization
    // Spawns the player at the first checkpoint when the scene is started
	void Start () {
        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        transform.position = gc.lastCheckPointPos;
	}
	
	// Respawns the player at the most recent checkpoint when they touch a kill volume
	void OnTriggerEnter (Collider col) {
        if (col.gameObject.CompareTag("KillVolume"))
        {
            CarryOverData.playerLives--;
            transform.position = gc.lastCheckPointPos;
        }
	}
}
