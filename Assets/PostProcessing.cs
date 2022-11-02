using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessing : MonoBehaviour
{
    private bool flashing;
    private void Start()
    {
        FeverMode.RGBFlash += RGBFlash;
    }
    private void OnDestroy()
    {
        FeverMode.RGBFlash -= RGBFlash;
    }
    private void RGBFlash(Color color)
    {

        VolumeProfile volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));

        // You can leave this variable out of your function, so you can reuse it throughout your class.
        UnityEngine.Rendering.Universal.Vignette vignette;

        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

        vignette.color.Override(color);
    }
}
