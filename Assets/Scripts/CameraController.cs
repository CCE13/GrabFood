using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void StunEffect()
    {
        StopAllCoroutines();
        float toRotate = Random.Range(-3, 4);
        StartCoroutine(RotateFX(toRotate));
    }

    private IEnumerator RotateFX(float angle)
    {
        float timePassed = 0;
        float timeToTake = 0.2f;
        float curAngle = 0;

        while(timePassed < timeToTake)
        {
            if(timePassed/timeToTake < 0.5f)
            {
                curAngle = Mathf.Lerp(0, angle, timePassed / (timeToTake/2));
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
