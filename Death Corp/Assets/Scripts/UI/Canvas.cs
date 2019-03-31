using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {

    private void Awake()
    {
        GameManager.canvasInstance = this;
    }

    public void UpgradeEarthSoulsCollectorClick()
    {
        GameManager.gameControllerInstance.UpgradeSoulsCollectorLevel(GameManager.gameControllerInstance.earthSoulsCollectorClick);
    }
}
