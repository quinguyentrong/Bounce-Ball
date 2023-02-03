using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    private bool IsPlayerWin = false;
    private bool IsSetActive = false;

    private void Update()
    {
        if (IsSetActive) return;

        IsSetActive = true;

        if (IsPlayerWin)
        {
            rb.velocity = new Vector3(0, 10f);
        }
        else
        {
            rb.velocity = new Vector3(0, -10f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player" || collision.transform.name == "Bot")
        {
            AddVelocityWhenCollision((Vector2)transform.position - (Vector2)collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "PlayerGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(true);

            IsPlayerWin = true;
            IsSetActive = false;//ERROR
        }
        
        if(collision.transform.name == "BotGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(false);

            IsPlayerWin = false;
            IsSetActive = false;//ERROR
        }
    }

    private void AddVelocityWhenCollision(Vector2 VelocityDirection)
    {
        rb.velocity = VelocityDirection * 10;
    }
}
