using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFadeEffect : MonoBehaviour
{
    public static TextFadeEffect textFadeEffectInstance;
    
    [SerializeField] private float fadeTime = 1;
    
    public void FadeTextIn(TextMeshProUGUI text)
    {
        StartCoroutine(FadeCoroutineIn(text));
    }

    public void FadeTextOut(TextMeshProUGUI text)
    {
        StartCoroutine(FadeCoroutineOut(text));
    }

    IEnumerator FadeCoroutineIn(TextMeshProUGUI text)
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
            text.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.clear, Color.white, waitTime));
            yield return null;
            waitTime += Time.deltaTime / fadeTime;
        }
    }
    
    
    IEnumerator FadeCoroutineOut(TextMeshProUGUI text)
    {
        float waitTime = 0;
        while (waitTime < 1)
        {
            text.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.white, Color.clear, waitTime));
            yield return null;
            waitTime += Time.deltaTime / fadeTime;
        }
    }

}
