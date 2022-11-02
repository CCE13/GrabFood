using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void NudgeShake()
    {
        StopAllCoroutines();
        int randomizerBool = Random.Range(0, 2);
        float toRotate = 0;

        switch (randomizerBool)
        {
            case 0:
                toRotate = Random.Range(0.5f, 4);
                break;
            case 1:
                toRotate = Random.Range(-0.5f, -3);
                break;
        }

        StartCoroutine(RotateFX(toRotate));
    }

    private IEnumerator RotateFX(float angle)
    {
        float timePassed = 0;
        float timeToTake = 0.2f;
        float curAngle = 0;

        while (timePassed < timeToTake)
        {
            if (timePassed / timeToTake < 0.5f)
            {
                curAngle = Mathf.Lerp(0, angle, timePassed / (timeToTake / 2));
            }
            else
            {
                curAngle = Mathf.Lerp(angle, 0, timePassed / (timeToTake / 2));
            }
            transform.rotation = Quaternion.Euler(60, 0, curAngle);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}
