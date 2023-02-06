using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BounceBall_GameManager : MonoBehaviour
{
    public static BounceBall_GameManager Instance;
    public Action OnSetScore;
    public Action OnNewTurn;
    public Action OnEndTurn;

    private int PlayerScores = 0;
    private int BotScores = 0;

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
    }

    private void Start()
    {
        CustomEventManager.Instance.OnNewGame += StartGame;
    }
    private void OnDestroy()
    {
        CustomEventManager.Instance.OnNewGame -= StartGame;
    }

    public void SetScore(bool isPlayerWin)
    {
        if (isPlayerWin)
        {
            PlayerScores++;
        }
        else
        {
            BotScores++;
        }

        EndTurn();
        CustomEventManager.Instance.OnSetScore(new Vector2Int(PlayerScores, BotScores));


        if (PlayerScores == 7)
        {
            CustomEventManager.Instance.OnGameOver(true);
            return;
        }
        
        if (BotScores == 7)
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
}
