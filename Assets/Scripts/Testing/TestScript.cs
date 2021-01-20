using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Vector3 m_offset;
    public float m_zcord;

    private void OnMouseDown()
    {
        m_zcord = Camera.main.WorldToScreenPoint(transform.position).z;
        m_offset = transform.position - GetmouseWorldPos();
    }

    private Vector3 GetmouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = m_zcord;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDrag()
    {
        transform.position = GetmouseWorldPos() + m_offset;
    }
}
