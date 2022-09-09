using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameUI : MonoBehaviour, IInitializable
{
    private RestartButton _restartButton;
    private ScoreText _highScoreText;

    private PlayerData _playerData;

    public void Initialize()
    {
        _restartButton = GetComponentInChildren<RestartButton>();
        _highScoreText = GetComponentInChildren<ScoreText>();
        
        _restartButton.Initialize();
        _restartButton.OnClick.AddListener(() =>
            AudioManager.Instance.PlaySound(SoundType.ButtonPress));
        _restartButton.OnClick.AddListener(RestartGame);
        
        _highScoreText.Initialize();

        gameObject.SetActive(false);
    }

    public void Construct(PlayerData playerData) => _playerData = playerData;
    
    private void RestartGame()
    {
        AudioManager.Instance.StopAllMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        
        _highScoreText.SetScoreText(_playerData.HighScore);
    }
}