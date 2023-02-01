using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        NewTurn(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player" || collision.transform.name == "Bot")
        {
            AddForceWhenCollision((Vector2)transform.position - (Vector2)collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "PlayerGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(true);
            NewTurn(true);
        }
        
        if(collision.transform.name == "BotGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(false);
            NewTurn(false);
        }
    }

    private void AddForceWhenCollision(Vector2 ForceDirection)
    {
        rb.AddForce(ForceDirection);
        rb.velocity = ForceDirection * 10;
    }

    private void NewTurn(bool isPlayerWin)
    {
        transform.position = new Vector3(0, 0, 0);
        if (isPlayerWin)
        {
            rb.AddForce(new Vector2(0, 10f));
            rb.velocity = new Vector3(0, 20f);
        }
        else
        {
            rb.AddForce(new Vector2(0, -10f));
            rb.velocity = new Vector3(0, -20f);
        }
    }
}
