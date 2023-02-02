using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BounceBall_GameManager : MonoBehaviour
{
    public static BounceBall_GameManager Instance;
    
    public Action OnNewGame;
    public Action OnPauseGame;
    public TextMeshProUGUI ScoreText;
    
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
        
        ScoreText.text = $"<color=#FF0000>{PlayerScores}</color> <color=#000000>-</color> <color=#0000FF>{BotScores} </color>";

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
