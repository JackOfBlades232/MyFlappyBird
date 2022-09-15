using UnityEngine;

public class MuteButton : ButtonBase
{
    [SerializeField]
    private Color _liveColor = Color.white;
    
    [SerializeField]
    private Color _mutedColor;
    
    [SerializeField]
    private bool _isMusic;

    private bool _isPressed;

    public override void Initialize()
    {
        base.Initialize();
        LoadInitState();
        OnClick.AddListener(OnPress);
    }

    private void LoadInitState()
    {
        bool isMuted = _isMusic
            ? SaveLoadManager.Instance.PlayerData.MusicIsMuted
            : SaveLoadManager.Instance.PlayerData.SoundIsMuted;
        
        if (isMuted)
            Mute();
        else
            Unmute();

        _isPressed = isMuted;
    }

    private void OnPress()
    {
        if (_isPressed)
            Unmute();
        else
            Mute();

        _isPressed = !_isPressed;

        SaveLoadManager.Instance.OnMuteButtonPressed(
            _isMusic,
            isMuted: _isPressed
        );
    }

    private void Mute()
    {
        Button.image.color = _mutedColor;
        
        if (_isMusic)
            AudioManager.Instance.MuteMusic();
        else
            AudioManager.Instance.MuteSounds();
    }
    
    private void Unmute()
    {
        Button.image.color = _liveColor;
        
        if (_isMusic)
            AudioManager.Instance.UnmuteMusic();
        else
            AudioManager.Instance.UnmuteSounds();
    }
}