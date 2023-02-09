using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Ball : MonoBehaviour
{
    #region BASE
    private void Start()
    {
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;
    }
    #endregion BASE

    #region GAME STATE
    private bool IsRedWin = false;
    private bool IsSetActive = false;

    private void Update()
    {
        if (IsSetActive) return;

        IsSetActive = true;


        if (IsRedWin)
        {
            Rb2D.velocity = new Vector3(0, 5f);
        }
        else
        {
            Rb2D.velocity = new Vector3(0, -5f);
        }
    }

    private void OnEndTurn()
    {
        IsSetActive = false;
        PoolingSystem.Despawn(gameObject);
    }
    #endregion GAME STATE

    #region VELOCITY
    [SerializeField] private Rigidbody2D Rb2D;

    private void AddVelocityWhenCollision(Vector2 VelocityDirection)
    {
        Rb2D.velocity = VelocityDirection * 10;
    }
    #endregion VELOCITY

    #region COLLISION
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Red" || collision.transform.name == "Blue")
        {
            AddVelocityWhenCollision(((Vector2)transform.position - (Vector2)collision.transform.position).normalized);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "RedGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(true);

            IsRedWin = true;
        }

        if (collision.transform.name == "BlueGainPoint")
        {
            BounceBall_GameManager.Instance.SetScore(false);

            IsRedWin = false;
        }
    }
    #endregion COLLISION
}
