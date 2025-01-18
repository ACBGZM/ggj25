using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float spawnInterval = 0.8f;
    [SerializeField] private float spawnAreaWidth;
    [SerializeField] private float spawnAreaHeight;

    private void Start()
    {
        StartCoroutine(SpawnBubbles());
    }

    private IEnumerator SpawnBubbles()
    {
        while (true)
        {
            SpawnBubble();
            yield return YieldHelper.WaitForSeconds(spawnInterval, true);
        }
    }

    private void SpawnBubble()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(transform.position.x - spawnAreaWidth, transform.position.x + spawnAreaWidth),
            Random.Range(transform.position.y - spawnAreaHeight, transform.position.y + spawnAreaHeight)
        );

        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 areaLeftBottom = new Vector3(transform.position.x - spawnAreaWidth, transform.position.y - spawnAreaHeight, 0);
        Vector3 areaRightUp = new Vector3(transform.position.x + spawnAreaWidth, transform.position.y + spawnAreaHeight, 0);
        Vector3 size = areaRightUp - areaLeftBottom;
        Gizmos.DrawWireCube(areaLeftBottom + size / 2, size);
    }
}