using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{

    #region Variables

    public Animator animator;
    private Text textComponent;

    public Color positiveColor;
    public Color negativeColor;

    #endregion Variables

    private void Awake()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        textComponent = gameObject.GetComponentInChildren<Text>();
    }

    public void SetText(string text)
    {
        textComponent.text = text;
    }

    public void SetColor(Color c)
    {
        textComponent.color = c;
    }
}
