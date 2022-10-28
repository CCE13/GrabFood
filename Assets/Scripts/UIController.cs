using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public abstract class UIController: MonoBehaviour
{
    public static event Action Restarted;
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Restarted?.Invoke();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
