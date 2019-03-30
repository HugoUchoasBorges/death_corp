using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPopupController : MonoBehaviour
{

    #region Variables

    private static FloatingPopup floatingPopup;

    #endregion

    public static void Initialize()
    {
        floatingPopup = Resources.Load<FloatingPopup>("Prefabs/Fantasminha");
    }

    public static void CreateFloatingPopup()
    {
        if (floatingPopup)
        {
            FloatingPopup instance = Instantiate(floatingPopup);
            instance.transform.SetParent(GameManager.earthInstance.transform, false);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            instance.transform.position = mousePosition;
        }
    }
}
