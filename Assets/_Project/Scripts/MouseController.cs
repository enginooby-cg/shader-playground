using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] Texture2D arrowCursor;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                UnlockMouse();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleMouseLock();
        }
    }

    private void ToggleMouseLock()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            LockMouse();
        }
        else
        {
            UnlockMouse();
        }
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
