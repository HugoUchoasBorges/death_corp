using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class State
    {
        [Header("Points")]
        public float clickAmount = 0;
        [SerializeField]
        private float soulsCollected = 100;
        public float SoulsCollected
        {
            get
            {
                return soulsCollected;
            }
            set
            {
                soulsCollected = Mathf.Max(value, 0);
            }
        }
        public float blessingPoints = 0;
        public float cursePoints = 0;

        [Space(5)]
        [Header("Game Info")]
        public float soulsCRI = 0;
        public float soulsCRC = 0;
        public float deathRate = 0;
        public float birthRate = 0;
        [Range(0f, 1f)]
        public float faithLevel = 0.5f;

        public bool checkAchievementState(State state)
        {
            return false;
            //System.Reflection.FieldInfo[] fields = this.GetType().GetFields();

            //foreach (System.Reflection.FieldInfo field in fields)
            //{
            //    int stateFieldValue = (int)this.GetType().GetField(field.Name).GetValue(state);
            //    int thisFieldValue = (int)this.GetType().GetField(field.Name).GetValue(this);

            //    if (stateFieldValue > thisFieldValue)
            //    {
            //        return false;
            //    }
            //}

            //return true;
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

    #region Population generator variables

    private float currentTime = 0;
    private int currentTimeInSeconds = 0;
    [Header("Generator settings")]
    [SerializeField]
    private float tickTime = 1.0f;

    private Text[] infoDisplays;

    #endregion

    private void Awake()
    {
        // Tells Singleton GameManager that I'm the main GameController instance  
        GameManager.gameControllerInstance = this;
        UpdateGUI();
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

        float soulsCollectedAmount = Mathf.Min(GameManager.earthInstance.Population, amount * multiplier);
        if (soulsCollectedAmount > 0)
        {
            // Collect the Souls
            gameState.SoulsCollected -= soulsCollectedAmount;
            gameState.clickAmount++;

            int blessingPoints = Mathf.RoundToInt(soulsCollectedAmount * gameState.faithLevel);
            int cursingPoints = Mathf.FloorToInt(soulsCollectedAmount) - blessingPoints;
            gameState.blessingPoints += blessingPoints;
            gameState.cursePoints += cursingPoints;

            FloatingPopupController.CreateFloatingPopup();
            UpdateGUI();
        }

        //foreach (Achievement achievement in achievements)
        //{
        //    if (gameState.checkAchievementState(achievement.requiredState))
        //    {
        //        Debug.Log(achievement.message);
        //    }
        //}
    }

    /// <summary>
    /// Updates all information on Screen
    /// </summary>
    public void UpdateGUI()
    {
        if (infoDisplays == null)
        {
            infoDisplays = GameManager.canvasInstance.GetComponentsInChildren<Text>();
        }

        // Temp
        infoDisplays[0].text = "Population: " + Mathf.FloorToInt(GameManager.earthInstance.Population).ToString();
        infoDisplays[1].text = "Souls: " + Mathf.FloorToInt(gameState.SoulsCollected).ToString();
    }

    /// <summary>
    /// This is a population generator, handles all the birth and death tasks
    /// and also collects the soul of the dead.
    /// </summary>
    public void HandlePopulation()
    {
        GameManager.earthInstance.Population += gameState.birthRate;

        float deathAmount = Mathf.Min(GameManager.earthInstance.Population, gameState.deathRate);
        GameManager.earthInstance.Population -= deathAmount;
        gameState.SoulsCollected += deathAmount;

        UpdateGUI();
    }


    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        currentTimeInSeconds = (int)currentTime % 60;

        if (currentTimeInSeconds >= tickTime)
        {
            // Every tickTime Seconds
            currentTime = 0;
            HandlePopulation();
        }
    }
}
