using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{

    #region Variables

    private Animator animator;

    [Range(1, 240)]
    [SerializeField]
    private int periodSeconds = 120;

    #endregion

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    #region Mouse Inputs

    private void OnMouseDrag()
    {
        //Debug.Log("Mouse Drag");
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        if (animator)
        {
            animator.SetBool("MouseEnter", true);
        }
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse Exit");
        if (animator)
        {
            animator.SetBool("MouseEnter", false);
        }
    }

    private void OnMouseOver()
    {
        //Debug.Log("Mouse Over");
    }

    private void OnMouseUp()
    {
        //Debug.Log("Mouse Up");
        if (animator)
        {
            animator.SetBool("MouseDown", false);
        }
    }
    private void OnMouseDown()
    {
        //Debug.Log("Mouse Down");
        if (animator)
        {
            animator.SetBool("MouseDown", true);
        }
    }

    #endregion

    private void rotate()
    {
        float rpm = (60f / periodSeconds);
        float rotationAngle = 6f * rpm * Time.deltaTime;

        transform.Rotate(Vector3.forward, rotationAngle);
    }

    private void Update()
    {
        rotate();
    }
}
