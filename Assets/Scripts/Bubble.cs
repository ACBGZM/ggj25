using Unity.Mathematics;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    private float _verticalSpeed;

    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    private float _scale;

    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    private float _expectLifeTime;

    [SerializeField] private float _shrinkRate = 0.1f;
    [SerializeField] private float _minScale = 0.5f;

    public void Awake()
    {
        _scale = GameplaySettings.BubbleInitialScale;

        _expectLifeTime = (GameplaySettings.BubbleInitialScale - _minScale) / _shrinkRate;
    }

    public void FixedUpdate()
    {
        Shrink();
        Floating();
    }

    private void Shrink()
    {
        _scale -= _shrinkRate * Time.deltaTime;
        if (_scale < _minScale)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = new Vector3(_scale, _scale, 1);
        }
    }

    private void Floating()
    {
        _verticalSpeed = 1 / math.pow(_scale, 2);
        transform.position += Vector3.up * _verticalSpeed * Time.fixedDeltaTime;
    }
}