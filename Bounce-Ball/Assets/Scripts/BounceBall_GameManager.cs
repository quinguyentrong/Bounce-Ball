using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BounceBall_GameManager : MonoBehaviour
{
    public static BounceBall_GameManager Instance;
    public BounceBall_Ball Ball;
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
        StartCoroutine(InstantiateBall(10f));
        StartCoroutine(InstantiateBall(20f));
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

        StartCoroutine(InstantiateBall(10f));
        StartCoroutine(InstantiateBall(20f));
    }

    IEnumerator InstantiateBall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(Ball, Vector3.zero, Quaternion.identity);
    }
}
