﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{

    #region Variables

    private Animator animator;

    [Header("Earth Configuration")]

    [Range(1, 240)]
    [SerializeField]
    private int periodSeconds = 120;

    [Space(5)]
    [Header("Game Info")]
    [SerializeField]
    private int soulsCollected = 0;

    #endregion

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void CollectSoul()
    {
        soulsCollected++;
        Debug.Log("Soul Collected");
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
        CollectSoul();

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

    private void Rotate()
    {
        float rpm = (60f / periodSeconds);
        float rotationAngle = 6f * rpm * Time.deltaTime;

        transform.Rotate(Vector3.forward, rotationAngle);
    }

    private void Update()
    {
        Rotate();
    }
}
