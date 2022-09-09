using UnityEngine;

public class SettingsMenu : MonoBehaviour, IInitializable
{
    private MuteButton[] _muteButtons;

    public void Initialize()
    {
        _muteButtons = GetComponentsInChildren<MuteButton>();

        foreach (MuteButton button in _muteButtons)
            button.Initialize();
    }
}