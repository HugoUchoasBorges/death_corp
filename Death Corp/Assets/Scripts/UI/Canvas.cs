using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Canvas : MonoBehaviour
{

    private void Awake()
    {
        GameManager.canvasInstance = this;
    }

    private void Start()
    {
        GameManager.gameControllerInstance.UpdateGUI();
        GameManager.gameControllerInstance.CheckButtons();
    }

    public void UpgradeSoulCollector(string name, bool cp)
    {
        GameController.SoulsCollector soulsCollector = GameManager.gameControllerInstance.GetSoulsCollectorByName(name);

        if (soulsCollector == null)
            throw new System.Exception("Souls Collection '" + name + "' not found");

        GameManager.gameControllerInstance.UpgradeSoulsCollectorLevel(soulsCollector, cp);
        GameManager.gameControllerInstance.UpdateGUI();
    }

    public void UpgradeSoulCollector()
    {
        GameObject btnObject = EventSystem.current.currentSelectedGameObject;

        if (!btnObject)
            return;

        string SoulsCollectorName = btnObject.GetComponentInParent<Image>().name;

        if (EventSystem.current.currentSelectedGameObject.name.Contains("CP"))
        {
            UpgradeSoulCollector(SoulsCollectorName, true);
        }
        else
        {
            UpgradeSoulCollector(SoulsCollectorName);
        }
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
