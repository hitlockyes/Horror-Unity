using UnityEngine;
using TMPro;
using System.Collections;

public class Welcome : MonoBehaviour
{
    public TMP_Text welcomeText;
    private bool hasShownTutorial = false;

    private void Start()
    {
        welcomeText = GetComponent<TMP_Text>();
        welcomeText.enabled = false;
        StartCoroutine(ShowTutorial());
    }

    private IEnumerator ShowTutorial()
    {
        if (!hasShownTutorial)
        {
            welcomeText.enabled = true;
            StartCoroutine(FadeInText(welcomeText));
            yield return new WaitForSeconds(2); 
            StartCoroutine(FadeOutText(welcomeText));
            hasShownTutorial = true;
        }
    }

    private IEnumerator FadeInText(TMP_Text textElement)
    {
        Color originalColor = textElement.color;
        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); 
        for (float t = 0.01f; t < 1; t += Time.deltaTime)
        {
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
    }

    private IEnumerator FadeOutText(TMP_Text textElement)
    {
        Color originalColor = textElement.color;
        for (float t = 0.01f; t < 1; t += Time.deltaTime)
        {
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }
        textElement.enabled = false;
        textElement.color = originalColor;
    }
}
