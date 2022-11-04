using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] private Vector3 spawnTop, spawnBottom;
    [SerializeField] private float oldLadySpeed, kidSpeed, adultSpeed;
    [SerializeField] private LayerMask keyMask;

    [SerializeField] private List<GameObject> speedType;
    [SerializeField] private GameObject ground;
    [SerializeField] private WordUpdater wu;

    private Animator anim;
    private bool isKid;

    private void OnEnable()
    {
        isKid = false;

        for (int i = 0; i < speedType.Count; i++)
        {
            speedType[i].SetActive(false);
        }
    }

    public void SpawnTop(Vector3 offset, bool spawnKid)
    {
        StopAllCoroutines();

        if (spawnKid)
        {
            transform.position = spawnTop;
            gameObject.SetActive(true);

            speedType[2].SetActive(true);
            anim = speedType[2].GetComponent<Animator>();
            StartCoroutine(KidWalk(kidSpeed));
            Invoke("ActiveFalse", 15);

            return;
        }

        transform.position = new Vector3(offset.x, spawnTop.y, spawnTop.z);
        gameObject.SetActive(true);
        transform.rotation = Quaternion.Euler(0, 180, 0);

        int type = Random.Range(0, 2);

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
        }

        Invoke("ActiveFalse", 15);
    }

    public void SpawnBottom(Vector3 offset, bool spawnKid)
    {
        StopAllCoroutines();

        if(spawnKid)
        {
            transform.position = spawnBottom;
            gameObject.SetActive(true);

            speedType[2].SetActive(true);
            anim = speedType[2].GetComponent<Animator>();
            StartCoroutine(KidWalk(kidSpeed));
            Invoke("ActiveFalse", 15);

            return;
        }

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
        }

        Invoke("ActiveFalse", 15);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            pm.Stun(1);

            // +1 to number of people ran over.
            Summarary.inst.peopleRanOverRound++;
            Summarary.inst.peopleRanOverTotal++;

            // Check if this is a kid, running over kid instantly grants you 0 stars for this word.
            if(isKid)
            {
                wu.RanOverKid();
            }

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

    private IEnumerator KidWalk(float speed)
    {
        isKid = true;
        anim.Play("Walk");
        char c = (char)('a' + Random.Range(0, 26));
        Vector3 key = ground.transform.Find(c.ToString()).transform.position;
        Vector3 target = new(key.x, transform.position.y, key.z);

        while (Vector3.Distance(transform.position, target) > 0.5f)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            yield return null;
        }

        anim.Play("Fall");

        Physics.Raycast(transform.position, Vector3.down, out var hit, 1, keyMask);
        if (hit.collider != null)
        {
            hit.transform.GetChild(0).gameObject.SetActive(false);
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

        Physics.Raycast(transform.position, Vector3.down, out var hit, 1, keyMask);
        if (hit.collider != null)
        {
            hit.transform.GetChild(0).gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
