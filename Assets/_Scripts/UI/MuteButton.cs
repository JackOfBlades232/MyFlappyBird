using UnityEngine;

public class MuteButton : ButtonBase
{
    [SerializeField]
    private bool _isMusic;

    private bool _isPressed;

    public override void Initialize()
    {
        base.Initialize();
        
        Button.image.color = Color.white;
        
        OnClick.AddListener(OnPress);
    }

    private void OnPress()
    {
        if (_isPressed)
            Unmute();
        else
            Mute();

        _isPressed = !_isPressed;
    }

    private void Mute()
    {
        // TODO : refactor this?
        Button.image.color = Color.gray;

        Debug.Log("Mute");
        
        if (_isMusic)
            AudioManager.Instance.MuteMusic();
        else
            AudioManager.Instance.MuteSounds();
    }
    
    private void Unmute()
    {
        Button.image.color = Color.white;

        Debug.Log("Unmute");
        
        if (_isMusic)
            AudioManager.Instance.UnmuteMusic();
        else
            AudioManager.Instance.UnmuteSounds();
    }
}