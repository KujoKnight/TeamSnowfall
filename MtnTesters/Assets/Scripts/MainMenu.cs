using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MM;
    public GameObject HTP;
    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        HTP.SetActive(true);
        MM.SetActive(false);
    }

    public void ReturnToMM()
    {
        MM.SetActive(true);
        HTP.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
