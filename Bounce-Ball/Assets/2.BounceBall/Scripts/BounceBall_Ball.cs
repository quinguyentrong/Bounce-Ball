using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb2D;
    
    private bool IsPlayerWin = false;
    private bool IsSetActive = false;

    private void Start()
    {
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;
    }

    private void Update()
    {
        if (IsSetActive) return;

        IsSetActive = true;


        if (IsPlayerWin)
        {
            Rb2D.velocity = new Vector3(0, 10f);
        }
        else
        {
            Rb2D.velocity = new Vector3(0, -10f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player" || collision.transform.name == "Bot")
        {
            AddVelocityWhenCollision(((Vector2)transform.position - (Vector2)collision.transform.position).normalized);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "PlayerGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(true);

            IsPlayerWin = true;
        }
        
        if(collision.transform.name == "BotGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(false);

            IsPlayerWin = false;
        }
        BounceBall_GameManager.Instance.OnSetScore();
    }

    private void AddVelocityWhenCollision(Vector2 VelocityDirection)
    {
        Rb2D.velocity = VelocityDirection * 10;
    }

    private void OnEndTurn()
    {
        IsSetActive = false;
    }
}