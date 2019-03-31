using System.Collections;
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
    [Header("Earth Info")]
    [SerializeField]
    private float population = 10000;

    public float Population {
        get {
            return population;
        }

        set {
            population = Mathf.Max(value, 0);
        }
    }

    #endregion

    void Awake()
    {
        // Tells Singleton GameManager that I'm the main Earth instance  
        GameManager.earthInstance = this;
    }
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        FloatingPopupController.Initialize();
    }

    #region Mouse Inputs

    private void OnMouseEnter()
    {
        if (animator)
        {
            animator.SetBool("MouseEnter", true);
        }
    }

    private void OnMouseExit()
    {
        if (animator)
        {
            animator.SetBool("MouseEnter", false);
        }
    }

    private void OnMouseUp()
    {
        GameManager.gameControllerInstance.CollectSouls();

        if (animator)
        {
            animator.SetBool("MouseDown", false);
        }
        GameManager.mousePointerInstance.UnClick();
    }
    private void OnMouseDown() 
    {
        if (animator)
        {
            animator.SetBool("MouseDown", true);
        }
        GameManager.mousePointerInstance.Click();
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
        // Rotate();
    }
}
