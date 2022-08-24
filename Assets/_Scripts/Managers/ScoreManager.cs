using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour, IInitializable
{
    private ScoreText _text;
    
    private int _score;

    public UnityEvent<int> OnScoreChanged;
    
    public void Initialize()
    {
        _text = FindObjectOfType<ScoreText>();
        
        _text.Initialize();
        OnScoreChanged.AddListener(_text.SetScoreText);
        
        ResetScore();
    }

    private void ResetScore()
    {
        _score = 0;
        
        OnScoreChanged?.Invoke(_score);
    }

    public void IncrementScore()
    {
        _score++;
        
        OnScoreChanged?.Invoke(_score);
    }
}