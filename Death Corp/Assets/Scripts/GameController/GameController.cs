using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [System.Serializable]
    public class State
    {
        public string name;

        [Header("Points")]
        public int clickAmount = 0;
        public int soulsCollected = 0;
        public int blessingPoints = 0;
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
    }

    #region Variables

    public State gameState;

    [Space(5)]
    [Header("PowerUps")]
    [SerializeField]
    private int multiplier = 1;

    #endregion

    private void Awake()
    {
        // Tells Singleton GameManager that I'm the main GameController instance  
        GameManager.gameControllerInstance = this;
        gameState.name = "GameState";
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
        gameState.soulsCollected += amount * multiplier;
        gameState.clickAmount++;
    }
}
