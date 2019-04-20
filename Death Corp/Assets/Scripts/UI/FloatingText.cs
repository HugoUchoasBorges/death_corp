using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{

    #region Variables

    public Animator animator;
    private Text textValue;

    public Color positiveColor;
    public Color negativeColor;

    #endregion Variables

    private void Awake()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        textValue = gameObject.GetComponentInChildren<Text>();
    }

    public void SetText(string text)
    {
        textValue.text = text;
    }

    public void SetColor(Color color)
    {
        textValue.color = color;
    }
}
