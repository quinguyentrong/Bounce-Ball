using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_Blue : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SelfSpriteRenderer;
    private float BlueVelocity = 5f;
    private bool IsPVPMode = false;
    public BounceBall_SpawnBall SpawnBall;
    private float TargerX;
    private bool IsNewTurn = false;

    private void Start()
    {
        BounceBall_GameManager.Instance.OnNewTurn += OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;
        BounceBall_GameManager.Instance.OnPVPMode += OnPVPMode;

        TouchController.Instance.OnTouching_BlueSide += MoveBlue;
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnNewTurn -= OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;
        BounceBall_GameManager.Instance.OnPVPMode -= OnPVPMode;

        TouchController.Instance.OnTouching_BlueSide -= MoveBlue;
    }

    private void Update()
    {
        if (IsPVPMode) return;
        
        if (IsNewTurn == false) return;

        CheckTarget();
        transform.position += new Vector3((TargerX - transform.position.x), 0, 0).normalized * BlueVelocity * Time.deltaTime;
    }

    private void MoveBlue(Vector2 touchPos)
    {
        if (IsPVPMode == false) return;
        transform.position = new Vector3(touchPos.x, 4f, 0);
    }

    private void CheckTarget()
    {
        float maxY = SpawnBall.ObjPooling[0].transform.position.y;
        TargerX = SpawnBall.ObjPooling[0].transform.position.x;

        if (maxY < SpawnBall.ObjPooling[1].transform.position.y)
        {
            TargerX = SpawnBall.ObjPooling[1].transform.position.x;
        }

        if (maxY < SpawnBall.ObjPooling[2].transform.position.y)
        {
            TargerX = SpawnBall.ObjPooling[2].transform.position.x;
        }
    }

    private void OnNewTurn()
    {
        IsNewTurn = true;
        StartCoroutine(Scale(10f));
        StartCoroutine(Scale(20f));
        StartCoroutine(Scale(30f));
        StartCoroutine(Scale(40f));
    }

    private void OnEndTurn()
    {
        IsNewTurn = false;
        StopAllCoroutines();
        SelfSpriteRenderer.size = new Vector2(3.48f, 0.8f);
    }

    private void ScaleGameObj()
    {
        SelfSpriteRenderer.size = new Vector2(SelfSpriteRenderer.size.x - 0.3f, SelfSpriteRenderer.size.y);
    }

    private void OnPVPMode()
    {
        IsPVPMode = true;
    }

    IEnumerator Scale(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ScaleGameObj();
    }
}
