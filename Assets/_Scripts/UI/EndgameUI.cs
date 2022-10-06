using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameUI : MonoBehaviour, IInitializable
{
    [SerializeField]
    private ScoreText _scoreText, _highScoreText;

    [SerializeField]
    private Image _medal; 

    [SerializeField]
    private Sprite _silverMedal, _goldMedal;

    private GameParams _params;

    private ButtonBase _restartButton;
    private SettingsMenu _settingsMenu;

    private PlayerData _playerData;

    public void Initialize()
    {
        _restartButton = GetComponentInChildren<ButtonBase>();
        _settingsMenu = GetComponentInChildren<SettingsMenu>();
        
        _restartButton.Initialize();
        _restartButton.OnClick.AddListener(() =>
            AudioManager.Instance.PlaySound(SoundType.ButtonPress));
        _restartButton.OnClick.AddListener(RestartGame);
        
        _scoreText.Initialize();
        _highScoreText.Initialize();
        _settingsMenu.Initialize();

        gameObject.SetActive(false);
    }

    public void Construct(GameParams gameParas, PlayerData playerData)
    {
        _params = gameParas;
        _playerData = playerData;
    }

    private void RestartGame()
    {
        AdsManager.Instance.HideBanner();
        AudioManager.Instance.StopAllMusic();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChooseMedal()
    {
        if (_playerData.LastScore >= _params.GoldMedalScoreThreshold)
            _medal.sprite = _goldMedal;
        else
            _medal.sprite = _silverMedal;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        
        _scoreText.SetScoreText(_playerData.LastScore);
        _highScoreText.SetScoreText(_playerData.HighScore);
        
        ChooseMedal();
    }
}