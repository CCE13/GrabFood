using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] private Vector3 spawnTop, spawnBottom;
    [SerializeField] private float travelDuration = 6;
    [SerializeField] private LayerMask keyMask;

    public GameObject currentKeyOn;
    private GameObject previousKeyOn;

    public void SpawnTop(Vector3 offset)
    {
        transform.position = new Vector3(offset.x, spawnTop.y, spawnTop.z);
        gameObject.SetActive(true);
        StartCoroutine(Move(Vector3.back));

        Invoke("ActiveFalse", 10);
    }

    public void SpawnBottom(Vector3 offset)
    {
        transform.position = new Vector3(offset.x, spawnBottom.y, spawnBottom.z);
        gameObject.SetActive(true);
        StartCoroutine(Move(Vector3.forward));

        Invoke("ActiveFalse", 8);
    }

    private void Update()
    {
        //currentKeyOn = CurrentKeyOn();

        //if (!currentKeyOn) return;
        //currentKeyOn.transform.GetChild(0).gameObject.SetActive(false);

        //if ((currentKeyOn != previousKeyOn|| currentKeyOn == null) && previousKeyOn)
        //{
        //    previousKeyOn.transform.GetChild(0).gameObject.SetActive(true);
        //}

        //previousKeyOn = currentKeyOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Stun(1);
        }
    }

    private IEnumerator Move(Vector3 dir)
    {
        float timePassed = 0;
        float timeToTake = travelDuration;
        while (timePassed < timeToTake)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (dir * 19.5f), 0.05f);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

    private GameObject CurrentKeyOn()
    {
        Physics.Raycast(transform.position, Vector3.down, out var hit, 1, keyMask);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
