using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingPopup : MonoBehaviour
{

    #region Variables

    private Animator animator;

    #endregion

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        if (animator)
        {
            // Pega informações da animação Popup em execução
            AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
            StartCoroutine(InitializePopupCoroutine(clipInfos[0].clip.length));
            
        }
    }

    IEnumerator InitializePopupCoroutine(float clipLength)
    {

        // Wait for the Popup Animation to finish
        yield return new WaitForSeconds(clipLength);
        FloatingPopupController.poolQueue.Enqueue(this);
    }
}
