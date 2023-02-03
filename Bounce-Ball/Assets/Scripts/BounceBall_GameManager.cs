using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BounceBall_GameManager : MonoBehaviour
{
    public static BounceBall_GameManager Instance;
    
    public Action OnNewGame;
    public Action OnPauseGame;

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
        NewGame();
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

        PauseGame();

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

        NewGame();
    }

    public void NewGame()
    {
        StartCoroutine(NewGameCountdown(3f));
    }

    public void PauseGame()
    {
        OnPauseGame();
    }

    IEnumerator NewGameCountdown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnNewGame();
    }
}
