using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour, IInitializable
{
    private ScoreText _text;

    public UnityEvent<int> OnScoreChanged;
    
    public int Score { get; private set; }
    
    public void Initialize()
    {
        _text = FindObjectOfType<ScoreText>();
        
        _text.Initialize();
        OnScoreChanged.AddListener(_text.SetScoreText);
        
        ResetScore();
    }

    private void ResetScore()
    {
        Score = 0;
        
        OnScoreChanged?.Invoke(Score);
    }

    public void IncrementScore()
    {
        Score++;
        
        OnScoreChanged?.Invoke(Score);
    }
}