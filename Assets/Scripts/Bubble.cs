using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using Unity.Mathematics;

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

    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    
    protected Collider2D _collider2D;
    
    private SpriteRenderer _spriteRenderer;

    protected bool _isMerging = false;
    
    public void Awake()
    {
        _initialScale = Random.Range(GameplaySettings.BubbleInitialScaleMin, GameplaySettings.BubbleInitialScaleMax);
        _scale = _initialScale;
        _expectLifeTime = (_initialScale - GameplaySettings.BubbleMinScale) / GameplaySettings.BubbleShrinkRate;
     
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    public void FixedUpdate()
    {
        if (!_isMerging)
        {
            Shrink();
            Floating();
        }
    }

    protected void Shrink()
    {
        _scale -= GameplaySettings.BubbleShrinkRate * Time.deltaTime;
        if (_scale < GameplaySettings.BubbleMinScale)
        {
            Burst();
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
            _verticalSpeed = -0.8f * (_scale - GameplaySettings.BubbleScaleThreshold);
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
        _isMerging = true;
        
        float combinedScale = Mathf.Sqrt(_scale * _scale + otherBubble._scale * otherBubble._scale);
        _scale = combinedScale > GameplaySettings.BubbleMaxScale ? GameplaySettings.BubbleMaxScale : combinedScale;
        
        Destroy(otherBubble.gameObject);

        transform.DOScale(new Vector3(_scale, _scale, 1), 0.75f).OnComplete(() =>
        {
            _isMerging = false;
        });
    }

    public virtual void Burst()
    {
        _spriteRenderer.DOFade(0, 0.5f);
        _collider2D.enabled = false;
        this.enabled = false;

        ParticleSystem burstEffect = Instantiate(GameplayManager.GetInstance().BubbleBurstParticlePrefab, transform.position, Quaternion.identity);
        float scale = Mathf.Min(transform.localScale.x, 1.5f);
        burstEffect.transform.localScale = new Vector3(scale, scale, 1);
        burstEffect.Play();
        Destroy(burstEffect.gameObject, burstEffect.main.duration);
        
        StartCoroutine(AfterBurstImpl());
    }

    protected virtual IEnumerator AfterBurstImpl()
    {
        gameObject.SetActive(false);
        yield return YieldHelper.WaitForSeconds(0.8f);
        Destroy(gameObject);
        yield return null;
    }
}