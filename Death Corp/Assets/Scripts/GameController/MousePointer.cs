using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{

    #region Variables

    public Animator animator;

    #endregion

    private void Awake()
    {
        GameManager.mousePointerInstance = this;
        Cursor.visible = false;

        animator = gameObject.GetComponent<Animator>();
    }

    public void Click()
    {
        if (animator)
        {
            if(animator.GetBool("MouseClick") == true)
            {
                animator.Play("MouseClick", -1, 0f);
            }else
            {
                animator.SetBool("MouseClick", true);
            }
        }
    }

    public void UnClick()
    {
        if (animator)
        {
            animator.SetBool("MouseClick", false);
        }
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
