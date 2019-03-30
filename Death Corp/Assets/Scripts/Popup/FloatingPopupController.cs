using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPopupController : MonoBehaviour
{

    #region Variables

    private static FloatingPopup floatingPopup;
    private static GameObject canvas;

    // Pooling
    public static Queue<FloatingPopup> poolQueue = new Queue<FloatingPopup>();
    private static int initialPoolingSize = 6;

    #endregion

    public static void Initialize()
    {
        floatingPopup = Resources.Load<FloatingPopup>("Prefabs/PopupParent");
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        for(int i= 0; i < initialPoolingSize; i++)
        {
            poolQueue.Enqueue(Instantiate(floatingPopup));
        }
    }

    public static void CreateFloatingPopup()
    {
        if (floatingPopup)
        {
            if(poolQueue.Count == 0)
            {
                poolQueue.Enqueue(Instantiate(floatingPopup));
            }
            FloatingPopup instance = poolQueue.Dequeue();

            if (canvas)
            {
                instance.transform.SetParent(canvas.transform, false);
                instance.transform.position = Input.mousePosition;
            }   
        }
    }
}
