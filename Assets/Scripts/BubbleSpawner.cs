using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bubblePrefab;
    [SerializeField] private float _spawnInterval = 0.8f;
    [SerializeField] private float _spawnAreaWidth;
    [SerializeField] private float _spawnAreaHeight;
    
    [SerializeField] private Transform _playerTransform;

    private void Start()
    {
        StartCoroutine(SpawnBubbles());
    }

    private IEnumerator SpawnBubbles()
    {
        while (!GameplayManager.GetInstance().IsGameOver)
        {
            SpawnBubble();
            yield return YieldHelper.WaitForSeconds(_spawnInterval, true);
        }

        yield return null;
    }

    private void SpawnBubble()
    {
        Vector2 spawnPosition;
        float distance;
        do
        {
            spawnPosition = new Vector2(
                Random.Range(transform.position.x - _spawnAreaWidth, transform.position.x + _spawnAreaWidth),
                Random.Range(transform.position.y - _spawnAreaHeight, transform.position.y + _spawnAreaHeight)
            );
            distance = Vector2.Distance(spawnPosition, _playerTransform.position);
        } while (distance < GameplaySettings.BubbleSpawnMinDistance);
        
        Instantiate(_bubblePrefab, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 areaLeftBottom = new Vector3(transform.position.x - _spawnAreaWidth, transform.position.y - _spawnAreaHeight, 0);
        Vector3 areaRightUp = new Vector3(transform.position.x + _spawnAreaWidth, transform.position.y + _spawnAreaHeight, 0);
        Vector3 size = areaRightUp - areaLeftBottom;
        Gizmos.DrawWireCube(areaLeftBottom + size / 2, size);
    }
}