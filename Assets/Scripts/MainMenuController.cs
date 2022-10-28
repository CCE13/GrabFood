using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuController : UIController
{
    public string sceneToLoad;
    public AudioMixer masterMixer;
    private Slider volumeSlider;

    private const string C_VOLUME = "volume";
    private void Start()
    {
        volumeSlider = GetComponentInChildren<Slider>();

        //gets the volume from the player prefs and sets the volume accordingly
        var value = PlayerPrefs.GetFloat(C_VOLUME);
        var volumeToSet = Mathf.Log10(value) * 20;

        volumeSlider.value = value;       
        masterMixer.SetFloat(C_VOLUME, volumeToSet);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void VolumeChange(float value)
    {
        var volumeToSet = Mathf.Log10(value) * 20;
        masterMixer.SetFloat(C_VOLUME, volumeToSet);
        volumeSlider.value = value;
        PlayerPrefs.SetFloat(C_VOLUME, value);
    }
}
