using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Bot : MonoBehaviour
{
    [SerializeField] private BounceBall_Ball Ball;
    
    private void Update()
    {
        transform.position = new Vector3(Ball.transform.position.x, 3.5f, 0);
        transform.localScale += new Vector3(-0.001f, 0, 0)* Time.deltaTime;
    }
}
