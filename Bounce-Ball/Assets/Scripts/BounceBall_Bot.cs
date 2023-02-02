using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Bot : MonoBehaviour
{
    //[SerializeField] private List<BounceBall_Ball> Ball;

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
        //transform.position = new Vector3(Ball.transform.position.x, 3.5f, 0);
        
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
