using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text pointsText;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnEnable()
    {
        TimeManager.SetTime += SetTimer;
        PointsManager.SetPoints += SetPoints;
    }
    private void OnDisable()
    {
        TimeManager.SetTime -= SetTimer;
        PointsManager.SetPoints -= SetPoints;
    }

    private void SetTimer(int hour, int min)
    {
        timeText.text = $"{hour.ToString("00")}:{min.ToString("00")}";
    }

    private void SetPoints(int points)
    {
        pointsText.text = points.ToString();
    }
}
