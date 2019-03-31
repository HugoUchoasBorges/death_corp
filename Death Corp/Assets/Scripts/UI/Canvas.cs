using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{

    private void Awake()
    {
        GameManager.canvasInstance = this;
    }

    public void UpgradeSoulCollector(string name)
    {
        GameController.SoulsCollector soulsCollector = GameManager.gameControllerInstance.GetSoulsCollectorByName(name);

        if (soulsCollector == null)
            throw new System.Exception("Souls Collection '" + name + "' not found");

        GameManager.gameControllerInstance.UpgradeSoulsCollectorLevel(soulsCollector);
        GameManager.gameControllerInstance.UpdateGUI();
    }

    #region UpgradeGen Methods

    public void UpgradeGen1()
    {
        UpgradeSoulCollector("Gen1");
    }

    public void UpgradeGen2()
    {
        UpgradeSoulCollector("Gen2");
    }

    public void UpgradeGen3()
    {
        UpgradeSoulCollector("Gen3");
    }

    public void UpgradeGen4()
    {
        UpgradeSoulCollector("Gen4");
    }

    public void UpgradeGen5()
    {
        UpgradeSoulCollector("Gen5");
    }

    #endregion
}
