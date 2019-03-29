using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGiz : MonoBehaviour {

    public GameObject check;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
