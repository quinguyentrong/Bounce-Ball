using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Bot : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D SelfPolygonCollider2D;
    [SerializeField] private SpriteRenderer SelfSpriteRenderer;
    
    public BounceBall_SpawnBall SpawnBall;
    private float TargerX;
    private float BotVelocity = 10f;
    private bool IsNewGame = false;
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
        if (IsNewGame == false) return;
        
        CheckTarget();
        transform.position += new Vector3(Mathf.Sign(TargerX - transform.position.x) * BotVelocity * Time.deltaTime, 0, 0);

        if (IsCanScale == false) return;
        SelfSpriteRenderer.size -= new Vector2(0.01f * Time.deltaTime, 0);
        //SelfPolygonCollider2D.size = SelfSpriteRenderer.size;
        //ERROR
    }

    private void CheckTarget()
    {
        float maxY = SpawnBall.ObjPooling[0].transform.position.y;
        TargerX = SpawnBall.ObjPooling[0].transform.position.x;

        if (maxY < SpawnBall.ObjPooling[1].transform.position.y)
        {
            TargerX = SpawnBall.ObjPooling[1].transform.position.x;
        }

        if (maxY < SpawnBall.ObjPooling[2].transform.position.y)
        {
            TargerX = SpawnBall.ObjPooling[2].transform.position.x;
        }
    }

    private void OnNewGame()
    {
        IsNewGame = true;
        IsCanScale = true;
    }

    private void OnPauseGame()
    {
        IsNewGame = false;
        IsCanScale = false;
        SelfSpriteRenderer.size = new Vector2(3.48f, 0.8f);
    }
}
