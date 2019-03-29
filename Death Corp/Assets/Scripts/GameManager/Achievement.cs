using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement {

    #region Variables

    public string name;
    public string message;
    public GameController.State requiredState;
    
    #endregion
}
