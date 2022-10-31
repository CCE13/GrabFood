using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private float y;
    private void Start()
    {
        y = transform.position.y;
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0));
        transform.position = new Vector3(transform.position.x, y + Mathf.PingPong(Time.time / 10,0.2f), transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            WordUpdater.inst.UpdateTime();

            // Visual Feedback, but for now just turn off.
            // +10 points.
            gameObject.SetActive(false);
        }
    }
}
