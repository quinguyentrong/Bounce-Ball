using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall_SpawnBall : MonoBehaviour
{
    [SerializeField] private GameObject Ball;
    
    public List<GameObject> ObjPooling = new List<GameObject>();
    private int AmountPool = 3;

    private void Start()
    {
        BounceBall_GameManager.Instance.OnNewTurn += OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn += OnEndTurn;
        
        for (int i = 0; i < AmountPool; i++)
        {
            GameObject obj = Instantiate(Ball);
            obj.SetActive(false);
            ObjPooling.Add(obj);
        }
    }

    private void OnDestroy()
    {
        BounceBall_GameManager.Instance.OnNewTurn -= OnNewTurn;
        BounceBall_GameManager.Instance.OnEndTurn -= OnEndTurn;
    }

    private GameObject GetObjPooling()
    {
        for (int i = 0; i < ObjPooling.Count; i++)
        {
            if (ObjPooling[i].activeInHierarchy == false)
            {
                return ObjPooling[i];
            }
        }
        return null;
    }

    private void Instantiate()
    {
        GameObject ball = GetObjPooling();

        if (ball != null)
        {
            ball.transform.position = new Vector3(0, 0, transform.position.z);
            ball.SetActive(true);
        }
    }
    IEnumerator InstantiateBall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate();
    }

    private void OnNewTurn()
    {
        StartCoroutine(InstantiateBall(0));
        StartCoroutine(InstantiateBall(10f));
        StartCoroutine(InstantiateBall(20f));
    }

    private void OnEndTurn()
    {
        StopAllCoroutines();

        for (int i = 0; i < ObjPooling.Count; i++)
        {
            ObjPooling[i].SetActive(false);
            ObjPooling[i].transform.position = new Vector3(0, 0, 0);
        }
    }
}
