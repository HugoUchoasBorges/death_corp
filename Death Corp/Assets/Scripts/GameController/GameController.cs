using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region Variables

    [Header("Points")]
    [SerializeField]
    private int blessingPoints = 0;
    [SerializeField]
    private int cursePoints = 0;

    #endregion

    private void Awake()
    {
        // Tells Singleton GameManager that I'm the main GameController instance  
        GameManager.gameControllerInstance = this;
    }
}
