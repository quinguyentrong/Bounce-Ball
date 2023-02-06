using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BounceBall_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;

    private void Start()
    {

        BounceBall_GameManager.Instance.OnSetScore += OnSetScore;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnSetScore -= OnSetScore;
    }

    private void OnSetScore()
    {
        ScoreText.text = $"<color=#F0684B>{BounceBall_GameManager.Instance.PlayerScores}</color> <color=#000000>-</color> <color=#6A70BD>{BounceBall_GameManager.Instance.BotScores} </color>";
    }
}
