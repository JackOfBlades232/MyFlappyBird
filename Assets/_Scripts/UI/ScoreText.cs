using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour, IInitializable
{
    private TMP_Text _text;
    
    public void Initialize() => _text = GetComponent<TMP_Text>();

    public void SetScoreText(int score) => _text.text = score.ToString();
}