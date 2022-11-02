using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterCont : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private TimeManager tm;
    [SerializeField] private WordUpdater wu;

    public void StartGame()
    {
        pm.started = true;
        tm.stopTime = false;
        wu.NextWord();
    }
}
