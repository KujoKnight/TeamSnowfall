using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {

    public Transform respawnPoint;
    public KillVolume killVolume;

	// Use this for initialization
	void Start () {

        killVolume = GetComponent<KillVolume>();

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            killVolume.respawnPoint = respawnPoint;
        }
    }
}
