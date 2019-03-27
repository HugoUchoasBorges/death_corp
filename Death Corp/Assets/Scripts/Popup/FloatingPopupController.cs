using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPopupController : MonoBehaviour
{

    #region Variables

    private static FloatingPopup floatingPopup;
    private static GameObject canvas;

    #endregion

    public static void Initialize()
    {
        floatingPopup = Resources.Load<FloatingPopup>("Prefabs/PopupParent");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public static void CreateFloatingPopup()
    {
        if (floatingPopup)
        {
            FloatingPopup instance = Instantiate(floatingPopup);
            if (canvas)
            {
                instance.transform.SetParent(canvas.transform, false);
                instance.transform.position = Input.mousePosition;
            }   
        }
    }
}
