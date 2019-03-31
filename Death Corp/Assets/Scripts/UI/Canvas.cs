using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{

    private void Awake()
    {
        GameManager.canvasInstance = this;
    }

    #region UpgradeGen Methods

    public void UpgradeGen(string name)
    {
        GameController.SoulsCollector soulsCollector = GameManager.gameControllerInstance.GetSoulsCollectorByName(name);

        if (soulsCollector == null)
            throw new System.Exception("Souls Collection '" + name + "' not found");

        GameManager.gameControllerInstance.UpgradeSoulsCollectorLevel(soulsCollector);
        GameManager.gameControllerInstance.UpdateGUI();
    }

    public void UpgradeGen1()
    {
        UpgradeGen("Gen1");
    }

    public void UpgradeGen2()
    {
        UpgradeGen("Gen2");
    }

    #endregion
}
