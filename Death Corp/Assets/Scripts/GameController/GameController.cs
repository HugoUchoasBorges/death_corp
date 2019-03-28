using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [System.Serializable]
    public class State
    {
        [Header("Points")]
        public int clickAmount = 0;
        public int soulsCollected = 0;
        public int blessingPoints = 0;
        public int cursePoints = 0;

        [Space(5)]
        [Header("Game Info")]
        public int soulsCRI = 0;
        public int soulsCRC = 0;
        public int deathRate = 0;
        public int birthRate = 0;
        public int faithLevel = 0;

        public bool checkAchievementState(State state)
        {
            if (state.clickAmount > clickAmount)
            {
                return false;
            }
            if (state.soulsCollected > soulsCollected)
            {
                return false;
            }
            if (state.blessingPoints > blessingPoints)
            {
                return false;
            }
            if (state.cursePoints > cursePoints)
            {
                return false;
            }
            if (state.soulsCRI > soulsCRI)
            {
                return false;
            }
            if (state.soulsCRC > soulsCRC)
            {
                return false;
            }
            if (state.deathRate > deathRate)
            {
                return false;
            }
            if (state.birthRate > birthRate)
            {
                return false;
            }
            if (state.faithLevel > faithLevel)
            {
                return false;
            }

            return true;
        }
    }

    #region Variables

    public State gameState;

    [Space(5)]
    [Header("PowerUps")]
    [SerializeField]
    private int multiplier = 1;

    [Space(5)]
    public Achievement[] achievements;

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
        gameState.soulsCollected += amount * multiplier;
        gameState.clickAmount++;

        foreach (Achievement achievement in achievements)
        {
            if (gameState.checkAchievementState(achievement.requiredState))
            {
                Debug.Log(achievement.message);
            }
        }
    }
}
