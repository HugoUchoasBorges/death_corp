using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{

    #region Variables

    [Range(1, 240)]
    [SerializeField]
    private int periodSeconds = 120;

    #endregion

    #region Mouse Inputs

    private void OnMouseDrag()
    {
        //Debug.Log("Mouse Drag");
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse Exit");
    }

    private void OnMouseOver()
    {
        //Debug.Log("Mouse Over");
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up");
    }
    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");
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
