using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour {

    public Transform player;
    public Transform respawnPoint;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
        }
    }

}
