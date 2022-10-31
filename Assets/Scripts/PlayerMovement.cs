using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask keyMask;
    public GameObject currentKeyOn;
    private bool _isMoving;

    // Start is called before the first frame update
    private void Start()
    {
        currentKeyOn = CurrentKeyOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving) return;
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) return;

            var keyPressed = Input.inputString;
            Transform keyToGoTo = ground.transform.Find(keyPressed);
            MoveCheck(keyToGoTo);

            //KeyChecker key = currentKeyOn.GetComponent<KeyChecker>();
            //if (!key.keysAround.Contains(keyToGoTo.name)) return;
            //var pos = new Vector3(keyToGoTo.position.x, transform.position.y, keyToGoTo.position.z);
            //StartCoroutine(Move(pos,keyToGoTo.gameObject));
            
        }
    }

    private void MoveCheck(Transform keyToGoTo)
    {
        RaycastHit hit;
        Vector3 startRay = new Vector3(currentKeyOn.transform.position.x, transform.position.y, currentKeyOn.transform.position.z);
        Vector3 endRay = new Vector3(keyToGoTo.position.x, transform.position.y, keyToGoTo.position.z);

        if(keyToGoTo.position.x == currentKeyOn.transform.position.x ||
           keyToGoTo.position.z == currentKeyOn.transform.position.z)
        {
            if (Physics.Linecast(startRay, endRay, out hit))
            {
                if(hit.collider.CompareTag("Obstacle"))
                {
                    Debug.Log("Playemovement.MoveCheck has hit an Obstacle");
                    // Use here for any visual effect to indicate that the player can not move there,, red flash.
                    return;
                }
            }

            StartCoroutine(Move(startRay,endRay, keyToGoTo.gameObject));
        }
    }

    private IEnumerator Move(Vector3 startpos, Vector3 endPos,GameObject key)
    {
        _isMoving = true;
        float distance = Vector3.Distance(startpos, endPos);
        float timeToTake = distance / 15; // divide by 25 during fever mode.
        float distanceOffset = distance / 100; 

        for (float i = 0; i < timeToTake; i+=Time.fixedDeltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, i / timeToTake);
            if (Vector3.Distance(transform.position, endPos) < distanceOffset) break;
            yield return null;
        }
        Debug.Log("Coroutine Move has finished.");
        transform.position = endPos;
        _isMoving = false;
        currentKeyOn = key;

    }

    private GameObject CurrentKeyOn()
    {
        Physics.Raycast(transform.position, Vector3.down, out var hit, 1, keyMask);
        if(hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
