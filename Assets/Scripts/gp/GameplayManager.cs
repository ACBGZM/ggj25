using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager _instance;
    public ParticleSystem BubbleBurstParticlePrefab;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public static GameplayManager GetInstance()
    {
        return _instance;
    }

    public bool IsGameOver { get; set; }
    
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
