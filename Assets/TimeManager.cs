using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private StarterCont sc;
    public bool stopTime;
    public float secondsPerHour;
    private float timeChanges, timeChangesOG;

    private float hour;
    private float min;
    private float time = 720;

    private float nextHour, finalHour;

    public static event Action<int, int> SetTime;
    public UnityEvent hourEnd, gameEnd;
    // Update is called once per frame

    private void Start()
    {
        // Calculate timeChange, 60/secondsPerHour
        timeChanges = 60 / secondsPerHour;
        timeChangesOG = timeChanges;
        nextHour = 780;
        finalHour = 1140;

        min = time % 60;
        hour = (time / 60f) % 60;
        SetTime?.Invoke((int)hour, (int)min);

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (stopTime) return;

        if(FeverMode.inst.isFever)
        {
            timeChanges = timeChangesOG / 2;
        }
        else
        {
            timeChanges = timeChangesOG;
        }

        time += Time.deltaTime * timeChanges;
        min = time % 60;
        hour = (time / 60f) % 60;
        SetTime?.Invoke((int)hour, (int)min);

        if(time >= finalHour)
        {
            stopTime = true;
            GameEnd();
            return;
        }

        if(time >= nextHour)
        {
            stopTime = true;
            HourEnd();
            Invoke("ShowSummary", 1.4f);
            return;
        }
    }

    public void HourEnd()
    {
        hourEnd.Invoke();

        nextHour += 60;
    }

    public void GameEnd()
    {
        gameEnd.Invoke();
        Invoke("ShowEnd", 1.4f);
    }

    private void ShowSummary()
    {
        // Show summary.
        Summarary.inst.RoundSummary();
    }

    private void ShowEnd()
    {
        EndScreen.inst.gameObject.SetActive(true);
        // Show endscreen.
        EndScreen.inst.EndGame();
    }
}
