using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Blue : MonoBehaviour
{
    #region BASE
    private void Start()
    {
        BounceBall_GameManager.Instance.OnNewTurn += OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;

        if (GameConfig.IsPvPMode)
        {
            TouchController.Instance.OnTouching_BlueSide += MoveBlue;
        }
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnNewTurn -= OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;

        if (GameConfig.IsPvPMode)
        {
            TouchController.Instance.OnTouching_BlueSide -= MoveBlue;
        }
    }
    #endregion BASE

    #region GAME STATE

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
    #endregion GAME STATE

    #region MOVE
    private void MoveBlue(Vector2 touchPos)
    {
        transform.position = new Vector3(touchPos.x, 4f, 0);
    }
    #endregion MOVE

    #region EFFECT
    [SerializeField] private SpriteRenderer SelfSpriteRenderer;

    private void ScaleGameObject()
    {
        SelfSpriteRenderer.size = new Vector2(SelfSpriteRenderer.size.x - 0.3f, SelfSpriteRenderer.size.y);
    }

    IEnumerator Scale(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ScaleGameObject();
    }
    #endregion EFFECT

    #region BOT
    public BounceBall_SpawnBall SpawnBall;

    private float TargerX;
    private bool IsNewTurn = false;
    private float BlueVelocity = 5f;

    private void Update()
    {
        if (GameConfig.IsPvPMode) return;

        if (IsNewTurn == false) return;

        CheckTarget();
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargerX, transform.position.y), Time.deltaTime * BlueVelocity);
    }

    private void CheckTarget()
    {
        float temp = -100f;

        for (int i = 0; i < SpawnBall.BallList.Count; i++)
        {
            if (temp < SpawnBall.BallList[i].transform.position.y)
            {
                temp = SpawnBall.BallList[i].transform.position.y;

                TargerX = SpawnBall.BallList[i].transform.position.x;
            }
        }
    }
    #endregion BOT
}
