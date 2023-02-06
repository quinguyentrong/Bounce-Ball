using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SelfSpriteRenderer;
    private Vector3 MousePos;

    private void Start()
    {
        BounceBall_GameManager.Instance.OnNewTurn += OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnNewTurn -= OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;
    }

    private void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(MousePos.x, -4.6f, 0);
    }

    private void OnNewTurn()
    {
        StartCoroutine(Scale(10f));
        StartCoroutine(Scale(20f));
        StartCoroutine(Scale(30f));
        StartCoroutine(Scale(40f));
    }

    private void OnEndTurn()
    {
        StopAllCoroutines();
        SelfSpriteRenderer.size = new Vector2(3.48f, 0.8f);
    }

    private void ScaleGameObj()
    {
        SelfSpriteRenderer.size = new Vector2(SelfSpriteRenderer.size.x - 0.3f, SelfSpriteRenderer.size.y);
    }

    IEnumerator Scale(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ScaleGameObj();
    }
}
