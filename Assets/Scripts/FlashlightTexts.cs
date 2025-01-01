using System.Collections;
using TMPro;
using UnityEngine;

public class FlashlightTexts : MonoBehaviour
{
    public flashlight flashlightScript;

    public TMP_Text lowText;
    public TMP_Text moderateText;
    public TMP_Text brightText;

    private Coroutine currentCoroutine;

    private void Start()
    {
        lowText.enabled = false;
        moderateText.enabled = false;
        brightText.enabled = false;

        if (flashlightScript == null)
        {
            flashlightScript = Object.FindFirstObjectByType<flashlight>();
        }
    }

    private void Update()
    {
        if (flashlightScript != null && flashlightScript.flashlightLight != null)
        {
            TextPopUp();
        }
    }

    private void TextPopUp()
    {
        if (currentCoroutine != null) return;

        if (flashlightScript.flashlightLight.intensity == 1)
        {
            DisableAllTexts();
            currentCoroutine = StartCoroutine(ShowText(lowText));
        }
        else if (flashlightScript.flashlightLight.intensity == 1.5)
        {
            DisableAllTexts();
            currentCoroutine = StartCoroutine(ShowText(moderateText));
        }
        else if (flashlightScript.flashlightLight.intensity == 2)
        {
            DisableAllTexts();
            currentCoroutine = StartCoroutine(ShowText(brightText));
        }
    }

    private void DisableAllTexts()
    {
        lowText.enabled = false;
        moderateText.enabled = false;
        brightText.enabled = false;
    }

    IEnumerator ShowText(TMP_Text textElement)
    {
        if (textElement != null)
        {
            textElement.enabled = true;
            yield return new WaitForSeconds(1);
            textElement.text = " ";
            currentCoroutine = null;
        }
    }
}
