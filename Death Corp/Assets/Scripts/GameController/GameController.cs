using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region Variables

    [Header("Points")]
    [SerializeField]
    private int clickAmount = 0;
    [SerializeField]
    private int soulsCollected = 0;
    [SerializeField]
    private int blessingPoints = 0;
    [SerializeField]
    private int cursePoints = 0;

    [Space(5)]
    [Header("Game Info")]
    [SerializeField]
    private int soulsCRI = 0;
    [SerializeField]
    private int soulsCRC = 0;
    [SerializeField]
    private int deathRate = 0;
    [SerializeField]
    private int birthRate = 0;
    [SerializeField]
    private int faithLevel = 0;

    [Space(5)]
    [Header("PowerUps")]
    [SerializeField]
    private int multiplier = 1;

    #endregion

    private void Awake()
    {
        // Tells Singleton GameManager that I'm the main GameController instance  
        GameManager.gameControllerInstance = this;
    }

    /// <summary>
    /// Collect 1 soul
    /// </summary>
    public void CollectSouls()
    {
        CollectSouls(1);
    }

    /// <summary>
    /// Collect Souls
    /// </summary>
    /// <param name="amount">Amount of Souls to Collect</param>
    public void CollectSouls(int amount)
    {
        FloatingPopupController.CreateFloatingPopup();
        soulsCollected += amount * multiplier;
        clickAmount++;
    }
}
