using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] private Vector3 spawnTop, spawnBottom;
    [SerializeField] private float oldLadySpeed, kidSpeed, adultSpeed;

    [SerializeField] private List<GameObject> speedType;

    private Animator anim;

    public void SpawnTop(Vector3 offset)
    {
        StopAllCoroutines();
        transform.position = new Vector3(offset.x, spawnTop.y, spawnTop.z);
        gameObject.SetActive(true);
        transform.rotation = Quaternion.Euler(0, 180, 0);

        int type = Random.Range(0, 3);

        switch (type)
        {
            case 0:
                speedType[type].SetActive(true);
                anim = speedType[type].GetComponent<Animator>();
                StartCoroutine(Move(Vector3.back, oldLadySpeed));
                break;
            case 1:
                speedType[type].SetActive(true);
                anim = speedType[type].GetComponent<Animator>();
                StartCoroutine(Move(Vector3.back, adultSpeed));
                break;
            case 3:
                speedType[type].SetActive(true);
                anim = speedType[type].GetComponent<Animator>();
                StartCoroutine(Move(Vector3.back, kidSpeed));
                break;
        }

        Invoke("ActiveFalse", 15);
    }

    public void SpawnBottom(Vector3 offset)
    {
        StopAllCoroutines();
        transform.position = new Vector3(offset.x, spawnBottom.y, spawnBottom.z);
        gameObject.SetActive(true);

        int type = Random.Range(0, 3);

        switch (type)
        {
            case 0:
                speedType[type].SetActive(true);
                anim = speedType[type].GetComponent<Animator>();
                StartCoroutine(Move(Vector3.forward, oldLadySpeed));
                break;
            case 1:
                speedType[type].SetActive(true);
                anim = speedType[type].GetComponent<Animator>();
                StartCoroutine(Move(Vector3.forward, adultSpeed));
                break;
            case 3:
                speedType[type].SetActive(true);
                anim = speedType[type].GetComponent<Animator>();
                StartCoroutine(Move(Vector3.forward, kidSpeed));
                break;
        }

        Invoke("ActiveFalse", 8);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Stun(1);
            StopAllCoroutines();
            anim.Play("Death");

            Invoke("ActiveFalse", 1f);
        }
    }

    private IEnumerator Move(Vector3 dir, float speed)
    {
        anim.Play("Walk");
        Vector3 target = transform.position + (dir * 19.5f);

        while (Vector3.Distance(transform.position, target) > 0.8f)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (dir * 19.5f), speed);
            yield return null;
        }
    }

    private void ActiveFalse()
    {
        for (int i = 0; i < speedType.Count; i++)
        {
            speedType[i].SetActive(false);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        CancelInvoke();
        gameObject.SetActive(false);
    }
}
