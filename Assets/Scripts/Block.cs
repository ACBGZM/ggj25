using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            Destroy(other.gameObject);
            // Destroy(gameObject);
        }
    }
}
