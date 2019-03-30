using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingPopup : MonoBehaviour
{

    #region Variables

    private Animator animator;
    private ParticleSystem particleSystem;

    #endregion

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        if (animator)
        {
            // Pega informações da animação Popup em execução
            AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
            // Destroi o objeto (popup) após o término da animação
            Destroy(gameObject, clipInfos[0].clip.length);
        }
        if (particleSystem)
        {
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
}
