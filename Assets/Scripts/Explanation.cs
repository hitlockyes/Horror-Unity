using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Explanation : MonoBehaviour
{
    public TMP_Text messageText;
    public Button continueButton;
    public TMP_Text continueText;
    public Image continueImage;
    private bool hasShownSequence = false;

    private void Start()
    {
        messageText.enabled = false;
        continueButton.gameObject.SetActive(false);
        StartCoroutine(DelayAndShowMessage());
    }

    private IEnumerator DelayAndShowMessage()
    {
        yield return new WaitForSeconds(3); 
        StartCoroutine(ShowMessageText());
    }

    private IEnumerator ShowMessageText()
    {
        if (!hasShownSequence)
        {
            yield return StartCoroutine(FadeInText(messageText));
            yield return new WaitForSeconds(3);
            yield return StartCoroutine(FadeInButton(continueButton));
            hasShownSequence = true;
        }
    }

    private IEnumerator FadeInText(TMP_Text textElement)
    {
        textElement.enabled = true;
        Color originalColor = textElement.color;
        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); 
        for (float t = 0.01f; t < 1; t += Time.deltaTime)
        {
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
    }

    private IEnumerator FadeInButton(Button button)
    {
        button.gameObject.SetActive(true);
        continueButton.enabled = true; 
        continueText.enabled = true;
        continueImage.enabled = true;
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0; 
        for (float t = 0.01f; t < 1; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }
        button.onClick.AddListener(OnContinueClicked);
    }

    private void OnContinueClicked()
    {
        StartCoroutine(FadeOutText(messageText));
        StartCoroutine(FadeOutButton(continueButton));
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
    }

    private IEnumerator FadeOutButton(Button button)
    {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        for (float t = 0.01f; t < 1; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        button.gameObject.SetActive(false);
    }
}
