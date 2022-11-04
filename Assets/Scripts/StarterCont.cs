using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterCont : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private TimeManager tm;
    [SerializeField] private WordUpdater wu;

    public GameObject ground;

    public void StartGame()
    {
        for (int i = 0; i < ground.transform.childCount; i++)
        {
            ground.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
        }
        pm.started = true;
        pm._isMoving = false;
        pm._isStunned = false;
        tm.stopTime = false;
        wu.NextWord();
        FeverMode.inst.paused = false;
    }

    public void StopGame()
    {
        for (int i = 0; i < ground.transform.childCount; i++)
        {
            ground.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
        }
        pm.started = false;
        tm.stopTime = true;
        wu.ClearPools();
    }
}
