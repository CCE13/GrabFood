using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    public UnityEvent events;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            events.Invoke();
        }
    }
}
