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
        public float soulsCollected = 0;
        public int blessingPoints = 0;
        public int cursePoints = 0;

        [Space(5)]
        [Header("Game Info")]
        public int soulsCRI = 0;
        public int soulsCRC = 0;
        public float deathRate = 0;
        public float birthRate = 0;
        public int faithLevel = 0;

        public bool checkAchievementState(State state)
        {
            System.Reflection.FieldInfo[] fields = this.GetType().GetFields();

            foreach (System.Reflection.FieldInfo field in fields)
            {
                int stateFieldValue = (int)this.GetType().GetField(field.Name).GetValue(state);
                int thisFieldValue = (int)this.GetType().GetField(field.Name).GetValue(this);

                if (stateFieldValue > thisFieldValue)
                {
                    return false;
                }
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

    #region Population generator variables
    
    private float currentTime = 0;
    private int currentTimeInSeconds = 0;
    [Header("Generator settings")]
    [SerializeField]
    private float tickTime = 1.0f;

    #endregion

    /// <summary>
    /// This is a population generator, handles all the birth and death tasks
    /// and also collects the soul of the dead.
    /// </summary>
    public void HandlePopulation()
    {
        currentTime += Time.deltaTime;
        currentTimeInSeconds = (int) currentTime % 60;
        if (currentTimeInSeconds >= tickTime)
        {
            currentTime = 0;
            GameManager.earthInstance.Population += (gameState.birthRate - gameState.deathRate);
        }
    }

    private void Update()
    {
        HandlePopulation();
    }
}
