using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Collider2D _collider;
    
    public void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            || other.gameObject.CompareTag("Bubble"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
