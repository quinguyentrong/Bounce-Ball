using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Player : MonoBehaviour
{
    private Vector3 MousePos;
    private bool IsCanScale = false;

    private void Start()
    {
        BounceBall_GameManager.Instance.OnNewGame += OnNewGame;
        BounceBall_GameManager.Instance.OnPauseGame += OnPauseGame;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnNewGame -= OnNewGame;
        BounceBall_GameManager.Instance.OnPauseGame -= OnPauseGame;
    }

    private void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(MousePos.x, -3.5f, 0);
        
        if (IsCanScale)
        {
            transform.localScale += new Vector3(-0.01f * Time.deltaTime, 0, 0);
        }
    }

    private void OnNewGame()
    {
        IsCanScale = true;
    }
    
    private void OnPauseGame()
    {
        IsCanScale = false;
        transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
    }
}
