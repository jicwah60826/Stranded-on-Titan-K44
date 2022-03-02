using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroFade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToUse;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOnStart = false;
    [SerializeField] private float timeMultiplier;
    private bool FadeIncomplete = false;

    private void Start()
    {

        if (fadeOnStart)
        {
            if (fadeIn)
            {
                StartCoroutine(FadeInText(timeMultiplier, textToUse));
                FadeIncomplete = true;
            }
            else
            {

                StartCoroutine(FadeOutText(timeMultiplier, textToUse));
            }
        }
    }

    private void Update()
    {
        if (FadeIncomplete)
        {
            StartCoroutine(FadeOutText(timeMultiplier, textToUse));
        }
    }

    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
{
    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    while (text.color.a < 1.0f)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
        yield return null;
    }
}


private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
{
    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    while (text.color.a > 0.0f)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
        yield return null;
    }
}
public void FadeInText(float timeSpeed = -1.0f)
{
    if (timeSpeed <= 0.0f)
    {
        timeSpeed = timeMultiplier;
    }
    StartCoroutine(FadeInText(timeSpeed, textToUse));
}
public void FadeOutText(float timeSpeed = -1.0f)
{
    if (timeSpeed <= 0.0f)
    {
        timeSpeed = timeMultiplier;
    }
    StartCoroutine(FadeOutText(timeSpeed, textToUse));
}

private IEnumerator TextFadeController(TextMeshProUGUI textToUse)
{
    yield return StartCoroutine(FadeInText(1f, textToUse));
    yield return new WaitForSeconds(2f);
    yield return StartCoroutine(FadeOutText(1f, textToUse));
    //End of transition, do some extra stuff!!
}
}


