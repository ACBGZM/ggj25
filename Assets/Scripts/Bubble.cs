using UnityEngine;
using Random = UnityEngine.Random;

public class Bubble : MonoBehaviour
{
    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    protected float _verticalSpeed;

    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    protected float _scale;

    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    protected float _initialScale;

    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    protected float _expectLifeTime;

    [SerializeField] protected float _shrinkRate = 0.1f;
    [SerializeField] protected float _minScale = 0.2f;

    public void Awake()
    {
        _initialScale = Random.Range(GameplaySettings.BubbleInitialScaleMin, GameplaySettings.BubbleInitialScaleMax);
        _scale = _initialScale;
        _expectLifeTime = (_initialScale - _minScale) / _shrinkRate;
    }

    public void FixedUpdate()
    {
        Shrink();
        Floating();
    }

    protected void Shrink()
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
        CalculateVerticalSpeed();
        transform.position += _verticalSpeed * Time.fixedDeltaTime * Vector3.up;
    }

    protected void CalculateVerticalSpeed()
    {
        if (_scale > GameplaySettings.BubbleScaleThreshold)
        {
            _verticalSpeed = -0.8f * Mathf.Pow(_scale - GameplaySettings.BubbleScaleThreshold, 2);
        }
        else
        {
            _verticalSpeed = 0.4f / Mathf.Pow(_scale, 2);
        }
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            Bubble otherBubble = other.gameObject.GetComponent<Bubble>();
            if (otherBubble != null && this.enabled && otherBubble.enabled)
            {
                MergeBubbles(otherBubble);
            }
        }
    }

    private void MergeBubbles(Bubble otherBubble)
    {
        float combinedScale = Mathf.Sqrt(_scale * _scale + otherBubble._scale * otherBubble._scale);
        _scale = combinedScale > GameplaySettings.BubbleMaxScale ? GameplaySettings.BubbleMaxScale : combinedScale;
        transform.localScale = new Vector3(_scale, _scale, 1);

        Destroy(otherBubble.gameObject);
    }
}