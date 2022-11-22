using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorHandler
{
    public static void ActiveCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void DeactiveCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
