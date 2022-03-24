using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamara : MonoBehaviour
{
    public CinemachineCameraOffset offset;
    public float offsetX, offsetY, maxOffset;
    float valor;

    public float EjeX;
    public float EjeY;

    private void Start()
    {
        valor = 0;
    }
    void Update()
    {
        EjeX = Input.GetAxis("Mouse X");
        EjeY = Input.GetAxis("Mouse Y");
        //if (Input.GetAxis("Mouse X")>0 && Mathf.Abs(offset.m_Offset.x) < Mathf.Abs(maxOffset))
        //{
        //    Debug.Log("x");
        //    offset.m_Offset.x += offsetX;
        //}
        //else if (Input.GetAxis("Mouse X")<0 && Mathf.Abs(offset.m_Offset.x) < Mathf.Abs(maxOffset))
        //{
        //    Debug.Log("-x");
        //    offset.m_Offset.x -= offsetX;
        //}
        //else if (Input.GetAxis("Mouse Y")>0 && Mathf.Abs(offset.m_Offset.y) < Mathf.Abs(maxOffset))
        //{
        //    Debug.Log("y");
        //    offset.m_Offset.y -= offsetY;
        //}
        //else if (Input.GetAxis("Mouse Y")<0  && Mathf.Abs(offset.m_Offset.y) < Mathf.Abs(maxOffset))
        //{
        //    Debug.Log("-y");
        //    offset.m_Offset.y += offsetY;
        //}
        //else
        //{
        //    offset.m_Offset = Vector2.zero;
        //}
        if(((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse X") < 0)|| (Input.GetAxis("Mouse X Teclado") > 0 || Input.GetAxis("Mouse X Teclado") < 0)) || ((Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse Y") > 0) || (Input.GetAxis("Mouse Y Teclado") > 0 || Input.GetAxis("Mouse Y Teclado") < 0)))
        {
            if((Input.GetAxis("Mouse X") > 0  || Input.GetAxis("Mouse X Teclado") > 0) && Mathf.Abs(offset.m_Offset.x)<maxOffset)
            {
                offset.m_Offset.x += offsetX;
            }
            else if ((Input.GetAxis("Mouse X") < 0  || Input.GetAxis("Mouse X Teclado") < 0) && Mathf.Abs(offset.m_Offset.x) < maxOffset)
            {
                offset.m_Offset.x -= offsetX;
            }
            if ((Input.GetAxis("Mouse Y") > 0 || Input.GetAxis("Mouse Y Teclado") > 0) && Mathf.Abs(offset.m_Offset.y) < maxOffset)
            {
                offset.m_Offset.y += offsetY;
            }
            else if ((Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse Y Teclado") < 0) && Mathf.Abs(offset.m_Offset.y) < maxOffset)
            {
                offset.m_Offset.y -= offsetY;
            }
        }
        else
        {           
            if (offset.m_Offset.x < 0)
            {
                offset.m_Offset.x += offsetX;
            }
            if (offset.m_Offset.x > 0)
            {
                offset.m_Offset.x -= offsetX;
            }
            if (offset.m_Offset.y < 0)
            {
                offset.m_Offset.y += offsetY;
            }
            if (offset.m_Offset.y > 0)
            {
                offset.m_Offset.y -= offsetY;
            }

        }


    }
}
