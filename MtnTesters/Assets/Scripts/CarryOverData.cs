using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarryOverData : MonoBehaviour {
    public static int playerScore;
    public static int playerLives;
    public static float gameTime;
    public Text timeCounter;
    public Text scoreCounter;

    // Use this for initialization
    void Start () {
        playerLives = 3;
        playerScore = 0;
        gameTime = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        scoreCounter.text = "Score:\n" + playerScore;
        gameTime = 1 * Time.time;
        timeCounter.text = "Time:\n" + gameTime.ToString("0:00");
    }

    private void OnTriggerEnter(Collider levelSwitch)
    {
        if (levelSwitch.gameObject.CompareTag("LevelSwitch"))
        {
            SceneManager.UnloadSceneAsync(0);
            SceneManager.LoadSceneAsync(1);
        }
    }
}
