using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Player : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D SelfPolygonCollider2D;
    [SerializeField] private SpriteRenderer SelfSpriteRenderer;
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

        if (IsCanScale == false) return;
        SelfSpriteRenderer.size -= new Vector2(0.01f * Time.deltaTime, 0);
        //SelfPolygonCollider2D.size = SelfSpriteRenderer.size;
        //ERROR
    }

    private void OnNewGame()
    {
        IsCanScale = true;
    }

    private void OnPauseGame()
    {
        IsCanScale = false;
        SelfSpriteRenderer.size = new Vector2(3.48f, 0.8f);
    }
}
