using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public Vector3 lastCheckPointPos;
    public static int playerScore;
    public static int playerLives;
    public static float gameTime;
    public Text timeCounter;
    public Text scoreCounter;
    public Text lifeCounter;



    //Makes sure the GameController game object can only have one instance of itself active in the game
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(instance);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    void Start()
    {
        playerLives = 3;
        playerScore = 0;
        gameTime = 0;
    }

    void Update()
    {
        scoreCounter.text = "Score:\n" + playerScore;
        gameTime = 1 * Time.time;
        timeCounter.text = "Time:\n" + gameTime.ToString("0:00");
        lifeCounter.text = "Lives: " + playerLives + " / 3";
    }
}
