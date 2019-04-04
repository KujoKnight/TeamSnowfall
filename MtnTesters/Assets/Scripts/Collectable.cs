using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            col.gameObject.SetActive(false);
        }
    }
}
