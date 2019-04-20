using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{

    #region Variables

    private static GameObject popupTextObject;

    #endregion

    public static void Initialize()
    {
        popupTextObject = Resources.Load<GameObject>("Prefabs/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        CreateFloatingText(text, location, false);
    }

    public static void CreateFloatingText(string text, Transform location, bool negative)
    {

        Canvas canvas = GameManager.canvasInstance;

        GameObject instance = Instantiate(popupTextObject);
        FloatingText floatingText = instance.GetComponent<FloatingText>();

        Color c = floatingText.positiveColor;
        if (negative)
        {
            c = floatingText.negativeColor;
        }

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = location.position;
        floatingText.SetText(text);
        floatingText.SetColor(c);
    }
}
