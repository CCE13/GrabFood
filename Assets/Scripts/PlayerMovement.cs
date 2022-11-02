using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool started = false;
    [SerializeField] private GameObject ground;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask keyMask;
<<<<<<< Updated upstream
    public GameObject currentKeyOn;
    private bool _isMoving;
=======
    [SerializeField] private ParticleSystem stunfx, smokefx;
    [SerializeField] private CameraController camCont;
    public GameObject currentKeyOn;
    private bool _isMoving, _isStunned;
    private Animator anim;
>>>>>>> Stashed changes

    private GameObject previousKeyOn;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>(); 
        currentKeyOn = CurrentKeyOn();
        previousKeyOn = currentKeyOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        currentKeyOn = CurrentKeyOn();

        if (!currentKeyOn) return;
        currentKeyOn.transform.GetChild(0).gameObject.SetActive(false);

        if(currentKeyOn != previousKeyOn && previousKeyOn)
        {
            previousKeyOn.transform.GetChild(0).gameObject.SetActive(true);
        }

        previousKeyOn = currentKeyOn;

        if (_isMoving) return;
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) return;

            var keyPressed = Input.inputString;
            Transform keyToGoTo = ground.transform.Find(keyPressed);
            if (!keyToGoTo) return;
            anim.Play("Move");
            MoveCheck(keyToGoTo);

            //KeyChecker key = currentKeyOn.GetComponent<KeyChecker>();
            //if (!key.keysAround.Contains(keyToGoTo.name)) return;
            //var pos = new Vector3(keyToGoTo.position.x, transform.position.y, keyToGoTo.position.z);
            //StartCoroutine(Move(pos,keyToGoTo.gameObject));
            
        }
    }

<<<<<<< Updated upstream
=======
    public void Stun(float duration)
    {
        // Add stun effects and stuff.
        StopAllCoroutines();
        _isMoving = false;
        _isStunned = true;
        MoveCheck(currentKeyOn.transform);
        anim.Play("Stun");
        stunfx.Play();
        camCont.NudgeShake();

        Invoke("UnStun", duration);

    }

    private void UnStun()
    {
        // Remove any animation or effects.
        anim.Play("Idle");
        _isStunned = false;
    }

>>>>>>> Stashed changes
    private void MoveCheck(Transform keyToGoTo)
    {
        Vector3 startRay = new Vector3(currentKeyOn.transform.position.x, transform.position.y, currentKeyOn.transform.position.z);
        Vector3 endRay = new Vector3(keyToGoTo.position.x, transform.position.y, keyToGoTo.position.z);

        Vector3 dir = (endRay - startRay).normalized;
        transform.LookAt(transform.position + dir);

        if (keyToGoTo.position.x == currentKeyOn.transform.position.x ||
           keyToGoTo.position.z == currentKeyOn.transform.position.z)
        {
            StartCoroutine(Move(startRay,endRay, keyToGoTo.gameObject));
            return;
        }

        // Insert a popup stating "I can't go there"
        camCont.NudgeShake();
    }

    private IEnumerator Move(Vector3 startpos, Vector3 endPos,GameObject key)
    {
        _isMoving = true;
        smokefx.Play();
        float distance = Vector3.Distance(startpos, endPos);
<<<<<<< Updated upstream
        float timeToTake = distance / 5; // divide by 15 during fever mode.

        if (FeverMode.inst.isFever)
        {
            timeToTake = distance / 8;
=======
        float timeToTake = distance / 2; // divide by 15 during fever mode.

        if (FeverMode.inst.isFever)
        {
            timeToTake = distance / 4;
>>>>>>> Stashed changes
        }

        float distanceOffset = distance / 100; 

        for (float i = 0; i < timeToTake; i+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, i / timeToTake);
            if (Vector3.Distance(transform.position, endPos) < distanceOffset) break;
            yield return null;
        }
        Debug.Log("Coroutine Move has finished.");
        transform.position = endPos;
        _isMoving = false;
        smokefx.Stop();
        if(!_isStunned) anim.Play("Idle");
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
