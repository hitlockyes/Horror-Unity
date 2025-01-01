using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class flashlight : MonoBehaviour
{
    [Header("Flashlight")]
    public GameObject _flashlight;
    public Light flashlightLight;

    [Header("On/Off Bools")]
    public bool lightOn;
    public bool lightOff;

    [Header("Flashlight settings")]
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float intensityChangeRate = 0.5f;

    [Header("Text")]
    public TMP_Text lowText;
    public TMP_Text moderateText;
    public TMP_Text brightText;

    [SerializeField] private Transform _playerCamera
;   private bool isAdjustingFlashlight = false;
    private float adjustCooldown = 0f;

    private void Start()
    {
        lightOff = true;
        _flashlight.SetActive(false);
        flashlightLight.intensity = 1.5f;
        _playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        OnOff();
        FlashlightFollowCamera();

        if (lightOn && !isAdjustingFlashlight)
        {
            Vector2 scrollValue = Mouse.current.scroll.ReadValue();

            if (scrollValue.y != 0)
            {
                StartCoroutine(AdjustFlashlight(scrollValue.y));
            }
        }
    }

    void OnOff()
    {
        if (lightOff && (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.fKey.wasPressedThisFrame))
        {
            _flashlight.SetActive(true);
            lightOff = false;
            lightOn = true;
        }
        else if (lightOn && (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.fKey.wasPressedThisFrame))
        {
            _flashlight.SetActive(false);
            lightOff = true;
            lightOn = false;
        }
    }

    void FlashlightFollowCamera()
    {
        if (_playerCamera != null && flashlightLight != null)
        {
            _flashlight.transform.position = _playerCamera.position;
            _flashlight.transform.rotation = _playerCamera.rotation;
        }

    }
        IEnumerator AdjustFlashlight(float scrollValueY)
        {
            isAdjustingFlashlight = true;

            if (scrollValueY > 0)
            {
                flashlightLight.intensity += intensityChangeRate;
            }
            else if (scrollValueY < 0)
            {
                flashlightLight.intensity -= intensityChangeRate;
            }

            flashlightLight.intensity = Mathf.Clamp(flashlightLight.intensity, 1, 2);

            yield return new WaitForSeconds(adjustCooldown);
            isAdjustingFlashlight = false;
        }
    
}
