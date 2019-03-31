using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    private Button[] buttons;
    private Image[] images;

    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        images = GetComponentsInChildren<Image>();
    }

    public void Click1()
    {
        buttons[3].interactable = false;
        buttons[2].interactable = true;
        images[5].enabled = false;
    }
    public void Click2()
    {
        buttons[2].interactable = false;
        buttons[1].interactable = true;
        images[4].enabled = false;
    }
    public void Click3()
    {
        buttons[1].interactable = false;
        buttons[0].interactable = true;
        images[3].enabled = false;
    }
    public void Click4()
    {
        SceneManager.LoadScene("Game");
    }
}
