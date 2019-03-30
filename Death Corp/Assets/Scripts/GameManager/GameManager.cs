using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    #region Variables

    public static Earth earthInstance;
    public static GameController gameControllerInstance;
    public static Canvas canvasInstance;

    #endregion

    /// <summary>
    /// Method to be called when the player dies all it lifes
    /// </summary>
    public static void GameOver()
    {
        ResetGame();
    }

    ///<summary>
    /// Resets the Game, loading an specified scene
    ///</summary>
    public static void ResetGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        ResetGame(scene);
    }

    /// <summary>
    /// Resets the Game, loading an specified scene
    /// </summary>
    /// <param name="scene">Optional Scene parameter to be loaded</param>
    public static void ResetGame(Scene scene)
    {
        // Loads the previous scene
        SceneManager.LoadScene(scene.name);
    }
}