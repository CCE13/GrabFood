using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterHolder : MonoBehaviour
{
    public int posInWord;
    public string letter;

    private float y;
    [SerializeField] private LayerMask keyMask;
    [SerializeField] private ObjectPool fxPool;

    private void Start()
    {
        y = transform.position.y;
    }

    private void OnEnable()
    {
        Physics.Raycast(transform.position, Vector3.down, out var hit, 1, keyMask);
        if (hit.collider != null)
        {
            hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
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
            WordUpdater.inst.UpdateTime(posInWord,letter);

            // Visual Feedback, but for now just turn off.
            if (FeverMode.inst.isFever)
            {
                PointsManager.inst.AddPoints(200);

                // Play animation.

                //var fx = fxPool.GetObj();
                //fx.transform.position = transform.position;
                //fx.SetActive(true);
            }
            else
            {
                PointsManager.inst.AddPoints(100);

                // Play animation.

                //var fx = fxPool.GetObj();
                //fx.transform.position = transform.position;
                //fx.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
