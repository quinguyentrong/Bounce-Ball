using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BounceBall_GameManager : MonoBehaviour
{
    #region BASE
    public static BounceBall_GameManager Instance;
    
    public Action OnSetScore;
    public Action OnNewTurn;
    public Action OnEndTurn;

    private void Start()
    {
        CustomEventManager.Instance.OnNewGame += StartGame;
    }
    private void OnDestroy()
    {
        CustomEventManager.Instance.OnNewGame -= StartGame;
    }
    #endregion BASE

    #region GAME STATE
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
    }

    private void NewTurn()
    {
        StartCoroutine(NewTurnCountdown(3f));
    }

    private void EndTurn()
    {
        OnEndTurn();
    }

    IEnumerator NewTurnCountdown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnNewTurn();
    }
    #endregion GAME STATE

    #region SET SCORE
    private int RedScores = 0;
    private int BlueScores = 0;
    private int WinScores = 7;

    public void SetScore(bool isRedWin)
    {
        if (isRedWin)
        {
            RedScores++;
        }
        else
        {
            BlueScores++;
        }

        EndTurn();

        CustomEventManager.Instance.OnSetScore(new Vector2Int(RedScores, BlueScores));


        if (RedScores == WinScores)
        {
            CustomEventManager.Instance.OnGameOver(true);
            return;
        }

        if (BlueScores == WinScores)
        {
            CustomEventManager.Instance.OnGameOver(false);
            return;
        }

        NewTurn();
    }

    private void StartGame()
    {
        CustomEventManager.Instance.OnSetScore(new Vector2Int(0, 0));
        NewTurn();
    }
    #endregion SET SCORE
}
