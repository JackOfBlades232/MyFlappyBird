using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameUI : MonoBehaviour, IInitializable
{
    private RestartButton _restartButton;

    public void Initialize()
    {
        _restartButton = GetComponentInChildren<RestartButton>();
        
        _restartButton.Initialize();
        _restartButton.OnClick.AddListener(RestartGame);

        gameObject.SetActive(false);
    }
    
    private void RestartGame() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void Activate() => gameObject.SetActive(true);
}