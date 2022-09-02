using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour, IInitializable
{
    private Button _button;

    public UnityEvent OnClick => _button.onClick;
    
    public void Initialize() => _button = GetComponent<Button>();
}