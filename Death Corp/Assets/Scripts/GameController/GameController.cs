using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Inner Classes

    [System.Serializable]
    public class SoulsCollector
    {
        #region SoulsCollector Variables

        public string name;
        public int level = 0;
        public float profit = 0;
        public float cost = 0;
        public float soulsCRI = 0;
        public float soulsCRC = 0;
        public float goodFaith = 0;
        public float badFaith = 0;

        public float x10 = 0;
        public float x25 = 0;
        public float x50 = 0;
        public float x100 = 0;
        public float x500 = 0;
        public float actualProfit = 0;

        #endregion

        public void UpgradeLevel()
        {
            GameManager.gameControllerInstance.gameState.level++;
            level++;
            CalculateNextLevel();
        }

        public void CalculateNextLevel()
        {
            switch (name)
            {
                case "Gen1":
                    CalculateGen1();
                    break;
                case "Gen2":
                    CalculateGen2();
                    break;
                case "Gen3":
                    CalculateGen3();
                    break;
                case "Gen4":
                    CalculateGen4();
                    break;
                case "Gen5":
                    CalculateGen5();
                    break;

                default:
                    CalculateGen1();
                    break;
            }

            if (level == 0)
                actualProfit = 0;
            else if (level < 10)
                actualProfit = profit;
            else if (level < 25)
                actualProfit = x10;
            else if (level < 50)
                actualProfit = x25;
            else if (level < 100)
                actualProfit = x50;
            else if (level < 500)
                actualProfit = x100;
            else
                actualProfit = x500;

            if (GameManager.gameControllerInstance != null)
            {
                GameManager.gameControllerInstance.gameState.soulsCRC = GameManager.gameControllerInstance.earthSoulsCollectorClick.actualProfit;
            }
        }

        #region CalculateGens
        private void CalculateGen1()
        {
            profit = 2 * level;
            cost = 10 + Mathf.Pow((float)level, 2f);
            x10 = 5 * profit;
            x25 = 10 * profit;
            x50 = 25 * profit;
            x100 = 50 * profit;
            x500 = 100 * profit;
        }
        private void CalculateGen2() { }
        private void CalculateGen3() { }
        private void CalculateGen4() { }
        private void CalculateGen5() { }

        #endregion 
    }

    [System.Serializable]
    public class State
    {
        #region State Variables

        [Header("Points")]
        public float clickAmount = 0;
        public float level = 0;
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

        #endregion

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

    #endregion

    #region Variables

    public State gameState;

    // Souls Collectors
    public SoulsCollector earthSoulsCollectorClick;
    public SoulsCollector[] earthSoulsCollector;
    public SoulsCollector[] heavenSoulsCollector;
    public SoulsCollector[] hellSoulsCollector;

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

        // Initiates all SoulsCollectorClicks
        CalculateSoulsCollectorLevel(earthSoulsCollectorClick);

        foreach (SoulsCollector soulsCollector in earthSoulsCollector)
        {
            CalculateSoulsCollectorLevel(soulsCollector);
        }
        foreach (SoulsCollector soulsCollector in hellSoulsCollector)
        {
            CalculateSoulsCollectorLevel(soulsCollector);
        }
        foreach (SoulsCollector soulsCollector in heavenSoulsCollector)
        {
            CalculateSoulsCollectorLevel(soulsCollector);
        }

    }

    public void UpgradeSoulsCollectorLevel(SoulsCollector soulsCollector)
    {
        soulsCollector.UpgradeLevel();
    }
    public void CalculateSoulsCollectorLevel(SoulsCollector soulsCollector)
    {
        soulsCollector.CalculateNextLevel();
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
        gameState.clickAmount++;
        float soulsCollectedAmount = Mathf.Min(gameState.SoulsCollected, amount * gameState.soulsCRC);
        if (soulsCollectedAmount > 0)
        {
            // Collect the Souls
            gameState.SoulsCollected -= soulsCollectedAmount;

            int blessingPoints = Mathf.RoundToInt(soulsCollectedAmount * gameState.faithLevel);
            int cursingPoints = Mathf.FloorToInt(soulsCollectedAmount) - blessingPoints;
            gameState.blessingPoints += blessingPoints;
            gameState.cursePoints += cursingPoints;

            FloatingPopupController.CreateFloatingPopup();
            UpdateGUI();
            //CheckAchievements();
        }
    }

    public void CheckAchievements()
    {
        foreach (Achievement achievement in achievements)
        {
            if (gameState.checkAchievementState(achievement.requiredState))
            {
                Debug.Log(achievement.message);
            }
        }
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

        string populationDisplay = "Population: " + Mathf.FloorToInt(GameManager.earthInstance.Population).ToString();
        string soulsDisplay = "Souls: " + Mathf.FloorToInt(gameState.SoulsCollected).ToString();
        string bpDisplay = "BP: " + Mathf.FloorToInt(gameState.blessingPoints).ToString();
        string cpDisplay = "CP: " + Mathf.FloorToInt(gameState.cursePoints).ToString();

        // Temp
        infoDisplays[0].text = populationDisplay;
        infoDisplays[1].text = soulsDisplay;
        infoDisplays[2].text = bpDisplay;
        infoDisplays[3].text = cpDisplay;
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
