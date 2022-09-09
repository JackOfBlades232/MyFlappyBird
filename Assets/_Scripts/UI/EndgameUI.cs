using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameUI : MonoBehaviour, IInitializable
{
    private ButtonBase _restartButton;
    private ScoreText _highScoreText;
    private SettingsMenu _settingsMenu;

    private PlayerData _playerData;

    public void Initialize()
    {
        _restartButton = GetComponentInChildren<ButtonBase>();
        _highScoreText = GetComponentInChildren<ScoreText>();
        _settingsMenu = GetComponentInChildren<SettingsMenu>();
        
        _restartButton.Initialize();
        _restartButton.OnClick.AddListener(() =>
            AudioManager.Instance.PlaySound(SoundType.ButtonPress));
        _restartButton.OnClick.AddListener(RestartGame);
        
        _highScoreText.Initialize();
        _settingsMenu.Initialize();

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