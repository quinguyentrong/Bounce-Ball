using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Bot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SelfSpriteRenderer;
    private float BotVelocity = 5f;

    public BounceBall_SpawnBall SpawnBall;
    private float TargerX;
    private bool IsNewTurn = false;

    private void Start()
    {
        BounceBall_GameManager.Instance.OnNewTurn += OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnNewTurn -= OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;
    }

    private void Update()
    {
        if (IsNewTurn == false) return;

        CheckTarget();
        transform.position += new Vector3((TargerX - transform.position.x), 0, 0).normalized * BotVelocity * Time.deltaTime;
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

    private void OnNewTurn()
    {
        IsNewTurn = true;
        StartCoroutine(Scale(10f));
        StartCoroutine(Scale(20f));
        StartCoroutine(Scale(30f));
        StartCoroutine(Scale(40f));
    }

    private void OnEndTurn()
    {
        IsNewTurn = false;
        StopAllCoroutines();
        SelfSpriteRenderer.size = new Vector2(3.48f, 0.8f);
    }

    private void ScaleGameObj()
    {
        SelfSpriteRenderer.size = new Vector2(SelfSpriteRenderer.size.x - 0.3f, SelfSpriteRenderer.size.y);
    }

    IEnumerator Scale(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ScaleGameObj();
    }
}
