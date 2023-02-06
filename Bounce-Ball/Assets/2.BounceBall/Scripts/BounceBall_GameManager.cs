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

    public int PlayerScores = 0;
    public int BotScores = 0;

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
        NewTurn();
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

        if (PlayerScores == 7)
        {
            Debug.Log("PLAYER WIN");
            return;
        }
        
        if (BotScores == 7)
        {
            Debug.Log("BOT WIN");
            return;
        }

        NewTurn();
    }

    public void NewTurn()
    {
        StartCoroutine(NewTurnCountdown(3f));
    }

    public void EndTurn()
    {
        OnEndTurn();
    }

    IEnumerator NewTurnCountdown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnNewTurn();
    }
}
