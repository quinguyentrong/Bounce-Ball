using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Player : MonoBehaviour
{
    private Vector3 MousePos;

    private void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(MousePos.x, -3.5f, 0);
        transform.localScale += new Vector3(-0.001f*Time.deltaTime, 0, 0);
    }
}
