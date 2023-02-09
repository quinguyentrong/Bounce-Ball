using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_SpawnBall : MonoBehaviour
{
    #region BASE
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
    #endregion BASE

    #region GAME STATE
    private void OnNewTurn()
    {
        StartCoroutine(SpawnBall(0f));
        StartCoroutine(SpawnBall(10f));
        StartCoroutine(SpawnBall(20f));
    }

    private void OnEndTurn()
    {
        StopAllCoroutines();
        for (int i = 0; i < BallList.Count; i++)
        {
            PoolingSystem.Despawn(BallList[i]);
        }
    }
    #endregion GAME STATE

    #region SPAWN BALL
    [SerializeField] private BounceBall_Ball Ball;
    public List<GameObject> BallList = new List<GameObject>();

    IEnumerator SpawnBall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SpawnGameObject();
    }

    private void SpawnGameObject()
    {
        var ball = PoolingSystem.Spawn(Ball);
        BallList.Add(ball.gameObject);
    }
    #endregion SPAWN BALL
}
