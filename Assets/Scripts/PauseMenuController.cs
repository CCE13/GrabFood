using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : UIController
{
    public static bool isPaused;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    private void Update()
    {
        //if P is pressed, pause or unpause the game based on the current state.
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                
            }
        }
    }
    //unpauses the game
    private void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //pauses the game
    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

}
