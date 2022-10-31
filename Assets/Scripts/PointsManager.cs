using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager inst;
    public static event Action<int> SetPoints;
    private int points;

    private void Awake()
    {
        inst = this;
    }

    public void AddPoints(int amt)
    {
        points += amt;
        SetPoints.Invoke(points);
    }
}
