using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public bool stopTime;
    public float timeChanges;

    private float hour;
    private float min;
    private float time;

    public static event Action<int, int> SetTime;
    // Update is called once per frame

    void Update()
    {
        if (stopTime) return;
        time += Time.deltaTime * timeChanges;
        min = time % 60;
        hour = (time / 60f) % 60;
        SetTime?.Invoke((int)hour, (int)min);
    }
}
