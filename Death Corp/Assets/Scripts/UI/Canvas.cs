using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{

    private void Awake()
    {
        GameManager.canvasInstance = this;
    }

    public void UpgradeSoulCollector(string name, bool cp)
    {
        GameController.SoulsCollector soulsCollector = GameManager.gameControllerInstance.GetSoulsCollectorByName(name);

        if (soulsCollector == null)
            throw new System.Exception("Souls Collection '" + name + "' not found");

        GameManager.gameControllerInstance.UpgradeSoulsCollectorLevel(soulsCollector, cp);
        GameManager.gameControllerInstance.UpdateGUI();
    }

    public void UpgradeSoulCollector(string name)
    {
        UpgradeSoulCollector(name, false);
    }

    public void UpgradeSoulCollectorCP(string name)
    {
        UpgradeSoulCollector(name, true);
    }

}
