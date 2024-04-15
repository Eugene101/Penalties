using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine.SceneManagement;

public class PauseMenPen : MonoBehaviour
{
    public GameObject pauseMenu;
    //public Button sound;
    //public Button restart;
    public Button quitgame;
    public Button returnButton;
    public Button mainMenu;
    public static bool isMenu1on;
    bool changed;
    //public Text Soundon;
    //public Text Soundoff;
    public static bool soundon = true;
    bool timeshift = true;
    float time = 0f;
    public static float unscaledTime = 1;
    public GameObject LaserLeft;
    public GameObject LaserRight;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        LaserLeft.gameObject.SetActive(false);
        LaserRight.gameObject.SetActive(false);
        //InvokeRepeating("CheckNewKills", 1f, 1f);
        InvokeRepeating("ResetTimeShift", 1f, unscaledTime);
    }
    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.JoystickButton1) && !isMenu1on && timeshift))
        {
            timeshift = false;
            Time.timeScale = 0f;
            isMenu1on = true;
            pauseMenu.gameObject.SetActive(true);
            LaserLeft.gameObject.SetActive(true);
            LaserRight.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton1) && isMenu1on && timeshift)
        {
            timeshift = false;
            Time.timeScale = 1f;
            isMenu1on = false;
            pauseMenu.gameObject.SetActive(false);
            LaserLeft.gameObject.SetActive(false);
            LaserRight.gameObject.SetActive(false);

        }

        if (!timeshift)
        {
            time += 0.02f;
            if (time > 1)
            {
                timeshift = true;
                //time = 0;
            }
        }



        //if (changed)
        //{
        //    PlayerPrefs.SetInt("mySound", soundon ? 1 : 0);

        //    if (soundon)
        //    {
        //        Soundon.gameObject.SetActive(true);
        //        Soundoff.gameObject.SetActive(false);
        //        AudioListener.pause = false;
        //    }

        //    else if (!soundon)
        //    {
        //        Soundon.gameObject.SetActive(false);
        //        Soundoff.gameObject.SetActive(true);
        //        AudioListener.pause = true;
        //    }

        //    changed = false;
        //}

    }

    public void Exitapp()
    {
        Application.Quit();
    }

 

    //public void SoundCh()
    //{
    //    soundon = !soundon;
    //    changed = true;
    //}

    public void ReturnToGame()
    {
        Time.timeScale = 1f;
        isMenu1on = false;
        pauseMenu.gameObject.SetActive(false);
        LaserLeft.gameObject.SetActive(false);
        LaserRight.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Penalties");
        SceneManager.LoadScene("MainMenu");
    }

    //public void RestartLevel()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.UnloadSceneAsync("Stadio");
    //    SceneManager.LoadScene("Stadio");
    //}


}
