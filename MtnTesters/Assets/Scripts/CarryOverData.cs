using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryOverData : MonoBehaviour {
    public GameObject dirLight;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter(Collider levelSwitch)
    {
        if (levelSwitch.gameObject.CompareTag("LevelSwitch"))
        {
            SceneManager.UnloadSceneAsync(0);
            SceneManager.LoadSceneAsync(1);
            DontDestroyOnLoad(dirLight);
        }
    }
}
