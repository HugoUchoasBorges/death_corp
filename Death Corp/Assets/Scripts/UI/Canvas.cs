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
}
