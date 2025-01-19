using System;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    public bool IsMovable;
    
    [SerializeField] private Vector3 _positionA;
    [SerializeField] private Vector3 _positionB;
    [SerializeField] private float _moveDuration = 2.0f;
    
    private void Start()
    {
        if (IsMovable)
        {
            MoveToPositionB();
        }
    }

    private void MoveToPositionB()
    {
        transform.DOMove(_positionB, _moveDuration).SetEase(Ease.Linear).OnComplete(MoveToPositionA);
    }

    private void MoveToPositionA()
    {
        transform.DOMove(_positionA, _moveDuration).SetEase(Ease.Linear).OnComplete(MoveToPositionB);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            other.gameObject.GetComponent<Bubble>().Burst();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Burst();
        }
    }

    private void Update()
    {
        if (IsMovable)
        {
            transform.Rotate(Vector3.forward, 180 * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (!IsMovable)
        {
            return;
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_positionA, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_positionB, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_positionA, _positionB);
    }
}
