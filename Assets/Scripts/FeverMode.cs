using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FeverMode : MonoBehaviour
{
    // Default 4, lower means slower depletion of fever.
    public int depletionSpeed = 4;

    public Gradient rgb;
    public Image fill;
    public Slider feverSlider;

    public static FeverMode inst;

    public bool paused;
    public bool isFever;
    private Color ogColor;

    public static event Action<Color> RGBFlash;

    private void Start()
    {
        inst = this;
        ogColor = fill.color;
    }

    private void Update()
    {
        if (paused) return;
        if (Input.GetKeyDown(KeyCode.Space)) StartFever();
        if (!isFever) return;
        float rgbtq = Mathf.PingPong(Time.time / 3, 1);
        fill.color = rgb.Evaluate(rgbtq);
        feverSlider.value -= Time.deltaTime * depletionSpeed;
        RGBFlash?.Invoke(rgb.Evaluate(rgbtq));
        if (feverSlider.value <= 0)
        {
            isFever = false;
            fill.color = ogColor;
            feverSlider.value = 0;
            RGBFlash?.Invoke(Color.white);
        }
    }

    [ContextMenu("StartFever")]
    public void StartFever()
    {
        if(feverSlider.value > 10)
        {
            isFever = true;
            return;
        }

        Debug.Log("Not enough fever juice to start fever stupid.");
    }

    public void AddFever(float amt)
    {
        StartCoroutine(FeverLerp(amt));
    }

    private IEnumerator FeverLerp(float amt)
    {
        float timepassed = 0;
        float timeToTake = 0.4f;
        float endAmt = feverSlider.value + amt;

        while(timepassed < timeToTake)
        {
            feverSlider.value = Mathf.Lerp(feverSlider.value, endAmt, timepassed / timeToTake);
            timepassed += Time.deltaTime;
            yield return null;
        }

        feverSlider.value = endAmt;
    }
}
