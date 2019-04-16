using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour
{

    #region Variables

    public Button nextButton;
    public Button backButton;

    public List<Image> imageSequence;
    private List<Image> clickedImages;

    #endregion

    private void Start()
    {
        clickedImages = new List<Image>();
    }

    public void Previous()
    {
        if (clickedImages.Count > 0)
        {
            Image image = clickedImages[0];
            image.enabled = true;

            imageSequence.Insert(0, image);
            clickedImages.Remove(image);
        }
        CheckButtons();
    }

    public void Next()
    {
        if (imageSequence.Count <= 1)
            PlayGame();
        else
        {
            Image image = imageSequence[0];
            image.enabled = false;

            clickedImages.Insert(0, image);
            imageSequence.Remove(image);
        }
        CheckButtons();
    }

    private void CheckButtons()
    {
        if (backButton)
        {
            if (clickedImages.Count > 0)
                backButton.interactable = true;
            else
                backButton.interactable = false;
        }

        if (nextButton)
        {
            if (imageSequence.Count <= 1)
                nextButton.GetComponentInChildren<Text>().text = "PLAY";
            else
                nextButton.GetComponentInChildren<Text>().text = "NEXT";
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
