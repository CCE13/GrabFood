using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private StarterCont sc;
    public bool stopTime;
    public float secondsPerHour;
    private float timeChanges, timeChangesOG;

    private float hour;
    private float min;
    private float time = 660;

    private float nextHour;

    public static event Action<int, int> SetTime;
    // Update is called once per frame

    private void Start()
    {
        // Calculate timeChange, 60/secondsPerHour
        timeChanges = 60 / secondsPerHour;
        timeChangesOG = timeChanges;
        nextHour = 720;

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

        if(time >= nextHour)
        {
            stopTime = true;
            HourEnd();
            return;
        }
    }

    public void HourEnd()
    {
        // Call "Round End"
        sc.StopGame();

        // Show summary.
        Summarary.inst.RoundSummary();

        nextHour += 60;
    }
}
