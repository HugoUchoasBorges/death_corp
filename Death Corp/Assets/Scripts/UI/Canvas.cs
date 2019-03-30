using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {

    private void Awake()
    {
        GameManager.canvasInstance = this;
    }
}
