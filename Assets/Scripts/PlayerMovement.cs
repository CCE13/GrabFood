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
            KeyChecker key = currentKeyOn.GetComponent<KeyChecker>();
            if (!key.keysAround.Contains(keyToGoTo.name)) return;
            var pos = new Vector3(keyToGoTo.position.x, transform.position.y, keyToGoTo.position.z);
            StartCoroutine(Move(pos,keyToGoTo.gameObject));
            
        }
    }

    private IEnumerator Move(Vector3 pos,GameObject key)
    {
        _isMoving = true;
        for (float i = 0; i < 2; i+=Time.deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed*Time.deltaTime);
            if(transform.position == pos)
            {
                break;
            }
            yield return null;
        }
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
