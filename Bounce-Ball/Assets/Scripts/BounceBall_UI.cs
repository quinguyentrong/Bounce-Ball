using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BounceBall_UI : MonoBehaviour
{
    [SerializeField] private GameObject LeftBorder;
    [SerializeField] private GameObject RightBorder;
    [SerializeField] private TextMeshProUGUI ScoreText;

    private float Ratio0 = 1080f / 1920f;
    private float Ratio1;


    private void Start()
    {
        Ratio1 = (float)Screen.width / (float)Screen.height;

        LeftBorder.transform.position = new Vector3(transform.position.x + 5 * (Ratio0 - Ratio1)-3.3f, 0, 0);
        RightBorder.transform.position = new Vector3(transform.position.x + 5 * (Ratio1 - Ratio0)+3.3f, 0, 0);

        BounceBall_GameManager.Instance.OnPauseGame += OnPauseGame;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnPauseGame -= OnPauseGame;
    }

    private void OnPauseGame()
    {
        ScoreText.text = $"<color=#FF0000>{BounceBall_GameManager.Instance.PlayerScores}</color> <color=#000000>-</color> <color=#0000FF>{BounceBall_GameManager.Instance.BotScores} </color>";
    }
}
