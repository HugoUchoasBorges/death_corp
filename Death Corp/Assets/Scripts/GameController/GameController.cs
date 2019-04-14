using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
        public float birthRate = 0;
        public float deathRate = 0;
        public float goodFaith = 0;
        public float badFaith = 0;

        public float x10 = 0;
        public float x25 = 0;
        public float x50 = 0;
        public float x100 = 0;
        public float x500 = 0;
        public float actualProfit = 0;

        #endregion

        public bool CheckUpgradeAvailability()
        {
            return CheckUpgradeAvailability(false);
        }

        public bool CheckUpgradeAvailability(bool cp)
        {
            GameController.State gameState = GameManager.gameControllerInstance.gameState;

            // It means it's an Earth upgrade
            if (name.Contains("Gen"))
            {
                if (!cp)
                {
                    if (gameState.blessingPoints >= cost)
                    {
                        gameState.blessingPoints -= cost;
                        return true;
                    }
                }
                else
                {
                    if (gameState.cursePoints >= cost)
                    {
                        gameState.cursePoints -= cost;
                        return true;
                    }
                }

                return false;
            }
            if (name.Contains("Bless"))
            {
                if (gameState.blessingPoints >= cost)
                {
                    gameState.blessingPoints -= cost;
                    return true;
                }
                return false;
            }
            if (name.Contains("Curse"))
            {
                if (gameState.cursePoints >= cost)
                {
                    gameState.cursePoints -= cost;
                    return true;
                }
                return false;
            }
            return false;
        }

        public void UpgradeLevel(bool cp)
        {
            if (CheckUpgradeAvailability(cp))
            {
                GameManager.gameControllerInstance.gameState.level++;
                level++;
                CalculateNextLevel();
            }
        }

        public void CalculateNextLevel()
        {
            GameController.State gameState = GameManager.gameControllerInstance.gameState;
            switch (name)
            {
                default:
                    CalculateGen1();
                    break;

                // Gen Cases
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

                // Bless Cases
                case "Bless1":
                    CalculateBless1();
                    break;
                case "Bless2":
                    CalculateBless2();
                    break;
                case "Bless3":
                    CalculateBless3();
                    break;
                case "Bless":
                    CalculateBless4();
                    break;

                // Curse Cases
                case "Curse1":
                    CalculateCurse1();
                    break;
                case "Curse2":
                    CalculateCurse2();
                    break;
                case "Curse3":
                    CalculateCurse3();
                    break;
                case "Curse4":
                    CalculateCurse4();
                    break;
            }

            if (name.Contains("Gen"))
            {
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
                    gameState.soulsCRC = GameManager.gameControllerInstance.earthSoulsCollectorClick.actualProfit;

                    gameState.soulsCRI = 0;
                    foreach (SoulsCollector soulsCollector in GameManager.gameControllerInstance.earthSoulsCollector)
                    {
                        gameState.soulsCRI += soulsCollector.actualProfit;
                    }
                }
            }
            else
            {
                if (name.Contains("Bless"))
                {
                    gameState.birthRate = gameState.initialBirthRate;
                    gameState.goodFaith = gameState.startGoodFaith;
                    foreach (SoulsCollector soulsCollector in GameManager.gameControllerInstance.heavenSoulsCollector)
                    {
                        if (soulsCollector.level > 0)
                        {
                            gameState.birthRate += soulsCollector.birthRate;
                            gameState.goodFaith += soulsCollector.goodFaith;
                        }
                    }
                }
                else if (name.Contains("Curse"))
                {
                    gameState.deathRate = gameState.initialDeathRate;
                    gameState.badFaith = gameState.startBadFaith;
                    foreach (SoulsCollector soulsCollector in GameManager.gameControllerInstance.hellSoulsCollector)
                    {
                        if (soulsCollector.level > 0)
                        {
                            gameState.deathRate += soulsCollector.deathRate;
                            gameState.badFaith += soulsCollector.badFaith;
                        }
                    }
                }


                gameState.faithLevel = 0.5f;
                if ((gameState.goodFaith + gameState.badFaith) > 0)
                    gameState.faithLevel = gameState.goodFaith / (gameState.goodFaith + gameState.badFaith);

                gameState.blessingPointsPerSecond = gameState.faithLevel * gameState.soulsCRI;
                gameState.cursePointsPerSecond = (1f - gameState.faithLevel) * gameState.soulsCRI;
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
        private void CalculateGen2()
        {
            profit = 0.1f * level;
            cost = 10 + Mathf.Pow((float)level, 1.5f);
            x10 = 2 * profit;
            x25 = 4 * profit;
            x50 = 8 * profit;
            x100 = 16 * profit;
            x500 = 32 * profit;
        }
        private void CalculateGen3()
        {
            profit = 4 * (12f + level);
            cost = 100 + Mathf.Pow((float)level, 1.5f);
            x10 = 2 * profit;
            x25 = 4 * profit;
            x50 = 8 * profit;
            x100 = 16 * profit;
            x500 = 32 * profit;
        }
        private void CalculateGen4()
        {
            profit = 2f * (200 + 2 * level);
            cost = 1000 + Mathf.Pow((float)level, 2f);
            x10 = 2 * profit;
            x25 = 5 * profit;
            x50 = 10 * profit;
            x100 = 20 * profit;
            x500 = 50 * profit;
        }
        private void CalculateGen5()
        {
            profit = 5000f + 500 * level;
            cost = 5000f + 2f * Mathf.Pow((float)level, 1.5f);
            x10 = 1.5f * profit;
            x25 = 3 * profit;
            x50 = 6 * profit;
            x100 = 10 * profit;
            x500 = 20 * profit;
        }

        #endregion 

        #region CalculateBlesses
        private void CalculateBless1()
        {
            birthRate = 0.5f * level;
            cost = 10 + Mathf.Pow((float)level, 2f);
            goodFaith = 0.5f * level;
        }
        private void CalculateBless2()
        {
            birthRate = 0.1f * level;
            cost = 10 + Mathf.Pow((float)level, 1.5f);
            goodFaith = level;
        }
        private void CalculateBless3()
        {
            birthRate = 4f * (12f + level);
            cost = 100 + Mathf.Pow((float)level, 1.5f);
            goodFaith = 5f + 1.5f * level;
        }
        private void CalculateBless4()
        {
            birthRate = 2f * (200f + 2f * level);
            cost = 100 + Mathf.Pow((float)level, 2f);
            goodFaith = 3f * level + 10f;
        }

        #endregion

        #region CalculateCurses

        private void CalculateCurse1()
        {
            deathRate = 0.5f * level;
            cost = 10 + Mathf.Pow((float)level, 2f);
            badFaith = 0.5f * level;
        }

        private void CalculateCurse2()
        {
            deathRate = 0.1f * level;
            cost = 10 + Mathf.Pow((float)level, 1.5f);
            badFaith = level;
        }

        private void CalculateCurse3()
        {
            deathRate = 4f * (12f + level);
            cost = 100 + Mathf.Pow((float)level, 1.5f);
            badFaith = 5f + 0.5f * level;
        }

        private void CalculateCurse4()
        {
            deathRate = 2f * (200f + 2f * level);
            cost = 1000 + Mathf.Pow((float)level, 2f);
            badFaith = 10f + 3f * level;
        }

        #endregion
    }

    [System.Serializable]
    public class State
    {
        #region State Variables

        [Header("Points")]
        public float clickAmount = 0;
        public float level = 0;
        public float initialSoulsCollected = 1000;
        [SerializeField]
        private float soulsCollected = 0;
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
        public float blessingPointsPerSecond = 0;
        public float cursePoints = 0;
        public float cursePointsPerSecond = 0;

        [Space(5)]
        [Header("Game Info")]
        public float soulsCRI = 0;
        public float soulsCRC = 0;
        public float initialDeathRate = 1;
        public float deathRate = 0;
        public float initialBirthRate = 1;
        public float birthRate = 0;
        [Range(0f, 1f)]
        public float faithLevel = 0.5f;
        public float badFaith = 0;
        public float goodFaith = 0;
        public float startGoodFaith = 1;
        public float startBadFaith = 1;

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

    public SoulsCollector GetSoulsCollectorByName(string name)
    {

        name = name.Replace("CP", "").Replace("BP", "");

        if (earthSoulsCollectorClick.name == name)
            return earthSoulsCollectorClick;
        foreach (SoulsCollector soulsCollector in earthSoulsCollector)
        {
            if (soulsCollector.name == name)
                return soulsCollector;
        }
        foreach (SoulsCollector soulsCollector in hellSoulsCollector)
        {
            if (soulsCollector.name == name)
                return soulsCollector;
        }
        foreach (SoulsCollector soulsCollector in heavenSoulsCollector)
        {
            if (soulsCollector.name == name)
                return soulsCollector;
        }

        return null;
    }

    private void Awake()
    {

        gameState.SoulsCollected = gameState.initialSoulsCollected;

        // Tells Singleton GameManager that I'm the main GameController instance  
        GameManager.gameControllerInstance = this;

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
        UpdateGUI();
    }

    public void UpgradeSoulsCollectorLevel(SoulsCollector soulsCollector, bool cp)
    {
        soulsCollector.UpgradeLevel(cp);
    }
    public void CalculateSoulsCollectorLevel(SoulsCollector soulsCollector)
    {
        soulsCollector.CalculateNextLevel();
    }

    /// <summary>
    /// Collects 1 soul
    /// </summary>
    public void CollectSouls()
    {
        CollectSouls(1);
    }

    /// <summary>
    /// Collects Souls
    /// </summary>
    /// <param name="amount">Amount of Souls to Collect</param>
    public void CollectSouls(int amount)
    {
        gameState.clickAmount++;
        float soulsCollectedAmount = Mathf.Min(gameState.SoulsCollected, amount * Mathf.Max(1, gameState.soulsCRC));
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

    public void CheckButtons()
    {
        if (!GameManager.canvasInstance)
            return;

        Button[] buttonList = GameManager.canvasInstance.GetComponentsInChildren<Button>();
        List<Button> upgradeButtons = new List<Button>(buttonList).FindAll(x => x.tag == "Upgrade");

        string SoulsCollectorName = "";
        foreach (Button button in upgradeButtons)
        {
            SoulsCollectorName = button.GetComponentInParent<Image>().name;
            SoulsCollector soulsCollector = GetSoulsCollectorByName(SoulsCollectorName);

            if (soulsCollector == null)
                throw new System.Exception("Souls Collection '" + SoulsCollectorName + "' not found");

            if (SoulsCollectorName.Contains("Bless"))
            {
                if (gameState.blessingPoints >= soulsCollector.cost)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else if (SoulsCollectorName.Contains("Curse"))
            {
                if (gameState.cursePoints >= soulsCollector.cost)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else if (SoulsCollectorName.Contains("Gen"))
            {
                if (button.name.Contains("CP"))
                {
                    if (gameState.cursePoints >= soulsCollector.cost)
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }
                else
                {
                    if (gameState.blessingPoints >= soulsCollector.cost)
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }
            }

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
        CheckButtons();

        if (GameManager.canvasInstance == null)
        {
            return;
        }

        if (infoDisplays == null && GameManager.canvasInstance != null)
        {
            infoDisplays = GameManager.canvasInstance.GetComponentsInChildren<Text>();
        }

        if (infoDisplays == null)
        {
            return;
        }

        string populationDisplay = "Population: " + GameManager.earthInstance.Population.ToString();
        string soulsDisplay = "Souls: " + Mathf.FloorToInt(gameState.SoulsCollected).ToString();
        string bpDisplay = "BP: " + Mathf.FloorToInt(gameState.blessingPoints).ToString();
        string cpDisplay = "CP: " + Mathf.FloorToInt(gameState.cursePoints).ToString();
        string scri = "SCRI/s: " + gameState.soulsCRI.ToString();
        string scrc = "SCRC: " + Mathf.FloorToInt(gameState.soulsCRC).ToString();

        string bps = "BP/s: " + gameState.blessingPointsPerSecond.ToString();
        string cps = "CP/s: " + gameState.cursePointsPerSecond.ToString();

        string birthRate = "Birth/s: " + gameState.birthRate.ToString();
        string deathRate = "Death/s: " + gameState.deathRate.ToString();

        // Temp
        infoDisplays[0].text = populationDisplay;
        infoDisplays[1].text = soulsDisplay;
        infoDisplays[2].text = bpDisplay;
        infoDisplays[3].text = cpDisplay;
        infoDisplays[4].text = scri;
        infoDisplays[5].text = scrc;
        infoDisplays[6].text = bps;
        infoDisplays[7].text = cps;
        infoDisplays[8].text = birthRate;
        infoDisplays[9].text = deathRate;

        Slider faithLevel = GameManager.canvasInstance.GetComponentInChildren<Slider>();
        if (faithLevel != null)
        {
            faithLevel.value = gameState.faithLevel;
        }

        List<Text> genTexts = new List<Text>(infoDisplays);
        foreach (Text text in genTexts.FindAll(x => x.tag == "GenText"))
        {
            string SoulsCollectorName = text.GetComponentInParent<Image>().name;
            SoulsCollector soulsCollector = GameManager.gameControllerInstance.GetSoulsCollectorByName(SoulsCollectorName);

            if (soulsCollector == null)
                throw new System.Exception("Souls Collection '" + SoulsCollectorName + "' not found");

            text.text = "LVL: " + soulsCollector.level;
        }
        foreach (Text text in genTexts.FindAll(x => x.tag == "Cost"))
        {
            string SoulsCollectorName = text.GetComponentInParent<Image>().name;
            SoulsCollector soulsCollector = GameManager.gameControllerInstance.GetSoulsCollectorByName(SoulsCollectorName);

            if (soulsCollector == null)
                throw new System.Exception("Souls Collection '" + SoulsCollectorName + "' not found");

            text.text = "$ " + Mathf.Ceil(soulsCollector.cost).ToString();
        }
    }

    /// <summary>
    /// This is a population generator, handles all the birth and death tasks
    /// and also collects the soul of the dead.
    /// </summary>
    public void HandlePopulation()
    {
        GameManager.earthInstance.Population += gameState.birthRate;
        gameState.blessingPoints += gameState.blessingPointsPerSecond;
        gameState.cursePoints += gameState.cursePointsPerSecond;

        float deathAmount = Mathf.Min(GameManager.earthInstance.Population, gameState.deathRate + gameState.soulsCRI);
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

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void HideCredits()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
