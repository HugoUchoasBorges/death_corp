using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{

    #region Variables

    private static FloatingText popupText;

    #endregion

    public static void Initialize()
    {
        popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        CreateFloatingText(text, location, false);
    }

    public static void CreateFloatingText(string text, Transform location, bool negative)
    {
        //if (!popupText)
        //    Initialize();

        Canvas canvas = GameManager.canvasInstance;

        FloatingText instance = Instantiate(popupText);

        Color color = instance.positiveColor;
        if (negative)
        {
            color = instance.negativeColor;
        }

        //Debug.Log("Value: " + text + " - Color: " + color);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = location.position;
        instance.SetText(text);
        instance.SetColor(color);
    }
}
