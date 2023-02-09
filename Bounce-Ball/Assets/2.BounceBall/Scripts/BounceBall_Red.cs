using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Red : MonoBehaviour
{
    #region BASE
    private void Start()
    {
        BounceBall_GameManager.Instance.OnNewTurn += OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;

        TouchController.Instance.OnTouching_RedSide += MoveRed;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnNewTurn -= OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;

        TouchController.Instance.OnTouching_RedSide -= MoveRed;
    }
    #endregion BASE

    #region GAME STATE
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
    #endregion GAME STATE

    #region MOVE
    private void MoveRed(Vector2 touchPos)
    {
        transform.position = new Vector3(touchPos.x, -4f, 0);
    }
    #endregion MOVE

    #region EFFECT
    [SerializeField] private SpriteRenderer SelfSpriteRenderer;

    private void ScaleGameObject()
    {
        SelfSpriteRenderer.size = new Vector2(SelfSpriteRenderer.size.x - 0.3f, SelfSpriteRenderer.size.y);
    }

    IEnumerator Scale(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ScaleGameObject();
    }
    #endregion EFFECT
}
