using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    private bool isCursorMode = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorMode();
        }
    }

    private void ToggleCursorMode()
    {
        isCursorMode = !isCursorMode;

        if (isCursorMode)
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;
        }
    }
}
