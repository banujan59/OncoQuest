using System.Collections;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2.0f;
    public Color fadeColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    private Renderer _rend = null;

    void Start()
    {
        _rend = GetComponent<Renderer>();
        if(fadeOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        Fade(1.0f, 0.0f);
    }

    public void FadeOut()
    {
        Fade(0.0f, 1.0f);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0.0f;
        while(timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            _rend.material.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }

        Color finalColor = fadeColor;
        finalColor.a = alphaOut;
        _rend.material.color = finalColor;
    }

}
